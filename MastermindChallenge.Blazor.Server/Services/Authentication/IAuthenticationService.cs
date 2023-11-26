namespace MastermindChallenge.Blazor.Server.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync(PlayerLoginDto loginModel);
    }
}
