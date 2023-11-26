namespace MastermindChallenge.Blazor.Server.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IClient _httpClient;

        public AuthenticationService(IClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> AuthenticateAsync(PlayerLoginDto loginModel)
        {
            //var response = await _httpClient.LoginAsync(loginModel);

            return true;
        }
    }
}
