using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace SpringOff.DNEx
{
	class Program
	{
		async static Task Main(string[] args)
		{
			if (args.Length < 2)
			{
				Console.WriteLine("Wrong parameters supplied.");
				Console.WriteLine("DriverNotes.Export.exe <email> <password>");
				Console.WriteLine(Environment.NewLine);
				return;
			}

			var login = args[0];
			var password = args[1];

			NLog.LogManager.LoadConfiguration("nlog.config");

			ILoggerFactory loggerFactory = new LoggerFactory();
			loggerFactory.AddNLog();

			var apiService = new ApiService(loggerFactory);
			var dumpService = new DumpService(apiService, loggerFactory);

			await new ExportService(apiService, dumpService)
				.Dump(new LoginRequest(login, password));

			Console.WriteLine("All Done.");
			Console.WriteLine(Environment.NewLine);
		}
	}
}