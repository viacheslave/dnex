using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog;

namespace SpringOff.DNEx
{
  internal interface IApiService
  {
    Task<LoginResponse> Login(LoginRequest request);
    Task<string> GetData(string userHash);

    Task<string> GetCarData(int carId, int toShow);
  }
  
  internal sealed class ApiService : IApiService
  {
    private const string BaseUrl = "http://www.drivernotes.net/api";
    private readonly Microsoft.Extensions.Logging.ILogger _logger;

    public ApiService(ILoggerFactory loggerFactory)
    {
      _logger = loggerFactory.CreateLogger<ApiService>();
    }

    public async Task<LoginResponse> Login(LoginRequest request)
    {
      if (request == null)
        throw new ArgumentNullException(nameof(request));
      
      var client = new HttpClient();
      var dto = new LoginRequestDto { Login = request.Login, Password = request.Password };
      var httpContent = new StringContent(JsonConvert.SerializeObject(dto));
      
      HttpResponseMessage responseMessage = await client.PostAsync($"{BaseUrl}/login", httpContent);
      if (responseMessage.IsSuccessStatusCode)
      {
        var response = await responseMessage.Content.ReadAsStringAsync();
        var responseDto = JsonConvert.DeserializeObject<LoginResponseDto>(response);
  
        if (responseDto.Status == "OK")
        {
          return new LoginResponse(responseDto.Data.UserId, responseDto.Data.UserHash);
        }
        else
        {
          _logger.LogError($"Unacceptable response status: {responseDto.Status}");          
        }
      }
      return null;
    }
  
    public async Task<string> GetData(string userHash)
    {
      if (userHash == null)
        throw new ArgumentNullException(nameof(userHash));
      
      var client = new HttpClient();
      client.DefaultRequestHeaders.Add("deviceID", "42");
      client.DefaultRequestHeaders.Add("DeviceType", "android");
      client.DefaultRequestHeaders.Add("Authorization", userHash);
      
      HttpResponseMessage responseMessage = await client.PostAsync($"{BaseUrl}/sync", null);
      if (responseMessage.IsSuccessStatusCode)
      {
        var responseBytes = await responseMessage.Content.ReadAsByteArrayAsync();
        var response = System.Text.Encoding.UTF8.GetString(responseBytes, 0, responseBytes.Length);
        var jsonObject = JsonConvert.DeserializeObject(response);
        
        return jsonObject.ToString();
      }
      return null;
    }

    public async Task<string> GetCarData(int carId, int toShow)
    {
      var client = new HttpClient();
      var dto = new CarStatsRequestDto { CarId = carId.ToString(), ToShow = toShow.ToString() };
      var httpContent = new StringContent(JsonConvert.SerializeObject(dto));
  
      HttpResponseMessage responseMessage = await client.PostAsync($"{BaseUrl}/car_statistics", httpContent);
      if (responseMessage.IsSuccessStatusCode)
      {
        var responseBytes = await responseMessage.Content.ReadAsByteArrayAsync();
        var response = System.Text.Encoding.UTF8.GetString(responseBytes, 0, responseBytes.Length);
        var jsonObject = JsonConvert.DeserializeObject(response);
        
        return jsonObject.ToString();
      }
      return null;
    }
  }
}