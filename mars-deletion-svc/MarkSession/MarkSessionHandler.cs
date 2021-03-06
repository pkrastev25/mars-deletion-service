﻿using System;
using System.Diagnostics;
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
        private const int MaxDelayForJobInSeconds = 60;

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
            var restartCount = 0;
            var stopwatch = new Stopwatch();

            while (!isMarkSessionDeleted)
            {
                try
                {
                    _loggerService.LogBackgroundJobInfoEvent(
                        $"Deletion job for mark session with id: {markSessionId} will start in {taskExecutionDelayInSeconds} second/s, restart count: {restartCount}"
                    );
                    await Task.Delay(TimeSpan.FromSeconds(taskExecutionDelayInSeconds));
                    stopwatch.Start();

                    var markSessionModel = await _markingServiceClient.GetMarkSessionById(markSessionId);
                    await _dependantResourceHandler.DeleteDependantResourcesForMarkSession(markSessionModel);
                    await _markingServiceClient.DeleteEmptyMarkingSession(markSessionId);

                    stopwatch.Stop();
                    isMarkSessionDeleted = true;
                }
                catch (MarkSessionDoesNotExistException)
                {
                    stopwatch.Stop();
                    isMarkSessionDeleted = true;
                }
                catch (Exception e)
                {
                    stopwatch.Stop();
                    _loggerService.LogBackgroundJobErrorEvent(stopwatch.Elapsed.TotalSeconds, e);
                    taskExecutionDelayInSeconds = taskExecutionDelayInSeconds * 2 % MaxDelayForJobInSeconds;
                    restartCount++;
                }
            }

            _loggerService.LogBackgroundJobInfoEvent(
                stopwatch.Elapsed.TotalSeconds,
                $"Deletion job for mark session with id: {markSessionId} completed!"
            );
        }
    }
}