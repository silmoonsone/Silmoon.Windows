using Silmoon.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Silmoon.Windows.WindowsApp.Commands
{
    public class UrlOpenCommand : ICommand
    {
#nullable enable
        public event EventHandler? CanExecuteChanged;
        public bool CanExecute(object? parameter)
        {
            return parameter is string url && !url.IsNullOrEmpty();
        }
#nullable disable

        public async void Execute(object parameter)
        {
            if (parameter is string url)
            {
                try
                {
                    await global::Windows.System.Launcher.LaunchUriAsync(new Uri(url));
                }
                catch
                {

                }
            }
        }
    }
}
