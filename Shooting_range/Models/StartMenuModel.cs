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
    }
}
