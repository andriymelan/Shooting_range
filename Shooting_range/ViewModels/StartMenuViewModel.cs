﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Shooting_range.Views;
using Shooting_range.Models;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Reflection;
using System.Windows.Media;

namespace Shooting_range.ViewModels
{
    public class StartMenuViewModel : INotifyPropertyChanged
    {
        #region InitializeDefauleConstructorAndINotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        static string cursorDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Cursor";

        public StartMenuViewModel()
        {
            StartMenuVisibility = new StartMenuModel();
            OpenSettingsCommand = new RelayCommand(OpenSettings);
            OpenRecordsCommand = new RelayCommand(OpenRecords);
            OpenSureExitCommand = new RelayCommand(OpenSureExit);
            BackToMenuCommand = new RelayCommand(BackToMenu);
            CloseAppCommand = new RelayCommand(CloseApp);
            OpenPlayCommand = new RelayCommand(OpenPlay);
            cursor = ($@"{cursorDirectory}\\MainCursor.cur");

            ChangeEnglishLanguageCommand = new RelayCommand(ChangeEnglishLanguage);
            ChangeUkrainianLanguageCommand = new RelayCommand(ChangeUkrainianLanguage);
            ChangeSpanishLanguageCommand = new RelayCommand(ChangeSpanishLanguage);

            OpenSoundSettingsCommand = new RelayCommand(OpenSoundSettings);
            OpenCustomizeSettingsCommand = new RelayCommand(OpenCustomizeSettings);
            OpenLanguageSettingsCommand = new RelayCommand(OpenLanguageSettings);

            SetDefaultSettingsCommand = new RelayCommand(SetDefaultSettings);

            ApplySettingsChangeCommand = new RelayCommand(ApplySettingsChange);

            TargetPathChangeAquaCommand = new RelayCommand(TargetPathChangeAqua);
            TargetPathChangeBlackCommand = new RelayCommand(TargetPathChangeBlack);
            TargetPathChangeBlueCommand = new RelayCommand(TargetPathChangeBlue);
            TargetPathChangeGreenCommand = new RelayCommand(TargetPathChangeGreen);
            TargetPathChangeOrangeCommand = new RelayCommand(TargetPathChangeOrange);
            TargetPathChangePurpleCommand = new RelayCommand(TargetPathChangePurple);
            TargetPathChangeRedCommand = new RelayCommand(TargetPathChangeRed);
            TargetPathChangeYellowCommand = new RelayCommand(TargetPathChangeYellow);

            CrosshairPathChangeFineAquaCommand = new RelayCommand(CrosshairPathChangeFineAqua);
            CrosshairPathChangeFineBlackCommand = new RelayCommand(CrosshairPathChangeFineBlack);
            CrosshairPathChangeFineBlueCommand = new RelayCommand(CrosshairPathChangeFineBlue);
            CrosshairPathChangeFineGreenCommand = new RelayCommand(CrosshairPathChangeFineGreen);
            CrosshairPathChangeFineOrangeCommand = new RelayCommand(CrosshairPathChangeFineOrange);
            CrosshairPathChangeFinePurpleCommand = new RelayCommand(CrosshairPathChangeFinePurple);
            CrosshairPathChangeFineRedCommand = new RelayCommand(CrosshairPathChangeFineRed);
            CrosshairPathChangeFineYellowCommand = new RelayCommand(CrosshairPathChangeFineYellow);

            CrosshairPathChangeDotAquaCommand = new RelayCommand(CrosshairPathChangeDotAqua);
            CrosshairPathChangeDotBlackCommand = new RelayCommand(CrosshairPathChangeDotBlack);
            CrosshairPathChangeDotBlueCommand = new RelayCommand(CrosshairPathChangeDotBlue);
            CrosshairPathChangeDotGreenCommand = new RelayCommand(CrosshairPathChangeDotGreen);
            CrosshairPathChangeDotOrangeCommand = new RelayCommand(CrosshairPathChangeDotOrange);
            CrosshairPathChangeDotPurpleCommand = new RelayCommand(CrosshairPathChangeDotPurple);
            CrosshairPathChangeDotRedCommand = new RelayCommand(CrosshairPathChangeDotRed);
            CrosshairPathChangeDotYellowCommand = new RelayCommand(CrosshairPathChangeDotYellow);

            ChangeMusicVolumeUpCommand = new RelayCommand(ChangeMusicVolumeUp);
            ChangeMusicVolumeDownCommand = new RelayCommand(ChangeMusicVolumeDown);
            ChangeSoundVolumeUpCommand = new RelayCommand(ChangeSoundVolumeUp);
            ChangeSoundVolumeDownCommand = new RelayCommand(ChangeSoundVolumeDown);

            OpenGridShotCommand = new RelayCommand(OpenGridShot);
            OpenSpyderShotCommand = new RelayCommand(OpenSpyderShot);
            OpenMotionShotComplexityCommand = new RelayCommand(OpenMotionShotComplexity);
            BackToGameModeCommand = new RelayCommand(BackToGameModeChange);

            SetMotionDifficultEasyCommand = new RelayCommand(SetMotionDifficultEasy);
            SetMotionDifficultMediumCommand = new RelayCommand(SetMotionDifficultMedium);
            SetMotionDifficultHardCommand = new RelayCommand(SetMotionDifficultHard);

            SetGameTimer15Command = new RelayCommand(SetGameTimer15);
            SetGameTimer30Command = new RelayCommand(SetGameTimer30);
            SetGameTimer60Command = new RelayCommand(SetGameTimer60);

            OpenPlayGameWindowCommand = new RelayCommand(OpenPlayGameWindow);

            MusicInitialize();
        }
        #endregion

