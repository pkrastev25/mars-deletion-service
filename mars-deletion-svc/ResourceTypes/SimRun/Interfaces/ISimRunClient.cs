using System.Threading.Tasks;
using mars_deletion_svc.MarkingService.Models;

namespace mars_deletion_svc.ResourceTypes.SimRun.Interfaces
{
    public interface ISimRunClient
    {
        Task DeleteResource(DependantResourceModel dependantResourceModel);
    }
}