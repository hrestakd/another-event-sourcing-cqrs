using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CQRSSplitWise.Client.Command.DAL.Context;
using System;

namespace CQRSSplitWise.Client.Command
{
	public class Program
	{
		public static void Main(string[] args)
		{
			AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

			var host = CreateHostBuilder(args).Build();

			using (var scope = host.Services.CreateScope())
			{
				try
				{
					var context = scope.ServiceProvider.GetService<SplitWiseSQLContext>();
					context.Database.EnsureDeleted();
					context.Database.Migrate();
				}
				catch
				{
					throw;
				}
			}

			host.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
