using CQRSSplitWise.Client.Query.Config;
using AutoMapper;
using CQRSSplitWise.Client.Query.DAL.Models;
using CQRSSplitWise.Client.Query.DAL.Repositories;
using CQRSSplitWise.Client.Query.DAL.Views;
using CQRSSplitWise.Client.Query.Rabbit;
using CQRSSplitWise.Client.Query.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.ObjectPool;
using CQRSSplitWise.Extensions.Rabbit;

namespace CQRSSplitWise.Client.Query
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
			services.Configure<TransactionHistoryDBSettings>(Configuration.GetSection(nameof(NoSQLDBSettings)));

			services.AddSingleton(x => x.GetRequiredService<IOptions<TransactionHistoryDBSettings>>().Value);

			services.AddTransient<IQueryRepository<TransactionHistory>, TransactionHistoryQueryRepository>();
			services.AddTransient<IInsertRepository<TransactionHistory>, TransactionHistoryQueryRepository>();
			services.AddSingleton<IQueryRepository<UserStatusView>, UserStatusViewRepository>();

			services.AddScoped<UserQueryService>();
			services.AddScoped<GroupQueryService>();
			services.AddTransient<ProcessTransactionEventHandler>();

			services.AddControllers();

			services.AddAutoMapper(typeof(Startup));

			var configuration = new MapperConfiguration(cfg => cfg.AddMaps(new[] { typeof(Startup) }));
			configuration.CompileMappings();
			configuration.AssertConfigurationIsValid();

			services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
			services.AddSingleton(x =>
			{
				var provider = x.GetRequiredService<ObjectPoolProvider>();
				// initialize the rabbit channel and keep it in the object pool
				return provider.Create(new RabbitModelObjectPoolPolicy());
			});

			services.AddSingleton<RabbitMQListener>();

			services.AddHostedService<Initializer>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			app.ApplicationServices.GetService<RabbitMQListener>();
		}
	}
}
