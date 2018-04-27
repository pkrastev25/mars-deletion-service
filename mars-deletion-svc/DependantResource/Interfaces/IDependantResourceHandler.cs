using System.Threading.Tasks;
using mars_deletion_svc.MarkSession.Models;

namespace mars_deletion_svc.DependantResource.Interfaces
{
    public interface IDependantResourceHandler
    {
        Task DeleteDependantResourcesForMarkSession(
            MarkSessionModel markSessionModel
        );
    }
}