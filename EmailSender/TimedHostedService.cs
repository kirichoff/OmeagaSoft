using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmailSender
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private Timer _timer;
        private EmailSendercs sender;

        public TimedHostedService(EmailSendercs _sender)
        {
            sender = _sender;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            

            _timer = new Timer(DoWork, null, TimeSpan.FromHours(23 - DateTime.Now.Hour),
                TimeSpan.FromHours(24));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            sender.OnTimedEvent();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
