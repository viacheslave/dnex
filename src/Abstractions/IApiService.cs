using System.Threading.Tasks;

namespace SpringOff.DNEx
{
  /// <summary>
  ///   API service
  /// </summary>
	internal interface IApiService
  {
    Task<LoginResponse> Login(LoginRequest request);

    Task<string> GetData(string userHash);

    Task<string> GetCarData(int carId, int toShow);
  }
}