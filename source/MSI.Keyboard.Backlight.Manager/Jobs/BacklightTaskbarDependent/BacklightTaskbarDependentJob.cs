using MSI.Keyboard.Backlight.Configuration;
using MSI.Keyboard.Backlight.Service;
using System.Drawing;

namespace MSI.Keyboard.Backlight.Manager.Jobs.BacklightTaskbarDependent
{
    public class BacklightTaskbarDependentJob : IBacklightTaskbarDependentJob
    {
        private const int Intensity = 100;

        private readonly IKeyboardService _keyboardService;
        private readonly IBacklightConfigurationBuilder _backlightConfigurationBuilder;

        private Color _currentColor;

        public BacklightTaskbarDependentJob(IKeyboardService keyboardService,
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
            var taskbarPosition = ColorMatcher.GetTaskbarPosition().Location;

            return ColorMatcher.GetColourAt(taskbarPosition);
        }
    }
}
