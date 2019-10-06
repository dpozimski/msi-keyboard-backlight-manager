using FluentScheduler;
using System;
namespace MSI.Keyboard.Backlight.Manager.Jobs
{
    public class QuartzBacklightJobAdapter : IJob
    {
        private readonly IBacklightJob _backlightJob;

        public QuartzBacklightJobAdapter(IBacklightJob backlightJob)
        {
            _backlightJob = backlightJob;
        }

        public void Execute()
        {
            var canExecute = _backlightJob.CanExecute();

            if (canExecute)
                _backlightJob.Execute().Wait();
        }
    }
}
