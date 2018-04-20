using System.Threading.Tasks;
using mars_deletion_svc.MarkSession.Models;

namespace mars_deletion_svc.MarkSession.Interfaces
{
    public interface IMarkSessionHandler
    {
        Task DeleteMarkSessionAndDependantResources(MarkSessionModel markSessionModel);
    }
}