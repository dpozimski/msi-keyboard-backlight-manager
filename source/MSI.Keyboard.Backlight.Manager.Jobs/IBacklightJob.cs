using System;
using System.Threading.Tasks;

namespace MSI.Keyboard.Backlight.Manager.Jobs
{
    public interface IBacklightJob
    {
        int Intensity { get; set; }
        TimeSpan RefreshInterval { get; }
        Task Execute();
    }
}
