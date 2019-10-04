using System.Threading.Tasks;

namespace SpringOff.DNEx
{
	internal interface IDumpService
	{
		Task<SyncExportResult> DumpStats(LoginResponse loginResponse);
		Task<CarExportResult> DumpCarStats(int userId, int carId);
	}
}