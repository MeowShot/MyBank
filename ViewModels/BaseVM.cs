using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace MyBank.ViewModels
{
    public class BaseVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        protected static void Close(object ToClose)
        {
            (ToClose as Window).Close();
        }
    }
}
