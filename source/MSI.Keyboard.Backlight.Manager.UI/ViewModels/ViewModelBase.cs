using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MSI.Keyboard.Backlight.Manager.UI.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaiseAndSetIfChanged<T>(ref T refValue, T value, [CallerMemberName] string propertyName = null)
        {
            if (!ReferenceEquals(refValue, value))
            {
                refValue = value;
                RaisePropertyChanged(propertyName);
            }
        }

        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
