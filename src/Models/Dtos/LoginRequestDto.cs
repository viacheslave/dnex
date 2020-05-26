using Newtonsoft.Json;

namespace SpringOff.DNEx
{
	/// <summary>
	///		DTO for login request
	/// </summary>
	internal sealed class LoginRequestDto
	{
		[JsonProperty("user_login")]
		public string Login { get; set; }

		[JsonProperty("user_password")]
		public string Password { get; set; }
	}
}
