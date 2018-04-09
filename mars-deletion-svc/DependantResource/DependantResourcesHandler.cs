using System;
using System.Linq;
using System.Threading.Tasks;
using mars_deletion_svc.DependantResource.Interfaces;
using mars_deletion_svc.MarkingService.Interfaces;
using mars_deletion_svc.ResourceTypes.Metadata.Interfaces;
using mars_deletion_svc.ResourceTypes.ResultConfig.Interfaces;
using mars_deletion_svc.ResourceTypes.Scenario.Interfaces;
using mars_deletion_svc.ResourceTypes.SimPlan.Interfaces;
using mars_deletion_svc.ResourceTypes.SimRun.Interfaces;
using mars_deletion_svc.Services.Inerfaces;
using Microsoft.AspNetCore.Mvc;

namespace mars_deletion_svc.DependantResource
{
    public class DependantResourcesHandler : IDependantResourcesHandler
    {
        private readonly IMarkingServiceClient _markingServiceClient;
        private readonly IMetadataClient _metadataClient;
        private readonly IScenarioClient _scenarioClient;
        private readonly IResultConfigClient _resultConfigClient;
        private readonly ISimPlanClient _simPlanClient;
        private readonly ISimRunClient _simRunClient;
        private readonly ILoggerService _loggerService;
        private readonly IErrorService _errorService;

        public DependantResourcesHandler(
            IMarkingServiceClient markingServiceClient,
            IMetadataClient metadataClient,
            IScenarioClient scenarioClient,
            IResultConfigClient resultConfigClient,
            ISimPlanClient simPlanClient,
            ISimRunClient simRunClient,
            ILoggerService loggerService,
            IErrorService errorService
        )
        {
            _markingServiceClient = markingServiceClient;
            _metadataClient = metadataClient;
            _scenarioClient = scenarioClient;
            _resultConfigClient = resultConfigClient;
            _simPlanClient = simPlanClient;
            _simRunClient = simRunClient;
            _loggerService = loggerService;
            _errorService = errorService;
        }

        public async Task<IActionResult> DeleteDependantResources(
            string resourceType,
            string resourceId,
            string projectId
        )
        {
            try
            {
                var dependantResources =
                    await _markingServiceClient.CreateMarkSession(resourceType, resourceId, projectId);
                dependantResources = dependantResources.Reverse();

                foreach (var dependantResourceModel in dependantResources)
                {
                    switch (dependantResourceModel.ResourceType)
                    {
                        case "metadata":
                        {
                            await _metadataClient.DeleteResource(dependantResourceModel);
                            break;
                        }
                        case "scenario":
                        {
                            await _scenarioClient.DeleteResource(dependantResourceModel);
                            break;
                        }
                        case "resultConfig":
                        {
                            await _resultConfigClient.DeleteResource(dependantResourceModel);
                            break;
                        }
                        case "simPlan":
                        {
                            await _simPlanClient.DeleteResource(dependantResourceModel);
                            break;
                        }
                        case "simRun":
                        {
                            await _simRunClient.DeleteResource(dependantResourceModel);
                            break;
                        }
                        case "resultData":
                        {
                            // TODO
                            break;
                        }
                        default:
                        {
                            _loggerService.LogWarningEvent(
                                $"Unknown {dependantResourceModel} is encountered while deleting! This might lead to an error in the system!"
                            );
                            break;
                        }
                    }
                }
                await _markingServiceClient.DeleteMarkingSession(resourceId);

                return new OkResult();
            }
            catch (Exception e)
            {
                _loggerService.LogErrorEvent(e);
                await _markingServiceClient.DeleteMarkingSession(resourceId);

                return _errorService.GetStatusCodeResultForError(e);
            }
        }
    }
}