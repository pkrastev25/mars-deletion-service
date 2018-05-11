using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using mars_deletion_svc.Exceptions;
using mars_deletion_svc.MarkingService;
using mars_deletion_svc.Services.Inerfaces;
using Moq;
using UnitTests._DataMocks;
using Xunit;

namespace UnitTests.MarkingService
{
    public class MarkingServiceClientTests
    {
        [Fact]
        public async void CreateMarkSession_OkStatusCode_ReturnsModel()
        {
            // Arrange
            var httpService = new Mock<IHttpService>();
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(MarkSessionModelDataMocks.MockMarkSessionModelJson)
            };
            httpService
                .Setup(m => m.PostAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(httpResponseMessage);
            var markingServiceClient = new MarkingServiceClient(httpService.Object);

            // Act
            var markSessionModel = await markingServiceClient.CreateMarkSession(
                It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>()
            );

            // Assert
            Assert.NotNull(markSessionModel);
        }

        [Fact]
        public async void CreateMarkSession_ConflictStatusCode_ThrowsException()
        {
            // Arrange
            var httpService = new Mock<IHttpService>();
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Conflict,
                Content = new StringContent("Some error has occurred!")
            };
            httpService
                .Setup(m => m.PostAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(httpResponseMessage);
            var markingServiceClient = new MarkingServiceClient(httpService.Object);
            Exception exception = null;

            try
            {
                // Act
                await markingServiceClient.CreateMarkSession(
                    It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<string>()
                );
            }
            catch (ResourceConflictException e)
            {
                exception = e;
            }

            // Assert
            Assert.IsType<ResourceConflictException>(exception);
        }

        [Fact]
        public async void CreateMarkSession_InternalServerErrorStatusCode_ThrowsException()
        {
            // Arrange
            var httpService = new Mock<IHttpService>();
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = new StringContent("Some error has occurred!")
            };
            httpService
                .Setup(m => m.PostAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(httpResponseMessage));
            var markingServiceClient = new MarkingServiceClient(httpService.Object);
            Exception exception = null;

            try
            {
                // Act
                await markingServiceClient.CreateMarkSession(
                    It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<string>()
                );
            }
            catch (FailedToCreateMarkSessionException e)
            {
                exception = e;
            }

            // Assert
            Assert.IsType<FailedToCreateMarkSessionException>(exception);
        }

        [Fact]
        public async void GetMarkSessionById_NotFoundStatusCode_ThrowsException()
        {
            // Arrange
            var httpService = new Mock<IHttpService>();
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound
            };
            httpService
                .Setup(m => m.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(httpResponseMessage);
            var markingServiceClient = new MarkingServiceClient(httpService.Object);
            Exception exception = null;

            try
            {
                // Act
                await markingServiceClient.GetMarkSessionById(It.IsAny<string>());
            }
            catch (MarkSessionDoesNotExistException e)
            {
                exception = e;
            }

            // Assert
            Assert.IsType<MarkSessionDoesNotExistException>(exception);
        }

        [Fact]
        public async void GetMarkSessionById_OkStatusCode_ReturnsModel()
        {
            // Arrange
            var httpService = new Mock<IHttpService>();
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(MarkSessionModelDataMocks.MockMarkSessionModelJson)
            };
            httpService
                .Setup(m => m.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(httpResponseMessage);
            var markingServiceClient = new MarkingServiceClient(httpService.Object);

            // Act
            var markSessionModel = await markingServiceClient.GetMarkSessionById(It.IsAny<string>());

            // Assert
            Assert.NotNull(markSessionModel);
        }

        [Fact]
        public async void GetMarkSessionsByMarkSessionType_NoContentStatusCode_ReturnsEmptyList()
        {
            // Arrange
            var httpService = new Mock<IHttpService>();
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NoContent,
                Content = new StringContent("")
            };
            httpService
                .Setup(m => m.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(httpResponseMessage);
            var markingServiceClient = new MarkingServiceClient(httpService.Object);

            // Act
            var markSessionModels = await markingServiceClient.GetMarkSessionsByMarkSessionType(It.IsAny<string>());

            // Assert
            Assert.Empty(markSessionModels);
        }

        [Fact]
        public async void GetMarkSessionsByMarkSessionType_InternalServerErrorStatusCode_ThrowsException()
        {
            // Arrange
            var httpService = new Mock<IHttpService>();
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = new StringContent("")
            };
            httpService
                .Setup(m => m.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(httpResponseMessage);
            var markingServiceClient = new MarkingServiceClient(httpService.Object);
            Exception exception = null;

            try
            {
                // Act
                await markingServiceClient.GetMarkSessionsByMarkSessionType(It.IsAny<string>());
            }
            catch (FailedToGetMarkSessionException e)
            {
                exception = e;
            }

            // Assert
            Assert.IsType<FailedToGetMarkSessionException>(exception);
        }

        [Fact]
        public async void UpdateMarkSessionType_OkStatusCode_NoExceptionThrown()
        {
            // Arrange
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK) {Content = new StringContent("")};
            var httpService = new Mock<IHttpService>();
            httpService
                .Setup(m => m.PutAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(httpResponseMessage);
            var markingServiceClient = new MarkingServiceClient(httpService.Object);
            Exception exception = null;

            try
            {
                // Act
                await markingServiceClient.UpdateMarkSessionType(It.IsAny<string>(), It.IsAny<string>());
            }
            catch (Exception e)
            {
                exception = e;
            }

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async void UpdateMarkSessionType_InternalServerErrorStatusCode_ThrowsException()
        {
            // Arrange
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("")
            };
            var httpService = new Mock<IHttpService>();
            httpService
                .Setup(m => m.PutAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(httpResponseMessage);
            var markingServiceClient = new MarkingServiceClient(httpService.Object);
            Exception exception = null;

            try
            {
                // Act
                await markingServiceClient.UpdateMarkSessionType(It.IsAny<string>(), It.IsAny<string>());
            }
            catch (Exception e)
            {
                exception = e;
            }

            // Assert
            Assert.NotNull(exception);
        }

        [Fact]
        public async void DeleteMarkingSession_NotFoundStatusCode_NoExceptionThrown()
        {
            // Arrange
            var httpService = new Mock<IHttpService>();
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent("")
            };
            httpService
                .Setup(m => m.DeleteAsync(It.IsAny<string>()))
                .ReturnsAsync(httpResponseMessage);
            var markingServiceClient = new MarkingServiceClient(httpService.Object);
            Exception exception = null;

            try
            {
                // Act
                await markingServiceClient.DeleteEmptyMarkingSession(It.IsAny<string>());
            }
            catch (Exception e)
            {
                exception = e;
            }

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async void DeleteMarkingSession_InternalServerErrorStatusCode_ThrowsException()
        {
            // Arrange
            var httpService = new Mock<IHttpService>();
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = new StringContent("")
            };
            httpService
                .Setup(m => m.DeleteAsync(It.IsAny<string>()))
                .ReturnsAsync(httpResponseMessage);
            var markingServiceClient = new MarkingServiceClient(httpService.Object);
            Exception exception = null;

            try
            {
                // Act
                await markingServiceClient.DeleteEmptyMarkingSession(It.IsAny<string>());
            }
            catch (FailedToDeleteMarkSessionException e)
            {
                exception = e;
            }

            // Assert
            Assert.IsType<FailedToDeleteMarkSessionException>(exception);
        }
    }
}