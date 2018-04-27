using System.Collections.Generic;
using System.Threading.Tasks;
using mars_deletion_svc.DependantResource.Interfaces;
using mars_deletion_svc.Exceptions;
using mars_deletion_svc.MarkSession.Models;
using mars_deletion_svc.ResourceTypes.Enums;
using mars_deletion_svc.ResourceTypes.Metadata.Interfaces;
using mars_deletion_svc.ResourceTypes.ResultConfig.Interfaces;
using mars_deletion_svc.ResourceTypes.ResultData.Interfaces;
using mars_deletion_svc.ResourceTypes.Scenario.Interfaces;
using mars_deletion_svc.ResourceTypes.SimPlan.Interfaces;
using mars_deletion_svc.ResourceTypes.SimRun.Interfaces;
using mars_deletion_svc.Utils;

namespace mars_deletion_svc.DependantResource
{
    public class DependantResourceHandler : IDependantResourceHandler
    {
        private readonly IMetadataClient _metadataClient;
        private readonly IScenarioClient _scenarioClient;
        private readonly IResultConfigClient _resultConfigClient;
        private readonly ISimPlanClient _simPlanClient;
        private readonly ISimRunClient _simRunClient;
        private readonly IResultDataClient _resultDataClient;

        public DependantResourceHandler(
            IMetadataClient metadataClient,
            IScenarioClient scenarioClient,
            IResultConfigClient resultConfigClient,
            ISimPlanClient simPlanClient,
            ISimRunClient simRunClient,
            IResultDataClient resultDataClient
        )
        {
            _metadataClient = metadataClient;
            _scenarioClient = scenarioClient;
            _resultConfigClient = resultConfigClient;
            _simPlanClient = simPlanClient;
            _simRunClient = simRunClient;
            _resultDataClient = resultDataClient;
        }

        public async Task DeleteDependantResourcesForMarkSession(
            MarkSessionModel markSessionModel
        )
        {
            var taskList = new List<Task>();

            foreach (var dependantResourceModel in markSessionModel.DependantResources)
            {
                switch (dependantResourceModel.ResourceType)
                {
                    case ResourceTypeEnum.Project:
                        // The project must not be deleted !
                        break;
                    case ResourceTypeEnum.Metadata:
                        taskList.Add(
                            _metadataClient.DeleteResource(dependantResourceModel)
                        );
                        break;
                    case ResourceTypeEnum.Scenario:
                        taskList.Add(
                            _scenarioClient.DeleteResource(dependantResourceModel)
                        );
                        break;
                    case ResourceTypeEnum.ResultConfig:
                        taskList.Add(
                            _resultConfigClient.DeleteResource(dependantResourceModel)
                        );
                        break;
                    case ResourceTypeEnum.SimPlan:
                        taskList.Add(
                            _simPlanClient.DeleteResource(dependantResourceModel, markSessionModel.ProjectId)
                        );
                        break;
                    case ResourceTypeEnum.SimRun:
                        taskList.Add(
                            _simRunClient.DeleteResource(dependantResourceModel, markSessionModel.ProjectId)
                        );
                        break;
                    case ResourceTypeEnum.ResultData:
                        taskList.Add(
                            _resultDataClient.DeleteResource(dependantResourceModel)
                        );
                        break;
                    default:
                        throw new UnknownResourceTypeExcetion(
                            $"{dependantResourceModel.ResourceType} is unknown!"
                        );
                }
            }

            await TaskUtil.ExecuteTasksInParallel(taskList);
        }
    }
}