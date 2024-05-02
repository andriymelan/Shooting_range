using Shooting_range.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Shooting_range.Views;

namespace Shooting_range.Services
{
    public class WindowService : IWindowService
    {
        public void OpenSettingsWindow()
        {
            var window = new MenuSettings();
            window.ShowDialog();
        }

        public void OpenSureExitWindow()
        {
            var window = new SureExit();
            window.ShowDialog();
        }
        public void CloseWindow()
        {
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

            if(window != null)
            {
                window.Close();
            }
        }
               
    }
}
