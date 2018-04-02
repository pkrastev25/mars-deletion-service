using System;
using System.Threading.Tasks;
using mars_deletion_svc.DependantResource.Interfaces;
using mars_deletion_svc.MarkingService.Interfaces;
using mars_deletion_svc.Services.Inerfaces;
using Microsoft.AspNetCore.Mvc;

namespace mars_deletion_svc.DependantResource
{
    public class DependantResourcesHandler : IDependantResourcesHandler
    {
        private readonly IMarkingServiceClient _markingServiceClient;
        private readonly ILoggerService _loggerService;
        private readonly IErrorService _errorService;

        public DependantResourcesHandler(
            IMarkingServiceClient markingServiceClient,
            ILoggerService loggerService,
            IErrorService errorService
        )
        {
            _markingServiceClient = markingServiceClient;
            _loggerService = loggerService;
            _errorService = errorService;
        }

        public async Task<IActionResult> DeleteDependantResources(
            string resourceType,
            string resourceId,
            string projectId
        )
        {
            try
            {
                var dependantResources =
                    await _markingServiceClient.GetDependantResources(resourceType, resourceId, projectId);

                // TODO: Delete all of them !

                return new OkObjectResult(dependantResources);
            }
            catch (Exception e)
            {
                _loggerService.LogErrorEvent(e);

                return _errorService.GetStatusCodeResultForError(e);
            }
        }
    }
}