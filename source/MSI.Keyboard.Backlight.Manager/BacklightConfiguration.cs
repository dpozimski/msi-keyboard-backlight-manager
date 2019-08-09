using System;

namespace MSI.Keyboard.Backlight.Manager
{
    public class BacklightConfiguration
    {
        public BacklightMode Mode { get; set; }
        public TimeSpan RefreshInterval { get; set; }
        public int Intensity { get; set; }
    }
}
