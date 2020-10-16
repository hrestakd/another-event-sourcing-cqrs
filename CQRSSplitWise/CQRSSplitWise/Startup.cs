using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MediatR;
using CQRSSplitWise.DAL.Context;
using Microsoft.EntityFrameworkCore;
using CQRSSplitWise.Config;
using Microsoft.Extensions.Options;
using CQRSSplitWise.DAL.Read;
using CQRSSplitWise.Services.Read;
using CQRSSplitWise.DAL.Read.Models;

namespace CQRSSplitWise
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<SplitWiseSQLContext>(x =>
			{
				x.UseSqlServer(Configuration["connectionStrings:SplitWiseSQLContext"]);
			});

			// Configure different collection configuration settings
			services.Configure<UserHistoryDBSettings>(Configuration.GetSection(nameof(NoSQLDBSettings)));
			services.Configure<GroupStateDBSettings>(Configuration.GetSection(nameof(NoSQLDBSettings)));
			services.Configure<GroupHistoryDBSettings>(Configuration.GetSection(nameof(NoSQLDBSettings)));
			services.Configure<WalletStateDBSettings>(Configuration.GetSection(nameof(NoSQLDBSettings)));

			services.AddSingleton(x => x.GetRequiredService<IOptions<UserHistoryDBSettings>>().Value);
			services.AddSingleton(x => x.GetRequiredService<IOptions<GroupStateDBSettings>>().Value);
			services.AddSingleton(x => x.GetRequiredService<IOptions<GroupHistoryDBSettings>>().Value);
			services.AddSingleton(x => x.GetRequiredService<IOptions<WalletStateDBSettings>>().Value);

			services.AddScoped<IQueryRepository<GroupState>, GroupStateQueryRepository>();
			services.AddScoped<IQueryRepository<GroupHistory>, GroupHistoryQueryRepository>();
			services.AddScoped<IQueryRepository<UserHistory>, UserHistoryQueryRepository>();
			services.AddScoped<IQueryRepository<WalletState>, WalletStateQueryRepository>();

			services.AddScoped<UserHistoryService>();
			services.AddScoped<GroupHistoryService>();

			services.AddControllers();
			services.AddMediatR(typeof(Startup));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
