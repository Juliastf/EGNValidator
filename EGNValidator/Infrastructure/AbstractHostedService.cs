using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EGNValidator.Infrastructure
{
    public abstract class AbstractHostedService : IHostedService
    {
        private Task executingTask;
        private readonly CancellationTokenSource stoppingCts = new CancellationTokenSource();

        public AbstractHostedService()
        {

        }

        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            // Store the task we're executing
            executingTask = ExecuteAsync(stoppingCts.Token);

            // If the task is completed then return it,
            // this will bubble cancellation and failure to the caller
            if (executingTask.IsCompleted)
            {
                return executingTask;
            }

            // Otherwise it's running
            return Task.CompletedTask;
        }

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            // Stop called without start
            if (executingTask == null)
            {
                return;
            }

            try
            {
                // Signal cancellation to the executing method
                stoppingCts.Cancel();
            }
            finally
            {
                // Wait until the task completes or the stop token triggers
                await Task.WhenAny(executingTask, Task.Delay(Timeout.Infinite,
                                                          cancellationToken));
            }
        }

        protected virtual async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //stoppingToken.Register(() =>
            //        _logger.LogDebug($" GracePeriod background task is stopping."));

            do
            {
                await Process();

                await Task.Delay(50000, stoppingToken); //50 seconds delay
            }
            while (!stoppingToken.IsCancellationRequested);
        }

        protected abstract Task Process();
    }
}
