﻿using System;
using System.Net.Http;
using IntegrationTests._ResourceTypeHelpers;
using IntegrationTests._ResourceTypeHelpers.Models;
using mars_deletion_svc.MarkingService;
using mars_deletion_svc.ResourceTypes.Enums;
using mars_deletion_svc.Services;
using Xunit;

namespace IntegrationTests.MarkingService
{
    public class MarkingServiceClientTests
    {
        [Fact]
        public async void CreateMarkSession_NotMarkedResources_ReturnsMarkSessionModel()
        {
            // Arrange
            var resourceType = ResourceTypeEnum.Metadata;
            var resourceId = "45db3205-83be-42a1-af14-6a03df9d9536";
            var projectId = "73fcb3bf-bc8b-4c8b-801f-8a90d92bf9c2";
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
            // Verify that the mark session is created
            Assert.NotNull(result);

            var metadata = await ResourceTypeHelper.RetreiveMetadata(resourceId);

            // Verify that the metadata is marked
            Assert.Equal(MetadataModel.ToBeDeletedState, metadata.State);
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
        public async void DeleteEmptyMarkingSession_MissingProjectId_ThrowsException()
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