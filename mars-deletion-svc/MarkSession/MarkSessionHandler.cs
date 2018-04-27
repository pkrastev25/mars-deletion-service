using System;
using System.Threading.Tasks;
using Hangfire;
using mars_deletion_svc.DependantResource.Interfaces;
using mars_deletion_svc.Exceptions;
using mars_deletion_svc.MarkingService.Interfaces;
using mars_deletion_svc.MarkSession.Interfaces;
using mars_deletion_svc.MarkSession.Models;
using mars_deletion_svc.Services.Inerfaces;

namespace mars_deletion_svc.MarkSession
{
    public class MarkSessionHandler : IMarkSessionHandler
    {
        private readonly IMarkingServiceClient _markingServiceClient;
        private readonly IDependantResourceHandler _dependantResourceHandler;
        private readonly ILoggerService _loggerService;

        public MarkSessionHandler(
            IMarkingServiceClient markingServiceClient,
            IDependantResourceHandler dependantResourceHandler,
            ILoggerService loggerService
        )
        {
            _markingServiceClient = markingServiceClient;
            _dependantResourceHandler = dependantResourceHandler;
            _loggerService = loggerService;
        }

        public async Task DeleteMarkSessionAndDependantResources(
            MarkSessionModel markSessionModel
        )
        {
            await Task.Run(() => BackgroundJob.Enqueue(() => StartDeletionProcess(markSessionModel.MarkSessionId)));
        }

        public async Task StartDeletionProcess(
            string markSessionId
        )
        {
            var isMarkSessionDeleted = false;
            var taskExecutionDelayInSeconds = 1;

            while (!isMarkSessionDeleted)
            {
                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(taskExecutionDelayInSeconds));

                    var markSessionModel = await _markingServiceClient.GetMarkSessionById(markSessionId);
                    await _dependantResourceHandler.DeleteDependantResourcesForMarkSession(markSessionModel);
                    await _markingServiceClient.DeleteMarkingSession(markSessionId);

                    isMarkSessionDeleted = true;
                }
                catch (MarkSessionDoesNotExistException)
                {
                    isMarkSessionDeleted = true;
                }
                catch (Exception e)
                {
                    _loggerService.LogErrorEvent(e);
                    taskExecutionDelayInSeconds *= 2;
                }
            }
        }
    }
}