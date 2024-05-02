using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Shooting_range.Services
{
    public interface IWindowService
    {
        void OpenSettingsWindow();
        void OpenSureExitWindow();
        void CloseWindow();
    }
}
