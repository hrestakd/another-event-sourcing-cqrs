using CQRSSplitWise.Client.Query.Config;
using AutoMapper;
using CQRSSplitWise.Client.Query.DAL.Models;
using CQRSSplitWise.Client.Query.DAL.Repositories;
using CQRSSplitWise.Client.Query.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.ObjectPool;
using CQRSSplitWise.Extensions.Rabbit;
using EventStoreDB.Extensions;
using CQRSSplitWise.Client.Query.EventHandlers;

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
			services.Configure<MongoDBSettings>(Configuration.GetSection(nameof(NoSQLDBSettings)));

			services.AddSingleton(x => x.GetRequiredService<IOptions<MongoDBSettings>>().Value);

			// User services setup
			services.AddTransient<IRepository<UserData>, UserRepository>();
			services.AddTransient<UserService>();
			services.AddTransient<UserCreatedEventHandler>();

			// Group services setup
			services.AddTransient<IRepository<GroupData>, GroupRepository>();
			services.AddTransient<GroupService>();
			services.AddTransient<GroupCreatedEventHandler>();
			services.AddTransient<AddedUsersToGroupEventHandler>();

			// Transaction services setup
			services.AddTransient<IRepository<Transaction>, TransactionRepository>();
			services.AddTransient<TransactionService>();
			services.AddTransient<CreateTransactionEventHandler>();

			// User balances
			services.AddTransient<IRepository<UserBalance>, UserBalanceRepository>();
			services.AddTransient<UserBalanceService>();
			services.AddTransient<UpdateBalanceEventHandler>();

			services.AddControllers();

			services.CreateEventStoreClient(new EventStoreSettings(
				"http://eventstore:2113",
				"admin",
				"changeit",
				true));

			services.AddHostedService<HandlerInitializer>();
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
		}
	}
}
