using System.ComponentModel;

namespace ch.hsr.wpf.gadgeothek.main.vms
{
    public abstract class Base : INotifyPropertyChanged
    {
            public event PropertyChangedEventHandler PropertyChanged;
            protected bool SetProperty<T>(ref T field, T value, string name = null)
            {
                if (Equals(field, value))
                    return false;
                field = value;
                OnPropertyChanged(name);
                return true;
            }
            protected void OnPropertyChanged(string name = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
    }
}
