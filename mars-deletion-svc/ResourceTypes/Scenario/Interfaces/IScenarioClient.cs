using System.Threading.Tasks;
using mars_deletion_svc.MarkingService.Models;

namespace mars_deletion_svc.ResourceTypes.Scenario.Interfaces
{
    public interface IScenarioClient
    {
        Task DeleteResource(DependantResourceModel dependantResourceModel);
    }
}