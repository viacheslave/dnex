namespace SpringOff.DNEx
{
  internal sealed class LoginResponse
  {
    public int UserId { get; }
    public string UserHash { get; }

    public LoginResponse(int userId, string userHash)
    {
      UserId = userId;
      UserHash = userHash;
    }
  }
}