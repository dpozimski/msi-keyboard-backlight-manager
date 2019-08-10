namespace MSI.Keyboard.Backlight.Manager.Analytics
{
    public class AppCenterAnalyticsConfiguration : IAnalyticsConfiguration
    {
        private const string AppCenterId = "4f0f5536-a10d-4d45-b6d0-7b5cfa826655";

        public void Configure()
        {
            Microsoft.AppCenter.AppCenter.LogLevel = Microsoft.AppCenter.LogLevel.Verbose;

            Microsoft.AppCenter.AppCenter.Start(
                AppCenterId,
                typeof(Microsoft.AppCenter.Analytics.Analytics),
                typeof(Microsoft.AppCenter.Crashes.Crashes));
        }
    }
}
