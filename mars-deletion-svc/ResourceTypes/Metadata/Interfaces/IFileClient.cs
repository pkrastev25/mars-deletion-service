using System.Threading.Tasks;
using mars_deletion_svc.MarkingService.Models;

namespace mars_deletion_svc.ResourceTypes.Metadata.Interfaces
{
    public interface IFileClient
    {
        Task DeleteResource(
            DependantResourceModel dependantResourceModel
        );
    }
}