        #region CreateProperty

        public RelayCommand OpenSettingsCommand { get; set; }
        public RelayCommand OpenRecordsCommand {  get; set; }
        public RelayCommand CloseAppCommand { get; set; }
        public RelayCommand OpenSureExitCommand { get; set; }
        public RelayCommand BackToMenuCommand { get; set; }
        public RelayCommand OpenPlayCommand { get; set; }

        public RelayCommand ChangeEnglishLanguageCommand { get; set; }
        public RelayCommand ChangeUkrainianLanguageCommand { get; set; }
        public RelayCommand ChangeSpanishLanguageCommand { get; set; }

        public RelayCommand OpenSoundSettingsCommand { get; set; }
        public RelayCommand OpenCustomizeSettingsCommand { get; set; }
        public RelayCommand OpenLanguageSettingsCommand { get; set; }

        public RelayCommand SetDefaultSettingsCommand { get; set; }

        public RelayCommand ApplySettingsChangeCommand { get; set; }

        public RelayCommand TargetPathChangeAquaCommand { get; set; }
        public RelayCommand TargetPathChangeBlackCommand { get; set; }
        public RelayCommand TargetPathChangeBlueCommand { get; set; }
        public RelayCommand TargetPathChangeGreenCommand { get; set; }
        public RelayCommand TargetPathChangeOrangeCommand { get; set; }
        public RelayCommand TargetPathChangePurpleCommand { get; set; }
        public RelayCommand TargetPathChangeRedCommand { get; set; }
        public RelayCommand TargetPathChangeYellowCommand { get; set; }

        public RelayCommand CrosshairPathChangeFineAquaCommand { get; set; }
        public RelayCommand CrosshairPathChangeFineBlackCommand { get; set; }
        public RelayCommand CrosshairPathChangeFineBlueCommand { get; set; }
        public RelayCommand CrosshairPathChangeFineGreenCommand { get; set; }
        public RelayCommand CrosshairPathChangeFineOrangeCommand { get; set; }
        public RelayCommand CrosshairPathChangeFinePurpleCommand { get; set; }
        public RelayCommand CrosshairPathChangeFineRedCommand { get; set; }
        public RelayCommand CrosshairPathChangeFineYellowCommand { get; set; }

