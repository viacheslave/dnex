using Newtonsoft.Json;

namespace SpringOff.DNEx
{
  internal sealed class LoginResponseDto
  {
    [JsonProperty("status")]
    public string Status { get; set; }
    
    [JsonProperty("data")]
    public LoginResponseUserDto Data { get; set; }
  }
}

