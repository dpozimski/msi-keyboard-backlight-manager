namespace MSI.Keyboard.Backlight.Manager.Settings
{
    public interface IFrontendAppSettings
    {
        bool ApplyConfigurationOnStartup { get; set; }
        bool StartMinimized { get;set; }
        bool RunOnStartup { get; set; }
    }
}
