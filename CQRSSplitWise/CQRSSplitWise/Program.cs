using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using CQRSSplitWise.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace CQRSSplitWise
{
	public class Program
	{
		public static void Main(string[] args)
		{
			//var host = CreateHostBuilder(args).Build();

			//using (var scope = host.Services.CreateScope())
			//{
			//	try
			//	{
			//		var context = scope.ServiceProvider.GetService<SplitWiseSQLContext>();
			//		context.Database.Migrate();
			//	}
			//	catch
			//	{
			//		throw;
			//	}
			//}

			//host.Run();

			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
