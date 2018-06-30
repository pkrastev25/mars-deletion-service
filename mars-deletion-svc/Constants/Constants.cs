using System;

namespace mars_deletion_svc.Constants
{
    public static class Constants
    {
        public const string FileSvcUrlKey = "FILE_SVC_URL";
        public const string MetadataSvcUrlKey = "METADATA_SVC_URL";
        public const string ScenarioSvcUrlKey = "SCENARIO_SVC_URL";
        public const string ResultConfigSvcUrlKey = "RESULT_CONFIG_SVC_URL";
        public const string SimRunnerSvcUrlKey = "SIM_RUNNER_SVC_URL";
        public const string DatabaseUtilitySvcUrlKey = "DATABASE_UTILITY_SVC_URL";
        public const string MarkingSvcUrlKey = "MARKING_SVC_URL";
        public const string MongoDbSvcUrlKey = "MONGO_DB_SVC_URL";

        public static readonly string MongoDbConnection =
            string.IsNullOrEmpty(Environment.GetEnvironmentVariable(MongoDbSvcUrlKey))
                ? "mongodb://mongodb:27017"
                : Environment.GetEnvironmentVariable(MongoDbSvcUrlKey);

        public const string MongoDbHangfireName = "hangfire-deletion-svc";
    }
}