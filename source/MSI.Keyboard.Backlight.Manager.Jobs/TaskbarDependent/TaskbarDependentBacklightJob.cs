using MSI.Keyboard.Backlight.Configuration;
using MSI.Keyboard.Backlight.Service;
using System;
using System.Drawing;

namespace MSI.Keyboard.Backlight.Manager.Jobs.TaskbarDependentBacklight
{
    public class TaskbarDependentBacklightJob : BaseBacklightJob
    {
        public override TimeSpan RefreshInterval => TimeSpan.FromMilliseconds(100);

        public TaskbarDependentBacklightJob(IKeyboardService keyboardService, IBacklightConfigurationBuilder backlightConfigurationBuilder)
            : base(keyboardService, backlightConfigurationBuilder)
        {
        }

        protected override BacklightConfiguration Configure(IBacklightConfigurationBuilder builder)
        {
            var taskbarColor = GetTaskbarColor();

            return builder
                .ForAllRegions(taskbarColor, Intensity)
                .Build();
        }

        private Color GetTaskbarColor()
        {
            var taskbarPosition = ColorMatcher.GetTaskbarPosition();

            var x = taskbarPosition.X + taskbarPosition.Width - 1;
            var y = taskbarPosition.Y + (taskbarPosition.Height / 2);

            var point = new Point(x, y);

            return ColorMatcher.GetColourAt(point);
        }
    }
}
