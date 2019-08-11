using System;
using System.Threading;

namespace MSI.Keyboard.Backlight.Manager.UI.Services
{
    public class MutexBasedSingleInstanceValidator : ISingleInstanceValidator
    {
        private const string MutexName = @"Global\MSI.Keyboard.Backlight.Manager.UI";
        private Mutex _mutex;

        public bool Validate()
        {
            _mutex = new Mutex(true, MutexName, out var createdNew);

            GC.KeepAlive(_mutex);

            return createdNew;
        }
    }
}
