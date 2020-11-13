using AutoMapper;
using CQRSSplitWise.Config;
using CQRSSplitWise.DAL.Context;
using CQRSSplitWise.DAL.Read;
using CQRSSplitWise.DAL.Read.Models;
using CQRSSplitWise.DAL.Read.Views;
using CQRSSplitWise.Rabbit;
using CQRSSplitWise.Services.Read;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;

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

			services.AddTransient<IQueryRepository<TransactionHistory>, TransactionHistoryQueryRepository>();
			services.AddTransient<IQueryRepository<UserStatusView>, UserStatusViewRepository>();

			services.AddScoped<UserQueryService>();
			services.AddScoped<GroupQueryService>();
			services.AddTransient<ProcessTransactionEventHandler>();

			services.AddControllers();
			services.AddAutoMapper(typeof(Startup));

			var configuration = new MapperConfiguration(cfg => cfg.AddMaps(new[] { typeof(Startup) }));
			configuration.CompileMappings();
			configuration.AssertConfigurationIsValid();

			services.AddMediatR(typeof(Startup));

			services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
			services.AddSingleton(x =>
			{
				var provider = x.GetRequiredService<ObjectPoolProvider>();
				// initialize the rabbit channel and keep it in the object pool
				return provider.Create(new RabbitModelObjectPoolPolicy());
			});
			services.AddSingleton<RabbitMQListener>();
			services.AddSingleton<RabbitMQPublisher>();

			services.AddHostedService<Initializer>();
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

			app.ApplicationServices.GetService<RabbitMQListener>();
		}
	}
}
