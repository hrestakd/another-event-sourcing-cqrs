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
using AutoMapper;

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
			services.Configure<TransactionHistoryDBSettings>(Configuration.GetSection(nameof(NoSQLDBSettings)));

			services.AddSingleton(x => x.GetRequiredService<IOptions<TransactionHistoryDBSettings>>().Value);

			services.AddScoped<IQueryRepository<TransactionHistory>, TransactionHistoryQueryRepository>();

			services.AddScoped<UserHistoryService>();
			services.AddScoped<GroupHistoryService>();
			services.AddScoped<ProcessTransactionEventHandler>();

			services.AddControllers();
			services.AddAutoMapper(typeof(Startup));

			var configuration = new MapperConfiguration(cfg => cfg.AddMaps(new[] { typeof(Startup) }));
			configuration.CompileMappings();
			configuration.AssertConfigurationIsValid();

			services.AddMediatR(typeof(Startup));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			//app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
