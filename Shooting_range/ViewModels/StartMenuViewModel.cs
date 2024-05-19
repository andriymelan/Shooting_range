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
using System.IO;
using System.Globalization;
using System.Threading;

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
        public StartMenuViewModel()
        {
            StartMenuVisibility = new StartMenuModel();
            OpenSettingsCommand = new RelayCommand(OpenSettings);
            OpenSureExitCommand = new RelayCommand(OpenSureExit);
            BackToMenuCommand = new RelayCommand(BackToMenu);
            CloseAppCommand = new RelayCommand(CloseApp);
            OpenPlayCommand = new RelayCommand(OpenPlay);
            string cursorDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Cursor";
            cursor = ($@"{cursorDirectory}\\MainCursor.cur");
            ChangeEnglishLanguageCommand = new RelayCommand(ChangeEnglishLanguage);
            ChangeUkrainianLanguageCommand = new RelayCommand(ChangeUkrainianLanguage);
            ChangeSpanishLanguageCommand = new RelayCommand(ChangeSpanishLanguage);
            OpenSoundSettingsCommand = new RelayCommand(OpenSoundSettings);
            OpenCustomizeSettingsCommand = new RelayCommand(OpenCustomizeSettings);
            OpenLanguageSettingsCommand = new RelayCommand(OpenLanguageSettings);
        }
        #region CreateProperty
        public RelayCommand OpenSettingsCommand {  get; set; }
        public RelayCommand CloseAppCommand { get; set; }
        public RelayCommand OpenSureExitCommand {  get; set; }
        public RelayCommand BackToMenuCommand {  get; set; }
        public RelayCommand OpenPlayCommand { get; set; }
        public RelayCommand ChangeEnglishLanguageCommand { get; set; }
        public RelayCommand ChangeUkrainianLanguageCommand { get; set; }
        public RelayCommand ChangeSpanishLanguageCommand { get; set; }
        public RelayCommand OpenSoundSettingsCommand {  get; set; }
        public RelayCommand OpenCustomizeSettingsCommand {  get; set; }
        public RelayCommand OpenLanguageSettingsCommand {  get; set; }
        private string Cursor { get; set; }
        public string cursor
        {
            get { return Cursor; }
            set
            {
                Cursor = value;
                OnPropertyChanged(nameof(Cursor));
            }
        }
        private StartMenuModel startMenuVisibility { get; set; }
        public StartMenuModel StartMenuVisibility
        {
            get { return startMenuVisibility; }
            set
            {
                startMenuVisibility = value;
                OnPropertyChanged(nameof(startMenuVisibility));
            }
        }
        #endregion
        #region StartMenuButtons
        private void BackToMenu(object sender)
        {
            OpenSmth("InitialVisibility");
        }


        private void OpenPlay(object sender)
        {
            OpenSmth("PlayVisibility");
        }


        private void OpenSettings(object sender)
        {
            OpenSmth("SettingsVisibility");
        }


        private void OpenSureExit(object sender)
        {
            OpenSmth("SureExitVisibility");
        }


        private void CloseApp(object sender)
        {
            Application.Current.Shutdown();
        }

        private void OpenSmth(string visibleParametr)
        {
            HideAll();
            switch (visibleParametr)
            {
                case "InitialVisibility":
                    StartMenuVisibility.InitialVisibility = Visibility.Visible;
                    break;
                case "PlayVisibility":
                    StartMenuVisibility.PlayVisibility = Visibility.Visible;
                    break;
                case "SettingsVisibility":
                    StartMenuVisibility.SettingsVisibility = Visibility.Visible;
                    break;
                case "SureExitVisibility":
                    StartMenuVisibility.SureExitVisibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }
        private void HideAll()
        {
            StartMenuVisibility.InitialVisibility = Visibility.Collapsed;
            StartMenuVisibility.SettingsVisibility = Visibility.Collapsed;
            StartMenuVisibility.SureExitVisibility = Visibility.Collapsed;
            StartMenuVisibility.PlayVisibility = Visibility.Collapsed;
        }
        #endregion
        #region LanguageChange
        private void ChangeEnglishLanguage(object sender)
        {
            Changelanguage("en-US");
        }
        private void ChangeUkrainianLanguage(object sender)
        {
            Changelanguage("uk-UA");
        }
        private void ChangeSpanishLanguage(object sender)
        {
            Changelanguage("es-ES");
        }
        private void Changelanguage(string language)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo($"{language}");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo($"{language}");

            Application.Current.Resources.MergedDictionaries.Clear();
            ResourceDictionary resdict = new ResourceDictionary()
            {
                Source = new Uri($"LanguagePack/Dictionary-{language}.xaml", UriKind.Relative)
            };
            Application.Current.Resources.MergedDictionaries.Add(resdict);
        }
        #endregion
        #region SettingsButtons
        private void OpenSoundSettings(object sender)
        {
            OpenSomeSettings("SoundSettings");
        }
        private void OpenCustomizeSettings(object sender)
        {
            OpenSomeSettings("CustomizeSettings");
        }
        private void OpenLanguageSettings(object sender)
        {
            OpenSomeSettings("LanguageSettings");
        }
        private void OpenSomeSettings(string settings)
        {
            CloseAllSettings();
            switch (settings)
            {
                case "SoundSettings":
                    StartMenuVisibility.SoundSettings = Visibility.Visible;
                    break;
                case "CustomizeSettings":
                    StartMenuVisibility.CustomizeSettings = Visibility.Visible;
                    break;
                case "LanguageSettings":
                    StartMenuVisibility.LanguageSettings = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }
        private void CloseAllSettings()
        {
            StartMenuVisibility.SoundSettings = Visibility.Collapsed;
            StartMenuVisibility.CustomizeSettings = Visibility.Collapsed;
            StartMenuVisibility.LanguageSettings = Visibility.Collapsed;
        }
        #endregion
    }

}
