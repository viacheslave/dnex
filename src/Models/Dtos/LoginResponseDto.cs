using Newtonsoft.Json;

namespace SpringOff.DNEx
{
	/// <summary>
	///		DTO for login response
	/// </summary>
	internal sealed class LoginResponseDto
	{
		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("data")]
		public LoginResponseUserDto Data { get; set; }
	}
}