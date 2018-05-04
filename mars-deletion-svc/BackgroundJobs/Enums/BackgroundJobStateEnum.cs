namespace mars_deletion_svc.BackgroundJobs.Enums
{
    public static class BackgroundJobStateEnum
    {
        public const string HangfireStateSucceededForBackgroundJob = "Succeeded";

        public const string StateDoneForBackgroundJob = "Done";
        public const string StateProcessingForBackgroundJob = "Processing";
    }
}