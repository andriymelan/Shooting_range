using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Shooting_range.Services;
using Shooting_range.Views;
using Shooting_range.Models;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Shooting_range.ViewModels
{
    public class StartMenuViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public ICommand OpenSettingsCommand {  get; set; }
        public ICommand CloseAppCommand { get; set; }
        public ICommand OpenSureExitCommand {  get; set; }
        public ICommand BackToMenuCommand {  get; set; }
        public ICommand OpenPlayCommand { get; set; }

        public StartMenuViewModel()
        {
            StartMenuVisibility = new StartMenuModel();
            OpenSettingsCommand = new RelayCommand(OpenSettings);
            OpenSureExitCommand = new RelayCommand(OpenSureExit);
            BackToMenuCommand = new RelayCommand(BackToMenu);
            CloseAppCommand = new RelayCommand(CloseApp);
            OpenPlayCommand = new RelayCommand(OpenPlay);
        }


        private StartMenuModel startMenuVisibility {  get; set; }
        public StartMenuModel StartMenuVisibility
        {
            get { return startMenuVisibility; }
            set 
            {
                startMenuVisibility = value; 
                OnPropertyChanged(nameof(startMenuVisibility));
            }
        }


        private void BackToMenu(object sender)
        {
            HideAll();
            StartMenuVisibility.InitialVisibility = Visibility.Visible;
        }


        private void OpenPlay(object sender)
        {
            HideAll();
            StartMenuVisibility.PlayVisibility = Visibility.Visible;
        }


        private void OpenSettings(object sender)
        {
            HideAll();
            StartMenuVisibility.SettingsVisibility = Visibility.Visible;
        }


        private void OpenSureExit(object sender)
        {
            HideAll();
            StartMenuVisibility.SureExitVisibility = Visibility.Visible;
        }


        private void CloseApp(object sender)
        {
            Application.Current.Shutdown();
        }


        private void HideAll()
        {
            StartMenuVisibility.InitialVisibility = Visibility.Collapsed;
            StartMenuVisibility.SettingsVisibility = Visibility.Collapsed;
            StartMenuVisibility.SureExitVisibility = Visibility.Collapsed;
            StartMenuVisibility.PlayVisibility = Visibility.Collapsed;
        }
    }

}
