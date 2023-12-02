using Blazored.LocalStorage;
using MastermindChallenge.Blazor.Server.Providers;
using Microsoft.AspNetCore.Components.Authorization;

namespace MastermindChallenge.Blazor.Server.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthenticationService(IClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
        }
        public async Task<bool> AuthenticateAsync(PlayerLoginDto loginModel)
        {
            var response = await _httpClient.LoginAsync(loginModel);

            // Store token
            await _localStorage.SetItemAsync("authToken", response.Token);

            // Change auth state
            await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedIn();

            return true;
        }

        public async Task Logout()
        {
            await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedOut();
        }
    }
}
