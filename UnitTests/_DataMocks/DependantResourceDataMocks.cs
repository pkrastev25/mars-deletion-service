using mars_deletion_svc.MarkingService.Models;

namespace UnitTests._DataMocks
{
    public static class DependantResourceDataMocks
    {
        public static DependantResourceModel MockDependantResourceModel()
        {
            return new DependantResourceModel
            {
                ResourceId = "acd8b6d6-5490-4240-9cf3-045b214c7912",
                ResourceType = "metadata"
            };
        }
    }
}