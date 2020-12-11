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
