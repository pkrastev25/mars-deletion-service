using System.Net;
using System.Net.Http;
using System.Threading;
using mars_deletion_svc.Services;
using mars_deletion_svc.Services.Inerfaces;
using Moq;
using Xunit;

namespace UnitTests.Services
{
    public class HttpServiceTests
    {
        [Fact]
        public async void PostAsync_OkStatusCode_ReturnsOkStatusCode()
        {
            // Arrange
            var httpClient = new Mock<HttpClient>();
            httpClient
                .Setup(m => m.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));
            var httpService = new HttpService(httpClient.Object);

            // Act
            var result = await httpService.PostAsync(It.IsAny<string>(), It.IsAny<string>());

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async void GetAsync_InternalServerErrorStatusCode_ReturnsInternalServerErrorStatusCode()
        {
            // Arrange
            var httpService = new Mock<IHttpService>();
            httpService
                .Setup(m => m.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.InternalServerError));

            // Act
            var result = await httpService.Object.GetAsync(It.IsAny<string>());

            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }
        
        [Fact]
        public async void PutAsync_OkStatusCode_ReturnsOkStatusCode()
        {
            // Arrange
            var httpClient = new Mock<HttpClient>();
            httpClient
                .Setup(m => m.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));
            var httpService = new HttpService(httpClient.Object);

            // Act
            var result = await httpService.PutAsync(It.IsAny<string>(), It.IsAny<string>());

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async void DeleteAsync_NotFoundStatusCode_ReturnsNotFoundStatusCode()
        {
            // Arrange
            var httpClient = new Mock<HttpClient>();
            httpClient
                .Setup(m => m.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.NotFound));
            var httpService = new HttpService(httpClient.Object);

            // Act
            var result = await httpService.DeleteAsync(It.IsAny<string>());

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}