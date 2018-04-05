using System.Threading.Tasks;
using mars_deletion_svc.MarkingService.Models;

namespace mars_deletion_svc.ResourceTypes.SimPlan.Interfaces
{
    public interface ISimPlanClient
    {
        Task DeleteResource(DependantResourceModel dependantResourceModel);
    }
}