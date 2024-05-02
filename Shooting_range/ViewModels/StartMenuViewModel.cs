using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Shooting_range.Services;
using Shooting_range.Views;
using Open_Window_Trial_1.Utilities;

namespace Shooting_range.ViewModels
{
    public class StartMenuViewModel
    {
        private WindowService windowService;
        public ICommand OpenMenuSettingsCommand { get; set; }
        public ICommand OpenSureExitCommand { get; set; }
        public ICommand CloseWindowCommand { get; set;}
        private void OnOpenSettingsWindow()
        {
            windowService.OpenSettingsWindow();
        }
        private void OnOpenSureExitWindow()
        {
            windowService.OpenSureExitWindow();
        }
        private void OnCloseWindow()
        {
            windowService.CloseWindow();
        }
        public StartMenuViewModel (WindowService windowService)
        {
            this.windowService = windowService;
            OpenMenuSettingsCommand = new RelayCommand(param => OnOpenSettingsWindow());
            OpenSureExitCommand = new RelayCommand(param => OnOpenSureExitWindow());
            CloseWindowCommand = new RelayCommand(param => OnCloseWindow());
        }
    }

}
