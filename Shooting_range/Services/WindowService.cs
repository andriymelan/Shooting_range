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
    public class WindowService
    {
        public void CloseWindow()
        {
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

            if(window != null)
            {
                window.Close();
            }
        }
        public void CloseApp()
        {
            Application.Current.Shutdown();
        }

        public void ShowWindow(Window window)
        {
            window.ShowDialog();
        }
    }
}
