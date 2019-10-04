using System;
using System.Linq;
using System.Threading.Tasks;

namespace SpringOff.DNEx
{
  internal sealed class ExportService
  {
    private readonly IApiService _apiService;
    private readonly IDumpService _dumpService;

    public ExportService(IApiService apiService, IDumpService dumpService)
    {
      _dumpService = dumpService;
      _apiService = apiService;
    }

    public async Task Dump(LoginRequest loginRequest)
    {
      if (loginRequest == null)
        throw new ArgumentNullException(nameof(loginRequest));

      var loginResponse = await _apiService.Login(loginRequest);
      if (loginResponse == null)
				throw new ApplicationException("Login request returned no response");
      
      var syncExportResult = await _dumpService.DumpStats(loginResponse);
      if (syncExportResult == null)
				throw new ApplicationException("Stats request returned no response");

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

