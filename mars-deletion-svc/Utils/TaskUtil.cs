using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mars_deletion_svc.Utils
{
    public static class TaskUtil
    {
        public static Task ExecuteTasksInParallel(
            IEnumerable<Task> taskList
        )
        {
            var allTasks = Task.WhenAll(taskList);

            try
            {
                allTasks.Wait();
            }
            catch (Exception)
            {
                // Do nothing here, the exception will be rethown later
            }

            if (allTasks.Exception != null)
            {
                throw allTasks.Exception;
            }

            return allTasks;
        }
    }
}