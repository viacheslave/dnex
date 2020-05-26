using Newtonsoft.Json;

namespace SpringOff.DNEx
{
	/// <summary>
	///		User response DTO
	/// </summary>
	internal sealed class LoginResponseUserDto
	{
		[JsonProperty("user_id")]
		public int UserId { get; set; }

		[JsonProperty("user_hash")]
		public string UserHash { get; set; }
	}
}

