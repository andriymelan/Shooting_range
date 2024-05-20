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
            ApplySettingsChangeCommand = new RelayCommand(ApplySettingsChange);
            TargetPathChangeAquaCommand = new RelayCommand(TargetPathChangeAqua);
            TargetPathChangeBlackCommand = new RelayCommand(TargetPathChangeBlack);
            TargetPathChangeBlueCommand = new RelayCommand(TargetPathChangeBlue);
            TargetPathChangeGreenCommand = new RelayCommand(TargetPathChangeGreen);
            TargetPathChangeOrangeCommand = new RelayCommand(TargetPathChangeOrange);
            TargetPathChangePurpleCommand = new RelayCommand(TargetPathChangePurple);
            TargetPathChangeRedCommand = new RelayCommand(TargetPathChangeRed);
            TargetPathChangeYellowCommand = new RelayCommand(TargetPathChangeYellow);
        }


        #region CreateProperty


        SettingsProperty settingsProperty = new SettingsProperty();
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
        public RelayCommand ApplySettingsChangeCommand {  get; set; }
        public RelayCommand TargetPathChangeAquaCommand {  get; set; }
        public RelayCommand TargetPathChangeBlackCommand {  get; set; }
        public RelayCommand TargetPathChangeBlueCommand {  get; set; }
        public RelayCommand TargetPathChangeGreenCommand {  get; set; }
        public RelayCommand TargetPathChangeOrangeCommand {  get; set; }
        public RelayCommand TargetPathChangePurpleCommand {  get; set; }
        public RelayCommand TargetPathChangeRedCommand {  get; set; }
        public RelayCommand TargetPathChangeYellowCommand {  get; set; }
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
        private bool isEnabledApplyButton {  get; set; } = false;
        public bool IsEnabledApplyButton
        {
            get { return isEnabledApplyButton; }
            set
            {
                isEnabledApplyButton = value;
                OnPropertyChanged(nameof(isEnabledApplyButton));
            }
        }
        private string ApplyLanguageChange { get; set; }
        private string TargetPathChange { get; set; }


        #region BorderProperties


        private int borderChangeLanguageEnglish { get; set; } = 0;
        public int BorderChangeLanguageEnglish {
            get 
            {
                return borderChangeLanguageEnglish;
            }
            set 
            {
                borderChangeLanguageEnglish = value;
                OnPropertyChanged(nameof(borderChangeLanguageEnglish));
            } 
        }
        private int borderChangeLanguageUkrainian { get; set; } = 0;
        public int BorderChangeLanguageUkrainian
        {
            get
            {
                return borderChangeLanguageUkrainian;
            }
            set
            {
                borderChangeLanguageUkrainian = value;
                OnPropertyChanged(nameof(borderChangeLanguageUkrainian));
            }
        }
        private int borderChangeLanguageSpanish { get; set; } = 0;
        public int BorderChangeLanguageSpanish
        {
            get
            {
                return borderChangeLanguageSpanish;
            }
            set
            {
                borderChangeLanguageSpanish = value;
                OnPropertyChanged(nameof(borderChangeLanguageSpanish));
            }
        }
        private int borderChangeTargetAqua { get; set; } = 0;
        public int BorderChangeTargetAqua
        {
            get
            {
                return borderChangeTargetAqua;
            }
            set
            {
                borderChangeTargetAqua = value;
                OnPropertyChanged(nameof(borderChangeTargetAqua));
            }
        }
        private int borderChangeTargetBlack { get; set; } = 0;
        public int BorderChangeTargetBlack
        {
            get
            {
                return borderChangeTargetBlack;
            }
            set
            {
                borderChangeTargetBlack = value;
                OnPropertyChanged(nameof(borderChangeTargetBlack));
            }
        }
        private int borderChangeTargetBlue { get; set; } = 0;
        public int BorderChangeTargetBlue
        {
            get
            {
                return borderChangeTargetBlue;
            }
            set
            {
                borderChangeTargetBlue = value;
                OnPropertyChanged(nameof(borderChangeTargetBlue));
            }
        }
        private int borderChangeTargetGreen { get; set; } = 0;
        public int BorderChangeTargetGreen
        {
            get
            {
                return borderChangeTargetGreen;
            }
            set
            {
                borderChangeTargetGreen = value;
                OnPropertyChanged(nameof(borderChangeTargetGreen));
            }
        }
        private int borderChangeTargetOrange { get; set; } = 0;
        public int BorderChangeTargetOrange
        {
            get
            {
                return borderChangeTargetOrange;
            }
            set
            {
                borderChangeTargetOrange = value;
                OnPropertyChanged(nameof(borderChangeTargetOrange));
            }
        }
        private int borderChangeTargetPurple { get; set; } = 0;
        public int BorderChangeTargetPurple
        {
            get
            {
                return borderChangeTargetPurple;
            }
            set
            {
                borderChangeTargetPurple = value;
                OnPropertyChanged(nameof(borderChangeTargetPurple));
            }
        }
        private int borderChangeTargetRed { get; set; } = 0;
        public int BorderChangeTargetRed
        {
            get
            {
                return borderChangeTargetRed;
            }
            set
            {
                borderChangeTargetRed = value;
                OnPropertyChanged(nameof(borderChangeTargetRed));
            }
        }
        private int borderChangeTargetYellow { get; set; } = 0;
        public int BorderChangeTargetYellow
        {
            get
            {
                return borderChangeTargetYellow;
            }
            set
            {
                borderChangeTargetYellow = value;
                OnPropertyChanged(nameof(borderChangeTargetYellow));
            }
        }
        #endregion
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
            ApplyLanguageChange = "en-US";
            BorderChangeLanguageHide();
            BorderChangeLanguageEnglish = 2;
            IsEnabledApplyButton = true;
        }
        private void ChangeUkrainianLanguage(object sender)
        {
            ApplyLanguageChange = "uk-UA";
            BorderChangeLanguageHide();
            BorderChangeLanguageUkrainian = 2;
            IsEnabledApplyButton = true;
        }
        private void ChangeSpanishLanguage(object sender)
        {
            ApplyLanguageChange = "es-ES";
            BorderChangeLanguageHide();
            BorderChangeLanguageSpanish = 2;
            IsEnabledApplyButton = true;
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
        private void BorderChangeLanguageHide()
        {
            BorderChangeLanguageEnglish = 0;    
            BorderChangeLanguageUkrainian = 0;    
            BorderChangeLanguageSpanish = 0;    
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
        #region TargetPathChange
        private void TargetPathChangeAqua(object sender)
        {
            TargetPathChange = "../Targets/Aqua-Target.png";
            HideAllBorderTarget();
            BorderChangeTargetAqua = 2;
            isEnabledApplyButton = true;
        }
        private void TargetPathChangeBlack(object sender)
        {
            TargetPathChange = "../Targets/Black-Target.png";
            HideAllBorderTarget();
            BorderChangeTargetBlack = 2;
            IsEnabledApplyButton = true;
        }
        private void TargetPathChangeBlue (object sender)
        {
            TargetPathChange = "../Targets/Blue-Target.png";
            HideAllBorderTarget();
            BorderChangeTargetBlue = 2;
            IsEnabledApplyButton = true;
        }
        private void TargetPathChangeGreen(object sender)
        {
            TargetPathChange = "../Targets/Green-Target.png";
            HideAllBorderTarget();
            BorderChangeTargetGreen = 2;
            IsEnabledApplyButton = true;
        }
        private void TargetPathChangeOrange(object sender)
        {
            TargetPathChange = "../Targets/Orange-Target.png";
            HideAllBorderTarget();
            BorderChangeTargetOrange = 2;
            IsEnabledApplyButton = true;
        }
        private void TargetPathChangePurple(object sender)
        {
            TargetPathChange = "../Targets/Purple-Target.png";
            HideAllBorderTarget();
            BorderChangeTargetPurple = 2;
            IsEnabledApplyButton = true;
        }
        private void TargetPathChangeRed(object sender)
        {
            TargetPathChange = "../Targets/Red-Target.png";
            HideAllBorderTarget();
            BorderChangeTargetRed = 2;
            IsEnabledApplyButton = true;
        }
        private void TargetPathChangeYellow(object sender)
        {
            TargetPathChange = "../Targets/Yellow-Target.png";
            HideAllBorderTarget();
            BorderChangeTargetYellow = 2;
            IsEnabledApplyButton = true;
        }
        private void ApplyTargetPathChange(string Path)
        {
            settingsProperty.TargetPath = Path;
        }
        private void HideAllBorderTarget()
        {
            BorderChangeTargetAqua = 0;
            BorderChangeTargetBlack = 0;
            BorderChangeTargetBlue = 0;
            BorderChangeTargetGreen = 0;
            BorderChangeTargetPurple = 0;
            BorderChangeTargetOrange = 0;
            BorderChangeTargetRed = 0;
            BorderChangeTargetYellow = 0;
        }
        #endregion
        #region ApplySettingsChange
        private void ApplySettingsChange(object sender)
        {
            if(ApplyLanguageChange!=null)
                Changelanguage(ApplyLanguageChange);
            BorderChangeLanguageHide();
            if(TargetPathChange!=null)
                ApplyTargetPathChange(TargetPathChange); 
            HideAllBorderTarget();
            IsEnabledApplyButton = false;
        }
        #endregion
    }

}
