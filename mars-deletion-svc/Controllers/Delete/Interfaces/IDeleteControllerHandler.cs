using System.Threading.Tasks;

namespace mars_deletion_svc.Controllers.Interfaces
{
    public interface IDeleteControllerHandler
    {
        Task CreateMarkSessionAndDeleteDependantResurces(
            string resourceType,
            string resourceId,
            string projectId
        );

        Task DeleteMarkSessionAndDependantResources(
            string markSessionId
        );
    }
}