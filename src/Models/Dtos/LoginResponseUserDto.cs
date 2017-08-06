using Newtonsoft.Json;

namespace SpringOff.DNEx
{
  internal sealed class LoginResponseUserDto
  {
    [JsonProperty("user_id")]
    public int UserId { get; set; }
  
    [JsonProperty("user_hash")]
    public string UserHash { get; set; }
  }
}

