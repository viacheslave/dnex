using System.Collections.Generic;
using Newtonsoft.Json;

namespace SpringOff.DNEx
{
  internal sealed class SyncDataObj
  {
    [JsonProperty("user")]
    public SyncDataUser User { get; set; }
  }
}