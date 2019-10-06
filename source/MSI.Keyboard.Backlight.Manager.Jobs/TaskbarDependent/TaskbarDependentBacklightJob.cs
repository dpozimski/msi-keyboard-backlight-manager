using MSI.Keyboard.Backlight.Configuration;
using MSI.Keyboard.Backlight.Service;
using System;
using System.Drawing;

namespace MSI.Keyboard.Backlight.Manager.Jobs.TaskbarDependentBacklight
{
    public class TaskbarDependentBacklightJob : BaseBacklightJob
    {
        private Color? _taskbarColor;

        public override TimeSpan RefreshInterval => TimeSpan.FromMilliseconds(100);

        public TaskbarDependentBacklightJob(IKeyboardService keyboardService, IBacklightConfigurationBuilder backlightConfigurationBuilder)
            : base(keyboardService, backlightConfigurationBuilder)
        {
        }

        public override bool CanExecute()
        {
            var newTaskbarColor = GetTaskbarColor();

            if (_taskbarColor.HasValue && newTaskbarColor == _taskbarColor)
                return false;

            _taskbarColor = newTaskbarColor;

            return true;
        }

        protected override BacklightConfiguration Configure(IBacklightConfigurationBuilder builder)
        {
            return builder
                .ForAllRegions(_taskbarColor.Value, Intensity)
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
