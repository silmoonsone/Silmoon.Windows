using Silmoon.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Silmoon.Windows.WinUI3.Commands
{
    public class UrlOpenCommand : ICommand
    {
#nullable enable
#pragma warning disable CS0067
        public event EventHandler? CanExecuteChanged;
#pragma warning restore CS0067
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
