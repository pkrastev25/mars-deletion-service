using System.Threading.Tasks;

namespace mars_deletion_svc.Controllers.Interfaces
{
    public interface IDeleteControllerHandler
    {
        Task<string> CreateMarkSessionAndDeleteDependantResurces(
            string resourceType,
            string resourceId,
            string projectId
        );

        Task<string> DeleteMarkSessionAndDependantResources(
            string markSessionId
        );
    }
}