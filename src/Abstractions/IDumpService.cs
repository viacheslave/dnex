using System.Threading.Tasks;

namespace SpringOff.DNEx
{
	/// <summary>
	///		Saves results to filesystem
	/// </summary>
	internal interface IDumpService
	{
		/// <summary>
		///		Saves general stats
		/// </summary>
		/// <param name="loginResponse"></param>
		/// <returns></returns>
		Task<SyncExportResult> DumpStats(LoginResponse loginResponse);

		/// <summary>
		///		Saves car stats
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="carId"></param>
		/// <returns></returns>
		Task<CarExportResult> DumpCarStats(int userId, int carId);
	}
}