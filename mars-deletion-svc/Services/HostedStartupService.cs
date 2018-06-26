using System;
using System.Threading;
using System.Threading.Tasks;
using mars_deletion_svc.MarkingService;
using mars_deletion_svc.MarkingService.Interfaces;
using mars_deletion_svc.MarkSession.Interfaces;
using mars_deletion_svc.Services.Inerfaces;

namespace mars_deletion_svc.Services
{
    public class HostedStartupService : AHostedService
    {
        private readonly IMarkingServiceClient _markingServiceClient;
        private readonly IMarkSessionHandler _markSessionHandler;
        private readonly ILoggerService _loggerService;

        public HostedStartupService(
            IMarkingServiceClient markingServiceClient,
            IMarkSessionHandler markSessionHandler,
            ILoggerService loggerService
        )
        {
            _markingServiceClient = markingServiceClient;
            _markSessionHandler = markSessionHandler;
            _loggerService = loggerService;
        }

        protected override async Task ExecuteAsync(
            CancellationToken cancellationToken
        )
        {
            await DeletePendingMarkSessions(cancellationToken);
        }

        private async Task DeletePendingMarkSessions(
            CancellationToken cancellationToken
        )
        {
            _loggerService.LogStartupInfoEvent("Hosted service started! Attemping to delete legacy mark sessions.");

            try
            {
                var markSessionModelsToBeDeleted = await _markingServiceClient.GetMarkSessionsByMarkSessionType(
                    MarkingServiceClient.MarkSessionTypeToBeDeleted
                );

                foreach (var markSessionModel in markSessionModelsToBeDeleted)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }

                    await _markSessionHandler.DeleteMarkSessionAndDependantResources(markSessionModel);
                }
            }
            catch (Exception e)
            {
                _loggerService.LogStartupErrorEvent(e);
            }

            _loggerService.LogStartupInfoEvent("Hosted service stopped!");
        }
    }
}