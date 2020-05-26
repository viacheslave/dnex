using Newtonsoft.Json;

namespace SpringOff.DNEx
{
	internal sealed class SyncDataResponse
  {
    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("data")]
    public SyncDataObj Data { get; set; }
  }
}