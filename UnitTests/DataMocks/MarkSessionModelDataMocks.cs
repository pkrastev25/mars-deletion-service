using System.Collections.Generic;
using mars_deletion_svc.MarkingService.Models;
using mars_deletion_svc.MarkSession.Models;
using Newtonsoft.Json;

namespace UnitTests.DataMocks
{
    public static class MarkSessionModelDataMocks
    {
        public static readonly string MockMarkSessionModelJson = JsonConvert.SerializeObject(MockMarkSessionModel());

        public static MarkSessionModel MockMarkSessionModel()
        {
            return new MarkSessionModel
            {
                MarkSessionId = "5ae86f68b90b230007d7ea34",
                ProjectId = "be1cabd5-c121-49a0-9860-824419efb39a",
                DependantResources = new List<DependantResourceModel>
                {
                    new DependantResourceModel
                    {
                        ResourceId = "acd8b6d6-5490-4240-9cf3-045b214c7912",
                        ResourceType = "metadata"
                    },
                    new DependantResourceModel
                    {
                        ResourceId = "3f9c4a5b-c4db-4098-bea9-333cacdc58b1",
                        ResourceType = "scenario"
                    },
                    new DependantResourceModel
                    {
                        ResourceId = "3f9c4a5b-c4db-4098-bea9-333cacdc58b1",
                        ResourceType = "resultConfig"
                    },
                    new DependantResourceModel
                    {
                        ResourceId = "3f9c4a5b-c4db-4098-bea9-333cacdc58b1",
                        ResourceType = "simPlan"
                    },
                    new DependantResourceModel
                    {
                        ResourceId = "3f9c4a5b-c4db-4098-bea9-333cacdc58b1",
                        ResourceType = "simRun"
                    },
                    new DependantResourceModel
                    {
                        ResourceId = "3f9c4a5b-c4db-4098-bea9-333cacdc58b1",
                        ResourceType = "resultData"
                    }
                }
            };
        }
    }
}