        public RelayCommand CrosshairPathChangeDotAquaCommand { get; set; }
        public RelayCommand CrosshairPathChangeDotBlackCommand { get; set; }
        public RelayCommand CrosshairPathChangeDotBlueCommand { get; set; }
        public RelayCommand CrosshairPathChangeDotGreenCommand { get; set; }
        public RelayCommand CrosshairPathChangeDotOrangeCommand { get; set; }
        public RelayCommand CrosshairPathChangeDotPurpleCommand { get; set; }
        public RelayCommand CrosshairPathChangeDotRedCommand { get; set; }
        public RelayCommand CrosshairPathChangeDotYellowCommand { get; set; }

        public RelayCommand ChangeMusicVolumeUpCommand { get; set; }
        public RelayCommand ChangeMusicVolumeDownCommand { get; set; }
        public RelayCommand ChangeSoundVolumeUpCommand { get; set; }
        public RelayCommand ChangeSoundVolumeDownCommand { get; set; }

        public RelayCommand OpenGridShotCommand { get; set; }
        public RelayCommand OpenSpyderShotCommand { get; set; }
        public RelayCommand OpenMotionShotComplexityCommand { get; set; }
        public RelayCommand OpenMotionShotTimerCommand { get; set; }
        public RelayCommand BackToGameModeCommand { get; set; }

        public RelayCommand SetGameTimer15Command { get; set; }
        public RelayCommand SetGameTimer30Command { get; set; }
        public RelayCommand SetGameTimer60Command { get; set; }

        public RelayCommand SetMotionDifficultEasyCommand { get; set; }
        public RelayCommand SetMotionDifficultMediumCommand { get; set; }
        public RelayCommand SetMotionDifficultHardCommand {  get; set; }

        public RelayCommand OpenPlayGameWindowCommand {  get; set; }

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

        StartMenuModel startMenuVisibility = new StartMenuModel();
        public StartMenuModel StartMenuVisibility
        {
            get { return startMenuVisibility; }
            set
            {
                startMenuVisibility = value;
                OnPropertyChanged(nameof(startMenuVisibility));
            }
        }

