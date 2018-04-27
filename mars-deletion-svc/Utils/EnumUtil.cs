using mars_deletion_svc.ResourceTypes.Enums;

namespace mars_deletion_svc.Utils
{
    public static class EnumUtil
    {
        public static bool DoesResourceTypeExist(
            string resourceType
        )
        {
            switch (resourceType)
            {
                case ResourceTypeEnum.Project:
                case ResourceTypeEnum.Metadata:
                case ResourceTypeEnum.Scenario:
                case ResourceTypeEnum.ResultConfig:
                case ResourceTypeEnum.SimPlan:
                case ResourceTypeEnum.SimRun:
                case ResourceTypeEnum.ResultData:
                    return true;
                default:
                    return false;
            }
        }
    }
}