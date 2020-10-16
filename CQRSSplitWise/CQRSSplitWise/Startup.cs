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

			services.Configure<NoSQLDBSettings>(Configuration.GetSection(nameof(NoSQLDBSettings)));

			services.AddSingleton(x => x.GetRequiredService<IOptions<NoSQLDBSettings>>().Value);

			services.AddTransient<GroupStateQueryRepository>();
			services.AddTransient<GroupHistoryQueryRepository>();
			services.AddTransient<UserHistoryQueryRepository>();
			services.AddTransient<WalletStateQueryRepository>();

			//services.AddTransient<TestService>();

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
