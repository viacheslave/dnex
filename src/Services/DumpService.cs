using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SpringOff.DNEx
{
	internal sealed class DumpService : IDumpService
	{
		private readonly IApiService _apiService;
		private readonly ILogger _logger;

		private readonly DateTime _stamp = DateTime.Now;

		public DumpService(IApiService apiService, ILoggerFactory loggerFactory)
		{
			_apiService = apiService;
			_logger = loggerFactory.CreateLogger<DumpService>();
		}

		public async Task<SyncExportResult> DumpStats(LoginResponse loginResponse)
		{
			var userData = await _apiService.GetData(loginResponse.UserHash);
			if (userData == null)
				return null;

			var syncData = JsonConvert.DeserializeObject<SyncDataResponse>(userData);
			if (syncData.Status != "OK")
				return null;

			_logger.LogInformation($"syncdata length: {userData.Length}");

			var exportFolder = CreateExportFolderIsNotExists();
			var fileName = $"drivernotes.syncdata.{loginResponse.UserId}.json";
			var filePath = Path.Combine(exportFolder, fileName);
			File.AppendAllText(filePath, userData);

			_logger.LogTrace($"Dumped syncdata to {filePath}");

			return new SyncExportResult
			{
				UserId = loginResponse.UserId,
				FileLocation = filePath,
				Response = syncData
			};
		}

		public async Task<CarExportResult> DumpCarStats(int userId, int carId)
		{
			var carExportResult = new CarExportResult();

			foreach (var showValue in Enumerable.Range(1, 2))
			{
				var carData = await _apiService.GetCarData(carId, showValue);
				if (carData == null)
					return null;

				_logger.LogInformation($"cardata {carId}, {showValue} length: {carData.Length}");

				var exportFolder = CreateExportFolderIsNotExists();
				var fileName = $"drivernotes.cardata.{userId}.{carId}.{showValue}.json";
				var filePath = Path.Combine(exportFolder, fileName);
				File.AppendAllText(filePath, carData);

				_logger.LogTrace($"Dumped cardata {carId}, {showValue} to {filePath}");

				carExportResult.Exports.Add(filePath);
			}

			return carExportResult;
		}

		private string CreateExportFolderIsNotExists()
		{
			var currentFolder = Environment.CurrentDirectory;
			var exportFolder = Path.Combine(currentFolder, Path.Combine("export", _stamp.ToString("yyyyMMddHHmmss")));

			if (!Directory.Exists(exportFolder))
				Directory.CreateDirectory(exportFolder);

			return exportFolder;
		}
	}
}