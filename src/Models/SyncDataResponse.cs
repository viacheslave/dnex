using System.Collections.Generic;
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

  internal sealed class SyncDataObj
  {
    [JsonProperty("user")]
    public SyncDataUser User { get; set; }
  }

  internal sealed class SyncDataUser
  {
    [JsonProperty("cars")]
    public List<SyncDataCar> Cars { get; set; } = new List<SyncDataCar>();
  }

  internal sealed class SyncDataCar
  {
    [JsonProperty("id")]
    public int Id { get; set; }
  }
}