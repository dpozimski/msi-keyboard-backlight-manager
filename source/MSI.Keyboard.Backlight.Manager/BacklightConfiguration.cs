using System;

namespace MSI.Keyboard.Backlight.Manager
{
    public class BacklightConfiguration
    {
        public bool BacklightTaskbarDependent { get; set; }
        public TimeSpan RefreshInterval { get; set; }
    }
}
