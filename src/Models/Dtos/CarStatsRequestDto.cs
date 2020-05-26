using Newtonsoft.Json;

namespace SpringOff.DNEx
{
	/// <summary>
	///		Car stats DTO
	/// </summary>
	internal sealed class CarStatsRequestDto
	{
		[JsonProperty("car_id")]
		public string CarId { get; set; }

		[JsonProperty("to_show")]
		public string ToShow { get; set; }
	}
}
