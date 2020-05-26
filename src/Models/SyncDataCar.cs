using Newtonsoft.Json;

namespace SpringOff.DNEx
{
  internal sealed class SyncDataCar
  {
    [JsonProperty("id")]
    public int Id { get; set; }
  }
}