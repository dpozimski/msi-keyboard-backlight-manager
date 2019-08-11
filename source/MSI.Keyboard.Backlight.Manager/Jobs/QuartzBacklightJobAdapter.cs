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
            _backlightJob.Execute().Wait();
        }
    }
}
