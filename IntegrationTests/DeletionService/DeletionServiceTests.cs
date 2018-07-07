using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using mars_deletion_svc;
using mars_deletion_svc.BackgroundJobs.Enums;
using mars_deletion_svc.Constants;
using mars_deletion_svc.ResourceTypes.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace IntegrationTests.DeletionService
{
    public class DeletionServiceTests
    {
        private readonly HttpClient _deletionServiceClient;

        public DeletionServiceTests()
        {
            var deletionServiceServer = new TestServer(
                new WebHostBuilder().UseStartup<Startup>()
            );
            _deletionServiceClient = deletionServiceServer.CreateClient();
        }

        [Fact]
        public async void DeleteResourcesBasedOnMetadata_ValidMetadataId_ResourcesDeleted()
        {
            // Arrange
            var resourceType = ResourceTypeEnum.Metadata;
            var resourceId = "9812b85c-aac7-4d20-bfb8-a3bd9b9292de";
            var projectId = "f3aced7f-d27f-4694-b5e7-5ed40d4944f7";
            var httpClient = new HttpClient();

            // Act
            var deleteResourceResponse = await _deletionServiceClient.DeleteAsync(
                $"/api/delete/{resourceType}/{resourceId}?projectId={projectId}"
            );

            // Assert
            // Validate that the background job is created
            Assert.Equal(HttpStatusCode.Accepted, deleteResourceResponse.StatusCode);

            // Wait for the background job to complete, this time period should be sufficient
            await Task.Delay(TimeSpan.FromSeconds(30));

            var backgroundJobId = await deleteResourceResponse.Content.ReadAsStringAsync();
            var backgroundJobStatusResponse = await _deletionServiceClient.GetAsync(
                $"/api/backgroundjob/{backgroundJobId}/status"
            );
            var backgroundJobStatus = await backgroundJobStatusResponse.Content.ReadAsStringAsync();

            // Validate that the background job is completed
            Assert.Equal(backgroundJobStatus, BackgroundJobStateEnum.StateDoneForBackgroundJob);

            var resourceResponse = await httpClient.GetAsync(
                GenerateUrlForRetrievingResource(resourceType, resourceId)
            );

            // Validate that the resource is deleted
            Assert.Equal(HttpStatusCode.NotFound, resourceResponse.StatusCode);
        }

        private string GenerateUrlForRetrievingResource(
            string resourceType,
            string resourceId
        )
        {
            switch (resourceType)
            {
                case ResourceTypeEnum.Metadata:
                    var metadataBaseUrlEnvironmental = Environment.GetEnvironmentVariable(Constants.MetadataSvcUrlKey);
                    var metadataBaseUrl = string.IsNullOrEmpty(metadataBaseUrlEnvironmental)
                        ? "metadata-svc"
                        : metadataBaseUrlEnvironmental;

                    return $"http://{metadataBaseUrl}/metadata/{resourceId}";
                default:
                    throw new ArgumentException($"No base URL specified for resource type: {resourceType}");
            }
        }
    }
}