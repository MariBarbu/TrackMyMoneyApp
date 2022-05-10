using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Jobs
{
    public class JobsRunnerService : IHostedService, IDisposable
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private bool _doJob;

        public JobsRunnerService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _doJob = false;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _doJob = true;
            new Task(DoJob).Start();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _doJob = false;
            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }

        private void DoJob()
        {
            int index = 0;

            while (_doJob)
            {
                Thread.Sleep(1000);
                index++;

                if (index %10 == 0)
                {
                    ExecuteJobs();
                }
            }
        }

        private void ExecuteJobs() 
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var monthJob = scope.ServiceProvider.GetService<IMonthJob>();
           
            ExecuteJob(monthJob);
            
        }

        private void ExecuteJob(IJob job)
        {
            var jobName = job.GetType().ToString();
            try
            {
                Console.WriteLine($"=======================================> Running job ({jobName}): " + DateTime.Now);
                job.Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine($"JOB FAILED - {jobName}");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        
    }
}
