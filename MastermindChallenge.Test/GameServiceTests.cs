using Xunit;
using Moq;
using MastermindChallenge.Blazor.Server.Services.Game;
using MastermindChallenge.Blazor.Server.Models;
using MastermindChallenge.Blazor.Server.Providers;
using System.Security.Claims;
using System.Threading.Tasks;
using MastermindChallenge.Blazor.Server.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace MastermindChallenge.Test
{
    public class GameServiceTests
    {
        private readonly Mock<IClient> _httpClientMock;
        private readonly Mock<ApiAuthenticationStateProvider> _authStateProviderMock;
        private readonly GameService _gameService;

        public GameServiceTests()
        {
            _httpClientMock = new Mock<IClient>();
            _authStateProviderMock = new Mock<ApiAuthenticationStateProvider>();
            _gameService = new GameService(_httpClientMock.Object, _authStateProviderMock.Object);
        }

        [Fact]
        public async Task SaveGameAsync_ShouldCallHttpClientSaveGameAsync_WithCorrectPlayerId()
        {
            // Arrange
            var gameDto = new SaveGameDto();
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim("uid", "testUid") }));
            _authStateProviderMock.Setup(x => x.GetAuthenticationStateAsync()).ReturnsAsync(new AuthenticationState(claimsPrincipal));
            _httpClientMock.Setup(x => x.SaveGameAsync(It.IsAny<SaveGameDto>())).Returns(Task.CompletedTask);

            // Act
            await _gameService.SaveGameAsync(gameDto);

            // Assert
            _httpClientMock.Verify(x => x.SaveGameAsync(It.Is<SaveGameDto>(y => y.PlayerId == "testUid")), Times.Once);
        }
    }
}