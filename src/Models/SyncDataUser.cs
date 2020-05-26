using System.Collections.Generic;
using Newtonsoft.Json;

namespace SpringOff.DNEx
{
	internal sealed class SyncDataUser
  {
    [JsonProperty("cars")]
    public List<SyncDataCar> Cars { get; set; } = new List<SyncDataCar>();
  }
}