        HighScoresModel highscores = new HighScoresModel();
        public HighScoresModel HighScores
        {
            get { return highscores; }
            set
            {
                highscores = value;
                OnPropertyChanged(nameof(highscores));
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
        private string CrosshairPathChange {  get; set; }
        private double musicVolume { get; set; } = SettingsPropertyModel.MusicVolume;
        public double MusicVolume
        {
            get { return musicVolume; }
            set
            {
                musicVolume = value;
                OnPropertyChanged(nameof(musicVolume));
            }
        }
        private double soundVolume { get; set; } = SettingsPropertyModel.SoundVolume;
        public double SoundVolume
        {
            get { return soundVolume; }
            set
            {
                soundVolume = value;
                OnPropertyChanged(nameof(soundVolume));
            }
        }

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
        private int borderChangeFineCrosshairAqua { get; set; } = 0;
        public int BorderChangeFineCrosshairAqua
        {
            get
            {
                return borderChangeFineCrosshairAqua;
            }
            set
            {
                borderChangeFineCrosshairAqua = value;
                OnPropertyChanged(nameof(borderChangeFineCrosshairAqua));
            }
        }
        private int borderChangeFineCrosshairBlack { get; set; } = 0;
        public int BorderChangeFineCrosshairBlack
        {
            get
            {
                return borderChangeFineCrosshairBlack;
            }
            set
            {
                borderChangeFineCrosshairBlack = value;
                OnPropertyChanged(nameof(borderChangeFineCrosshairBlack));
            }
        }
        private int borderChangeFineCrosshairBlue { get; set; } = 0;
        public int BorderChangeFineCrosshairBlue
        {
            get
            {
                return borderChangeFineCrosshairBlue;
            }
            set
            {
                borderChangeFineCrosshairBlue = value;
                OnPropertyChanged(nameof(borderChangeFineCrosshairBlue));
            }
        }
        private int borderChangeFineCrosshairGreen { get; set; } = 0;
        public int BorderChangeFineCrosshairGreen
        {
            get
            {
                return borderChangeFineCrosshairGreen;
            }
            set
            {
                borderChangeFineCrosshairGreen = value;
                OnPropertyChanged(nameof(borderChangeFineCrosshairGreen));
            }
        }
        private int borderChangeFineCrosshairOrange { get; set; } = 0;
        public int BorderChangeFineCrosshairOrange
        {
            get
            {
                return borderChangeFineCrosshairOrange;
            }
            set
            {
                borderChangeFineCrosshairOrange = value;
                OnPropertyChanged(nameof(borderChangeFineCrosshairOrange));
            }
        }
        private int borderChangeFineCrosshairPurple { get; set; } = 0;
        public int BorderChangeFineCrosshairPurple
        {
            get
            {
                return borderChangeFineCrosshairPurple;
            }
            set
            {
                borderChangeFineCrosshairPurple = value;
                OnPropertyChanged(nameof(borderChangeFineCrosshairPurple));
            }
        }
        private int borderChangeFineCrosshairRed { get; set; } = 0;
        public int BorderChangeFineCrosshairRed
        {
            get
            {
                return borderChangeFineCrosshairRed;
            }
            set
            {
                borderChangeFineCrosshairRed = value;
                OnPropertyChanged(nameof(borderChangeFineCrosshairRed));
            }
        }
        private int borderChangeFineCrosshairYellow { get; set; } = 0;
        public int BorderChangeFineCrosshairYellow
        {
            get
            {
                return borderChangeFineCrosshairYellow;
            }
            set
            {
                borderChangeFineCrosshairYellow = value;
                OnPropertyChanged(nameof(borderChangeFineCrosshairYellow));
            }
        }
        private int borderChangeDotCrosshairAqua { get; set; } = 0;
        public int BorderChangeDotCrosshairAqua
        {
            get
            {
                return borderChangeDotCrosshairAqua;
            }
            set
            {
                borderChangeDotCrosshairAqua = value;
                OnPropertyChanged(nameof(borderChangeDotCrosshairAqua));
            }
        }
        private int borderChangeDotCrosshairBlack { get; set; } = 0;
        public int BorderChangeDotCrosshairBlack
        {
            get
            {
                return borderChangeDotCrosshairBlack;
            }
            set
            {
                borderChangeDotCrosshairBlack = value;
                OnPropertyChanged(nameof(borderChangeDotCrosshairBlack));
            }
        }
        private int borderChangeDotCrosshairBlue { get; set; } = 0;
        public int BorderChangeDotCrosshairBlue
        {
            get
            {
                return borderChangeDotCrosshairBlue;
            }
            set
            {
                borderChangeDotCrosshairBlue = value;
                OnPropertyChanged(nameof(borderChangeDotCrosshairBlue));
            }
        }
        private int borderChangeDotCrosshairGreen { get; set; } = 0;
        public int BorderChangeDotCrosshairGreen
        {
            get
            {
                return borderChangeDotCrosshairGreen;
            }
            set
            {
                borderChangeDotCrosshairGreen = value;
                OnPropertyChanged(nameof(borderChangeDotCrosshairGreen));
            }
        }
        private int borderChangeDotCrosshairOrange { get; set; } = 0;
        public int BorderChangeDotCrosshairOrange
        {
            get
            {
                return borderChangeDotCrosshairOrange;
            }
            set
            {
                borderChangeDotCrosshairOrange = value;
                OnPropertyChanged(nameof(borderChangeDotCrosshairOrange));
            }
        }
        private int borderChangeDotCrosshairPurple { get; set; } = 0;
        public int BorderChangeDotCrosshairPurple
        {
            get
            {
                return borderChangeDotCrosshairPurple;
            }
            set
            {
                borderChangeDotCrosshairPurple = value;
                OnPropertyChanged(nameof(borderChangeDotCrosshairPurple));
            }
        }
        private int borderChangeDotCrosshairRed { get; set; } = 0;
        public int BorderChangeDotCrosshairRed
        {
            get
            {
                return borderChangeDotCrosshairRed;
            }
            set
            {
                borderChangeDotCrosshairRed = value;
                OnPropertyChanged(nameof(borderChangeDotCrosshairRed));
            }
        }
        private int borderChangeDotCrosshairYellow { get; set; } = 0;
        public int BorderChangeDotCrosshairYellow
        {
            get
            {
                return borderChangeDotCrosshairYellow;
            }
            set
            {
                borderChangeDotCrosshairYellow = value;
                OnPropertyChanged(nameof(borderChangeDotCrosshairYellow));
            }
        }
        #endregion
        #endregion

        #region StartMenuButtons
        private void BackToMenu(object sender)
        {
            ButtonClick();
            OpenSmth("InitialVisibility");
        }


        private void OpenPlay(object sender)
        {
            ButtonClick();
            OpenSmth("PlayVisibility");
        }


        private void OpenSettings(object sender)
        {
            ButtonClick();
            OpenSmth("SettingsVisibility");
        }

        private void OpenRecords(object sender)
        {
            ButtonClick();
            OpenSmth("RecordsVisibility");
        }

        private void OpenSureExit(object sender)
        {
            ButtonClick();
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
                case "RecordsVisibility":
                    StartMenuVisibility.RecordsVisibility = Visibility.Visible;
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
            StartMenuVisibility.RecordsVisibility = Visibility.Collapsed;
        }
        #endregion

        #region SettingsButtons
        private void OpenSoundSettings(object sender)
        {
            ButtonClick();
            OpenSomeSettings("SoundSettings");
        }
        private void OpenCustomizeSettings(object sender)
        {
            ButtonClick();
            OpenSomeSettings("CustomizeSettings");
        }
        private void OpenLanguageSettings(object sender)
        {
            ButtonClick();
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

        #region PlayButtons
        private void OpenGridShot(object sender)
        {
            ButtonClick();
            CloseGameProperties();
            GameModeSettingsModel.TypeGameMode = "GridShot";
            StartMenuVisibility.GridShotVisibility = Visibility.Visible;
            StartMenuVisibility.GameModeVisibility = Visibility.Collapsed;
        }
        private void OpenSpyderShot(object sender)
        {
            ButtonClick();
            CloseGameProperties();
            GameModeSettingsModel.TypeGameMode = "SpyderShot";
            StartMenuVisibility.SpyderShotVisibility = Visibility.Visible;
            StartMenuVisibility.GameModeVisibility = Visibility.Collapsed;
        }
        private void OpenMotionShotComplexity(object sender)
        {
            ButtonClick();
            CloseGameProperties();
            GameModeSettingsModel.TypeGameMode = "MotionShot";
            StartMenuVisibility.MotionComplexityVisibility = Visibility.Visible;
            StartMenuVisibility.GameModeVisibility = Visibility.Collapsed;
        }

        private void SetMotionDifficultEasy(object sender)
        {
            ButtonClick();
            GameModeSettingsModel.DifficultOfGameModeMotionGrid = "Easy";
            OpenMotionShotTimer();
        }
        private void SetMotionDifficultMedium(object sender)
        {
            ButtonClick();
            GameModeSettingsModel.DifficultOfGameModeMotionGrid = "Medium";
            OpenMotionShotTimer();
        }
        private void SetMotionDifficultHard(object sender)
        {
            ButtonClick();
            GameModeSettingsModel.DifficultOfGameModeMotionGrid = "Hard";
            OpenMotionShotTimer();
        }
        private void OpenMotionShotTimer()
        {
            CloseGameProperties();
            StartMenuVisibility.MotionShotVisibility = Visibility.Visible;
            StartMenuVisibility.MotionComplexityVisibility = Visibility.Collapsed;
        }


        private void SetGameTimer15(object sender)
        {
            GameModeSettingsModel.GameTimer = 15;
            OpenPlayGameWindow(sender);
        }

        private void SetGameTimer30(object sender)
        {
            GameModeSettingsModel.GameTimer = 30;
            OpenPlayGameWindow(sender);
        }

        private void SetGameTimer60(object sender)
        {
            GameModeSettingsModel.GameTimer = 60;
            OpenPlayGameWindow(sender);
        }

        private void BackToGameModeChange(object sender)
        {
            ButtonClick();
            CloseGameProperties();
            startMenuVisibility.MotionComplexityVisibility = Visibility.Collapsed;
            startMenuVisibility.GameModeVisibility = Visibility.Visible;
        }
        private void CloseGameProperties()
        {
            StartMenuVisibility.GridShotVisibility = Visibility.Collapsed;
            StartMenuVisibility.SpyderShotVisibility = Visibility.Collapsed;
            StartMenuVisibility.MotionShotVisibility = Visibility.Collapsed;
        }
        #endregion

        #region LanguageChange
        private void ChangeLanguage(object sender, string languageCode)
        {
            ButtonClick();
            ApplyLanguageChange = languageCode;
            BorderChangeLanguageHide();
            IsEnabledApplyButton = true;
        }

        private void ChangeEnglishLanguage(object sender)
        {
            ChangeLanguage(sender, "en-US" );
            BorderChangeLanguageEnglish = 2;
        }

        private void ChangeUkrainianLanguage(object sender)
        {
            ChangeLanguage(sender, "uk-UA");
            BorderChangeLanguageUkrainian = 2;
        }

        private void ChangeSpanishLanguage(object sender)
        {
            ChangeLanguage(sender, "es-ES");
            BorderChangeLanguageSpanish = 2;
        }

        private void ChangeLanguage(string language)
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

        #region TargetPathChange
        private void ApplyTargetPathChange(string Path)
        {
            SettingsPropertyModel.TargetPath = Path;
        }
        private void TargetPathChangeAqua(object sender)
        {
            TargetChoose("Aqua");
        }
        private void TargetPathChangeBlack(object sender)
        {
            TargetChoose("Black");
        }
        private void TargetPathChangeBlue (object sender)
        {
            TargetChoose("Blue");
        }
        private void TargetPathChangeGreen(object sender)
        {
            TargetChoose("Green");
        }
        private void TargetPathChangeOrange(object sender)
        {
            TargetChoose("Orange");
        }
        private void TargetPathChangePurple(object sender)
        {
            TargetChoose("Purple");
        }
        private void TargetPathChangeRed(object sender)
        {
            TargetChoose("Red");
        }
        private void TargetPathChangeYellow(object sender)
        {
            TargetChoose("Yellow");
        }


        private void TargetChoose(string targetColor)
        {
            TargetPathChange = $"../Targets/{targetColor}-Target.png";
            HideAllBorderTarget();
            string propertyName = $"BorderChangeTarget{targetColor}";
            var property = GetType().GetProperty(propertyName);
            property.SetValue(this, 2);
            IsEnabledApplyButton = true;
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

        #region CrosshairPathChange
        private void ApplyCrosshairPathChange(string Path)
        {
            SettingsPropertyModel.CrosshairPath = Path;
        }
        private void CrosshairPathChangeFineAqua(object sender)
        {
            CrosshairPathChange = @"\\Fine\\AquaFineCrosshair.cur";
            HideAllBorderCrosshair();
            BorderChangeFineCrosshairAqua = 2;
            IsEnabledApplyButton = true;
        }
        private void CrosshairPathChangeFineBlack(object sender)
        {
            CrosshairColorChange("Fine", "Black");
        }
        private void CrosshairPathChangeFineBlue(object sender)
        {
            CrosshairColorChange("Fine", "Blue");
        }
        private void CrosshairPathChangeFineGreen(object sender)
        {
            CrosshairColorChange("Fine", "Green");
        }
        private void CrosshairPathChangeFineOrange(object sender)
        {
            CrosshairColorChange("Fine", "Orange");
        }
        private void CrosshairPathChangeFinePurple(object sender)
        {
            CrosshairColorChange("Fine", "Purple");
        }
        private void CrosshairPathChangeFineRed(object sender)
        {
            CrosshairColorChange("Fine", "Red");
        }
        private void CrosshairPathChangeFineYellow(object sender)
        {
            CrosshairColorChange("Fine", "Yellow");
        }
        private void CrosshairPathChangeDotAqua(object sender)
        {
            CrosshairColorChange("Dot", "Aqua");
        }
        private void CrosshairPathChangeDotBlack(object sender)
        {
            CrosshairColorChange("Dot", "Black");
        }
        private void CrosshairPathChangeDotBlue(object sender)
        {
            CrosshairColorChange("Dot", "Blue");
        }
        private void CrosshairPathChangeDotGreen(object sender)
        {
            CrosshairColorChange("Dot", "Green");
        }
        private void CrosshairPathChangeDotOrange(object sender)
        {
            CrosshairColorChange("Dot", "Orange");
        }
        private void CrosshairPathChangeDotPurple(object sender)
        {
            CrosshairColorChange("Dot", "Purple");
        }
        private void CrosshairPathChangeDotRed(object sender)
        {
            CrosshairColorChange("Dot", "Red");
        }
        private void CrosshairPathChangeDotYellow(object sender)
        {
            CrosshairColorChange("Dot", "Yellow");
        }

        private void CrosshairColorChange(string crosshairType, string crosshairColor)
        {
            CrosshairPathChange = $@"\\{crosshairType}\\{crosshairColor}{crosshairType}Crosshair.cur";
            HideAllBorderCrosshair();
            string propertyName = $"BorderChange{crosshairType}Crosshair{crosshairColor}";
            var property = GetType().GetProperty(propertyName);
            property.SetValue(this, 2);
            IsEnabledApplyButton = true;
        }

        private void HideAllBorderCrosshair()
        {
            BorderChangeFineCrosshairAqua = 0;
            BorderChangeFineCrosshairBlack = 0;
            BorderChangeFineCrosshairBlue = 0;
            BorderChangeFineCrosshairGreen = 0;
            BorderChangeFineCrosshairOrange = 0;
            BorderChangeFineCrosshairPurple = 0;
            BorderChangeFineCrosshairRed = 0;
            BorderChangeFineCrosshairYellow = 0;

            BorderChangeDotCrosshairYellow = 0;
            BorderChangeDotCrosshairAqua = 0;
            BorderChangeDotCrosshairBlack = 0;
            BorderChangeDotCrosshairBlue = 0;
            BorderChangeDotCrosshairGreen = 0;
            BorderChangeDotCrosshairOrange = 0;
            BorderChangeDotCrosshairPurple = 0;
            BorderChangeDotCrosshairRed = 0;
            BorderChangeDotCrosshairYellow = 0;
        }
        #endregion

        #region ChangeVolume
        private void ChangeMusicVolumeUp(object sender)
        {
            if (MusicVolume != 100)
            {
                ButtonClick();
                MusicVolume += 5;
                IsEnabledApplyButton = true;
            }
        }
        private void ChangeMusicVolumeDown(object sender)
        {
            if (MusicVolume != 0)
            {
                ButtonClick();
                MusicVolume -= 5;
                IsEnabledApplyButton = true;
            }
        }
        private void ChangeSoundVolumeUp(object sender)
        {
            if (SoundVolume != 100)
            {
                ButtonClick();
                SoundVolume += 5;
                IsEnabledApplyButton = true;
            }
        }



        private void ChangeSoundVolumeDown(object sender)
        {
            if (SoundVolume != 0)
            {
                ButtonClick();
                SoundVolume -= 5;
                IsEnabledApplyButton = true;
            }
        }
        private void ApplyMusicVolumeChange(double volume)
        {
            SettingsPropertyModel.MusicVolume = volume;
        }
        private void ApplySoundVolumeChange(double volume)
        {
            SettingsPropertyModel.SoundVolume = volume;
        }
        #endregion

        #region MusicAndSound
        MediaPlayer ButtonClickSound; 
        MediaPlayer MainMenuMusic; 

        static string FilePath = Assembly.GetExecutingAssembly().Location;
        static string DirectionPath = System.IO.Path.GetDirectoryName(FilePath);
        static string ButtonClickPath = System.IO.Path.Combine(DirectionPath, "Sounds/SoundEffects/ButtonClick.wav");
        static string MainMenuMusicPath = System.IO.Path.Combine(DirectionPath, "Sounds/Music/MainMenuMusic.wav");

        private void ButtonClick()
        {
            ButtonClickSound = new MediaPlayer();
            ButtonClickSound.Open(new Uri(ButtonClickPath));
            ButtonClickSound.Volume = (SettingsPropertyModel.SoundVolume)/100;
            ButtonClickSound.Play();
        }
        private void MusicInitialize()
        {
            MainMenuMusic = new MediaPlayer();
            MainMenuMusic.MediaEnded += MusicEnd;
            MainMenuMusic.Open(new Uri(MainMenuMusicPath));
            MainMenuMusic.Volume = (SettingsPropertyModel.MusicVolume) / 100;
            MainMenuMusic.Play();
        }
        private void MusicEnd(object sender, EventArgs e)
        {
            MainMenuMusic.Position = TimeSpan.MinValue;
        }
        #endregion

        #region ApplySettingsChanges
        private void ApplySettingsChange(object sender)
        {
            ButtonClick();
            if (ApplyLanguageChange != null)
                ChangeLanguage(ApplyLanguageChange);
            BorderChangeLanguageHide();
            if (TargetPathChange != null)
                ApplyTargetPathChange(TargetPathChange);
            if (CrosshairPathChange != null)
                ApplyCrosshairPathChange(CrosshairPathChange);
            ApplyMusicVolumeChange(MusicVolume);
            ApplySoundVolumeChange(SoundVolume);
            MainMenuMusic.Volume = (SettingsPropertyModel.MusicVolume) / 100;
            ButtonClickSound.Volume = (SettingsPropertyModel.SoundVolume) / 100;
            HideAllBorderTarget();
            HideAllBorderCrosshair();
            IsEnabledApplyButton = false;
        }
        private void SetDefaultSettings(object sender)
        {
            ApplyLanguageChange = null;
            TargetPathChange = null;
            CrosshairPathChange = null;

            HideAllBorderTarget();
            HideAllBorderCrosshair();
            BorderChangeLanguageHide();

            ChangeLanguage("en-US");
            ApplyTargetPathChange("../Targets/Aqua-Target.png");
            ApplyCrosshairPathChange(@"\\Fine\\AquaFineCrosshair.cur");
            MusicVolume = 30;
            ApplyMusicVolumeChange(MusicVolume);
            MainMenuMusic.Volume = (SettingsPropertyModel.MusicVolume) / 100;
            SoundVolume = 30;
            ApplySoundVolumeChange(SoundVolume);
            ButtonClickSound.Volume = (SettingsPropertyModel.SoundVolume) / 100;
            IsEnabledApplyButton = false;
        }
        #endregion

        #region OpenPlayGameWindow
        private void OpenPlayGameWindow (object sender)
        {
            MainMenuMusic.Stop();
            var PlayGameWindow = new PlayGameView();
            var StartMenuWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x=>x.IsActive);
            PlayGameWindow.Show();
            StartMenuWindow.Close();
        }
        #endregion
    }
}
