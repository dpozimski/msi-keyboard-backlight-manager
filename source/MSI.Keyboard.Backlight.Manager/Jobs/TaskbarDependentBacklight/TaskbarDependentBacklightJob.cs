using MSI.Keyboard.Backlight.Configuration;
using MSI.Keyboard.Backlight.Service;
using System.Drawing;

namespace MSI.Keyboard.Backlight.Manager.Jobs.TaskbarDependentBacklight
{
    public class TaskbarDependentBacklightJob : ITaskbarDependentBacklightJob
    {
        private readonly IKeyboardService _keyboardService;
        private readonly IBacklightConfigurationBuilder _backlightConfigurationBuilder;

        private Color _currentColor;

        public int Intensity { get; set; }
        public bool RequireRefreshing => true;

        public TaskbarDependentBacklightJob(IKeyboardService keyboardService,
                                            IBacklightConfigurationBuilder backlightConfigurationBuilder)
        {
            _keyboardService = keyboardService;
            _backlightConfigurationBuilder = backlightConfigurationBuilder;
        }

        public void Execute()
        {
            var taskbarColor = GetTaskbarColor();

            if (taskbarColor == _currentColor)
                return;

            _currentColor = taskbarColor;

            var backlightConfiguration = _backlightConfigurationBuilder
                .ForAllRegions(taskbarColor, Intensity)
                .Build();

            _keyboardService.ApplyConfigurationAsync(backlightConfiguration).Wait();
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
