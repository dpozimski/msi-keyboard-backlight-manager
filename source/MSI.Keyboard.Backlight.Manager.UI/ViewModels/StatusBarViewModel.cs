using MediatR;
using MSI.Keyboard.Backlight.Manager.Queries;
using System;
using System.Timers;
using System.Windows.Input;

namespace MSI.Keyboard.Backlight.Manager.UI.ViewModels
{
    public class StatusBarViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;

        private bool _deviceNotSupported;

        public bool DeviceNotSupported
        {
            get => _deviceNotSupported;
            private set => RaiseAndSetIfChanged(ref _deviceNotSupported, value);
        }

        public DateTime Now => DateTime.Now;

        public ICommand CheckIsDeviceSupportedCommand { get; }

        public StatusBarViewModel(IMediator mediator)
        {
            _mediator = mediator;

            CheckIsDeviceSupportedCommand = new RelayCommand<object>(CheckIsDeviceSupported);

            InitTimer();
        }

        private void InitTimer()
        {
            var timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += (o, e) => RaisePropertyChanged(nameof(Now));
            timer.Start();
        }

        private async void CheckIsDeviceSupported(object obj)
        {
            DeviceNotSupported = !(await _mediator.Send(new CheckIfDeviceIsSupportedQuery()));
        }
    }
}
