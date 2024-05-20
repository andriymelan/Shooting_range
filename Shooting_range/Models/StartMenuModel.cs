using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Shooting_range.Models
{
    public class StartMenuModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #region VisibilityStartMenu
        private Visibility initialVisibility { get; set; } = Visibility.Visible;  
        public Visibility InitialVisibility
        {
            get { return initialVisibility; }
            set
            {
                initialVisibility = value;
                OnPropertyChanged(nameof(initialVisibility));
            }
        }
        private Visibility settingsVisibility { get; set; } = Visibility.Collapsed;
        public Visibility SettingsVisibility
        {
            get { return settingsVisibility; }
            set
            {
                settingsVisibility = value;
                OnPropertyChanged(nameof(settingsVisibility));
            }
        }
        private Visibility sureExitVisibility { get; set; } = Visibility.Collapsed;
        public Visibility SureExitVisibility
        {
            get { return sureExitVisibility; }
            set
            {
                sureExitVisibility = value;
                OnPropertyChanged(nameof(sureExitVisibility));
            }
        }
        private Visibility playVisibility { get; set; } = Visibility.Collapsed;
        public Visibility PlayVisibility
        {
            get { return playVisibility; }
            set
            {
                playVisibility = value;
                OnPropertyChanged(nameof(playVisibility));
            }
        }
        #endregion

        #region VisibilitySettings
        private Visibility soundSettings {  get; set; } = Visibility.Collapsed;
        public Visibility SoundSettings
        {
            get { return soundSettings; }
            set
            {
                soundSettings = value;
                OnPropertyChanged(nameof(soundSettings));
            }
        }
        private Visibility customizeSettings { get; set; } = Visibility.Collapsed;
        public Visibility CustomizeSettings
        {
            get { return customizeSettings; }
            set
            {
                customizeSettings = value;
                OnPropertyChanged(nameof(customizeSettings));
            }
        }
        private Visibility languageSettings { get; set; } = Visibility.Collapsed;
        public Visibility LanguageSettings
        {
            get { return languageSettings; }
            set
            {
                languageSettings = value;
                OnPropertyChanged(nameof(languageSettings));
            }
        }
        #endregion

        #region VisibilityPlay
        private Visibility gameModeVisibility {  get; set; } = Visibility.Visible;
        public Visibility GameModeVisibility
        {
            get { return gameModeVisibility; }
            set
            {
                gameModeVisibility = value;
                OnPropertyChanged(nameof(gameModeVisibility));
            }
        }
        private Visibility gridShotVisibility {  get; set; } = Visibility.Collapsed;
        public Visibility GridShotVisibility
        {
            get { return gridShotVisibility; }
            set
            {
                gridShotVisibility = value;
                OnPropertyChanged(nameof(gridShotVisibility));
            }
        }
        private Visibility spiderShotVisibility {  get; set; } = Visibility.Collapsed;
        public Visibility SpiderShotVisibility
        {
            get { return spiderShotVisibility; }
            set
            {
                spiderShotVisibility = value;
                OnPropertyChanged(nameof(spiderShotVisibility));
            }
        }
        private Visibility motionShotVisibility {  get; set; } = Visibility.Collapsed;
        public Visibility MotionShotVisibility
        {
            get { return motionShotVisibility; }
            set
            {
                motionShotVisibility = value;
                OnPropertyChanged(nameof(motionShotVisibility));
            }
        }
        private Visibility motionComplexityVisibility { get; set; } = Visibility.Collapsed;
        public Visibility MotionComplexityVisibility
        {
            get { return motionComplexityVisibility; }
            set
            {
                motionComplexityVisibility = value;
                OnPropertyChanged(nameof(motionComplexityVisibility));
            }
        }
        #endregion
    }
}
