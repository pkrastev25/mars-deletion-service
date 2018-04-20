using System.Threading.Tasks;
using mars_deletion_svc.Controllers.Interfaces;
using mars_deletion_svc.MarkingService.Interfaces;
using mars_deletion_svc.MarkSession.Interfaces;

namespace mars_deletion_svc.Controllers
{
    public class DeleteControllerHandler : IDeleteControllerHandler
    {
        private readonly IMarkingServiceClient _markingServiceClient;
        private readonly IMarkSessionHandler _markSessionHandler;

        public DeleteControllerHandler(
            IMarkingServiceClient markingServiceClient,
            IMarkSessionHandler markSessionHandler
        )
        {
            _markingServiceClient = markingServiceClient;
            _markSessionHandler = markSessionHandler;
        }

        public async Task CreateMarkSessionAndDeleteDependantResurces(
            string resourceType,
            string resourceId,
            string projectId
        )
        {
            var markSessionModel = await _markingServiceClient.CreateMarkSession(
                resourceType,
                resourceId,
                projectId
            );

            await _markSessionHandler.DeleteMarkSessionAndDependantResources(markSessionModel);
        }
    }
}