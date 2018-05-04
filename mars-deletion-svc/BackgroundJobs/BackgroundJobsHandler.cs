using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Hangfire;
using mars_deletion_svc.BackgroundJobs.Enums;
using mars_deletion_svc.BackgroundJobs.Interfaces;
using mars_deletion_svc.Exceptions;
using MongoDB.Bson;

namespace mars_deletion_svc.BackgroundJobs
{
    public class BackgroundJobsHandler : IBackgroundJobsHandler
    {
        private readonly IBackgroundJobClient _backgroundJobClient;

        public BackgroundJobsHandler(
            IBackgroundJobClient backgroundJobClient
        )
        {
            _backgroundJobClient = backgroundJobClient;
        }

        public async Task<string> CreateBackgroundJob(
            Expression<Func<Task>> backgroundJob
        )
        {
            return await Task.Run(
                () =>  _backgroundJobClient.Enqueue(
                    backgroundJob
                )
            );
        }

        public async Task<string> GetJobStatusForBackgroundJobId(
            string backgroundJobId
        )
        {
            return await Task.Run(() =>
            {
                try
                {
                    var unused = new BsonObjectId(new ObjectId(backgroundJobId));
                }
                catch (Exception e)
                {
                    throw new BackgroundJobDoesNotExistException(
                        $"Background job with id: {backgroundJobId} does not exist!",
                        e
                    );
                }

                var connection = JobStorage.Current.GetConnection();
                var jobData = connection.GetJobData(backgroundJobId);

                if (jobData == null)
                {
                    throw new BackgroundJobDoesNotExistException(
                        $"Background job with id: {backgroundJobId} does not exist!"
                    );
                }

                var stateName = jobData.State;

                return stateName == BackgroundJobStateEnum.HangfireStateSucceededForBackgroundJob
                    ? BackgroundJobStateEnum.StateDoneForBackgroundJob
                    : BackgroundJobStateEnum.StateProcessingForBackgroundJob;
            });
        }
    }
}