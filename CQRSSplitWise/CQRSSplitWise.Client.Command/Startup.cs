using AutoMapper;
using CQRSSplitWise.Client.Command.DAL.Context;
using CQRSSplitWise.Extensions.Rabbit;
using EventStoreDB.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.ObjectPool;

namespace CQRSSplitWise.Client.Command
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

			services.AddControllers();
			services.AddAutoMapper(typeof(Startup));

			var configuration = new MapperConfiguration(cfg => cfg.AddMaps(new[] { typeof(Startup) }));
			configuration.CompileMappings();
			configuration.AssertConfigurationIsValid();

			services.AddMediatR(typeof(Startup));

			//services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
			//services.AddSingleton(x =>
			//{
			//	var provider = x.GetRequiredService<ObjectPoolProvider>();
			//	// initialize the rabbit channel and keep it in the object pool
			//	return provider.Create(new RabbitModelObjectPoolPolicy());
			//});
			//services.AddSingleton<RabbitMQPublisher>();

			services.CreateEventStoreClient(new EventStoreSettings(
				"http://eventstore:2113",
				"admin",
				"changeit",
				true));
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
