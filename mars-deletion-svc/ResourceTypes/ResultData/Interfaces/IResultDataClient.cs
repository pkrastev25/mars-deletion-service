using System.Threading.Tasks;
using mars_deletion_svc.MarkingService.Models;

namespace mars_deletion_svc.ResourceTypes.ResultData.Interfaces
{
    public interface IResultDataClient
    {
        Task DeleteResource(
            DependantResourceModel dependantResourceModel
        );
    }
}