using MSI.Keyboard.Backlight.Manager.UI.Services;
using System;
using System.Timers;
using System.Windows.Input;

namespace MSI.Keyboard.Backlight.Manager.UI.ViewModels
{
    public class StatusBarViewModel : ViewModelBase, IDisposable
    {
        private readonly IKeyboardBacklightService _keyboardBacklightService;
        private bool _deviceNotSupported;
        private Timer _timer;

        public bool DeviceNotSupported
        {
            get => _deviceNotSupported;
            private set => RaiseAndSetIfChanged(ref _deviceNotSupported, value);
        }

        public DateTime Now => DateTime.Now;

        public ICommand CheckIsDeviceSupportedCommand { get; }

        public StatusBarViewModel(IKeyboardBacklightService keyboardBacklightService)
        {
            _keyboardBacklightService = keyboardBacklightService;

            CheckIsDeviceSupportedCommand = new RelayCommand<object>(CheckIsDeviceSupported);

            InitTimer();
        }

        private void InitTimer()
        {
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Elapsed += (o, e) => RaisePropertyChanged(nameof(Now));
            _timer.Start();
        }

        private async void CheckIsDeviceSupported(object obj)
        {
            DeviceNotSupported = !await _keyboardBacklightService.IsDeviceSupported();
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
