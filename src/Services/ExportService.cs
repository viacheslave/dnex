using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SpringOff.DNEx
{
  internal sealed class ExportService
  {
    private readonly IApiService _apiService;
    private readonly IDumpService _dumpService;
    private readonly Microsoft.Extensions.Logging.ILogger _logger;

    public ExportService(IApiService apiService, IDumpService dumpService, ILoggerFactory loggerFactory)
    {
      _dumpService = dumpService;
      _apiService = apiService;
      _logger = loggerFactory.CreateLogger<ExportService>();
    }
    public async Task Dump(LoginRequest loginRequest)
    {
      if (loginRequest == null)
        throw new ArgumentNullException(nameof(loginRequest));

      var loginResponse = await _apiService.Login(loginRequest);
      if (loginResponse == null)
        return;
      
      var syncExportResult = await _dumpService.DumpStats(loginResponse);
      if (syncExportResult == null)
        return;

      Console.WriteLine($"Sync Data exported to: {syncExportResult.FileLocation}");

      var cars = syncExportResult.Response?.Data?.User?.Cars;
      if (cars?.Any() == true)
      {
        foreach (var car in cars)
        {
          var carExportResult = await _dumpService.DumpCarStats(loginResponse.UserId, car.Id);

          foreach (var carExportResultLocation in carExportResult.Exports)
            Console.WriteLine($"Car {car.Id} Data exported to: {carExportResultLocation}");
        }
      }
    }
  }
}

