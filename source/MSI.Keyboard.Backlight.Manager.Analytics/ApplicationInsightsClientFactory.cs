using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.WindowsServer.TelemetryChannel;
using System;
using System.Diagnostics;
using System.Reflection;

namespace MSI.Keyboard.Backlight.Manager.Analytics
{
    public class ApplicationInsightsClientFactory : IApplicationInsightsClientFactory
    {
        private const string TelemetryKey = "0e64d9b0-7d70-49cc-b74b-de7c3d52e981";

        public TelemetryClient Create()
        {
            var config = new TelemetryConfiguration();
            config.InstrumentationKey = TelemetryKey;
            config.TelemetryChannel = new ServerTelemetryChannel();
            config.TelemetryChannel.DeveloperMode = Debugger.IsAttached;
#if DEBUG
            config.TelemetryChannel.DeveloperMode = true;
#endif
            var client = new TelemetryClient(config);
            client.Context.Component.Version = Assembly.GetEntryAssembly().GetName().Version.ToString();
            client.Context.Session.Id = Guid.NewGuid().ToString();
            client.Context.User.Id = $"{Environment.UserName}_{Environment.MachineName}";
            client.Context.Device.OperatingSystem = Environment.OSVersion.ToString();

            return client;
        }
    }
}
