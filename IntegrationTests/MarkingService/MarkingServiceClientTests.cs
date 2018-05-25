using System;
using System.Net.Http;
using mars_deletion_svc.MarkingService;
using mars_deletion_svc.ResourceTypes.Enums;
using mars_deletion_svc.Services;
using Xunit;

namespace IntegrationTests.MarkingService
{
    public class MarkingServiceClientTests
    {
        [Fact]
        public async void CreateMarkSession_UnmarkedResources_ReturnsMarkSessionModel()
        {
            // Arrange
            var resourceType = ResourceTypeEnum.Metadata;
            var resourceId = "4439722e-a6d0-4f7a-9d33-0cc5a2a66da0";
            var projectId = "623be379-ed40-49f3-bdd8-416f8cd0faa6";
            var markSessionType = MarkingServiceClient.MarkSessionTypeToBeDeleted;
            var httpService = new HttpService(new HttpClient());
            var markingServiceClient = new MarkingServiceClient(httpService);

            // Act
            var result = await markingServiceClient.CreateMarkSession(
                resourceType,
                resourceId,
                projectId,
                markSessionType
            );

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetMarkSessionById_ValidMarkSessionId_ReturnsMarkSessionModel()
        {
            // Arrange
            var markSessionId = "5b07decf7aa54a0007b3db51";
            var httpService = new HttpService(new HttpClient());
            var markingServiceClient = new MarkingServiceClient(httpService);

            // Act
            var result = await markingServiceClient.GetMarkSessionById(markSessionId);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetMarkSessionsByMarkSessionType_MarkSessionModelExists_ReturnsMarkSessionModelList()
        {
            // Arrange
            var markSessionType = MarkingServiceClient.MarkSessionTypeToBeDeleted;
            var httpService = new HttpService(new HttpClient());
            var markingServiceClient = new MarkingServiceClient(httpService);

            // Act
            var result = await markingServiceClient.GetMarkSessionsByMarkSessionType(markSessionType);

            // Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public async void UpdateMarkSessionType_MarkSessionModelExists_NoExceptionThrown()
        {
            // Arrange
            var markSessionId = "5b07dee57aa54a0007b3db52";
            var markSessionType = MarkingServiceClient.MarkSessionTypeToBeDeleted;
            var httpService = new HttpService(new HttpClient());
            var markingServiceClient = new MarkingServiceClient(httpService);
            Exception exception = null;

            try
            {
                // Act
                await markingServiceClient.UpdateMarkSessionType(markSessionId, markSessionType);
            }
            catch (Exception e)
            {
                exception = e;
            }

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async void DeleteEmptyMarkingSession_ValidMarkSessionId_ThrowsException()
        {
            // Arrange
            var markSessionId = "5b07def67aa54a0007b3db53";
            var httpService = new HttpService(new HttpClient());
            var markingServiceClient = new MarkingServiceClient(httpService);
            Exception exception = null;

            try
            {
                // Act
                await markingServiceClient.DeleteEmptyMarkingSession(markSessionId);
            }
            catch (Exception e)
            {
                exception = e;
            }

            // Assert
            Assert.NotNull(exception);
        }
    }
}