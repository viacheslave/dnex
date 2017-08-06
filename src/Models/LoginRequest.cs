namespace SpringOff.DNEx
{
  internal sealed class LoginRequest
  {
    public string Login { get; }
    public string Password { get; }

    public LoginRequest(string login, string password)
    {
      Login = login;
      Password = password;
    }
  }
}