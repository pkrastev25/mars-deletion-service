using System;
using System.Threading.Tasks;
using mars_deletion_svc.BackgroundJobs.Interfaces;
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
        private readonly IBackgroundJobsHandler _backgroundJobsHandler;
        private readonly IMarkingServiceClient _markingServiceClient;
        private readonly IDependantResourceHandler _dependantResourceHandler;
        private readonly ILoggerService _loggerService;

        public MarkSessionHandler(
            IBackgroundJobsHandler backgroundJobsHandler,
            IMarkingServiceClient markingServiceClient,
            IDependantResourceHandler dependantResourceHandler,
            ILoggerService loggerService
        )
        {
            _backgroundJobsHandler = backgroundJobsHandler;
            _markingServiceClient = markingServiceClient;
            _dependantResourceHandler = dependantResourceHandler;
            _loggerService = loggerService;
        }

        public async Task<string> DeleteMarkSessionAndDependantResources(
            MarkSessionModel markSessionModel
        )
        {
            return await _backgroundJobsHandler.CreateBackgroundJob(
                () => StartDeletionProcess(markSessionModel.MarkSessionId)
            );
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
                    await _markingServiceClient.DeleteEmptyMarkingSession(markSessionId);

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