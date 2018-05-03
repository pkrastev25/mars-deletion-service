using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using mars_deletion_svc.Utils;
using Xunit;

namespace UnitTests.Utils
{
    public class TaskUtilTests
    {
        [Fact]
        public async void ExecuteTasksInParallel_CompletedTasks_NoExceptionThrown()
        {
            // Arrange
            var taskList = new List<Task>
            {
                Task.CompletedTask,
                Task.CompletedTask,
                Task.CompletedTask
            };
            Exception exception = null;

            // Act
            try
            {
                await TaskUtil.ExecuteTasksInParallel(taskList);
            }
            catch (Exception e)
            {
                exception = e;
            }

            // Assert
            Assert.Null(exception);
        }
        
        [Fact]
        public async void ExecuteTasksInParallel_FailedTask_ThrowsException()
        {
            // Arrange
            var taskList = new List<Task>
            {
                Task.CompletedTask,
                Task.CompletedTask,
                Task.FromException(new Exception())
            };
            Exception exception = null;

            // Act
            try
            {
                await TaskUtil.ExecuteTasksInParallel(taskList);
            }
            catch (Exception e)
            {
                exception = e;
            }

            // Assert
            Assert.NotNull(exception);
        }
    }
}