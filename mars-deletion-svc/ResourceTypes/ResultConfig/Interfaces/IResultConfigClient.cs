using System.Threading.Tasks;
using mars_deletion_svc.MarkingService.Models;

namespace mars_deletion_svc.ResourceTypes.ResultConfig.Interfaces
{
    public interface IResultConfigClient
    {
        Task DeleteResource(
            DependantResourceModel dependantResourceModel
        );
    }
}