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
    public class PlayGameModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private bool isEnablePlayGrid { get; set; } = false;
        public bool IsEnablePlayGrid
        {
            get { return isEnablePlayGrid; }
            set
            {
                isEnablePlayGrid = value;
                OnPropertyChanged(nameof(isEnablePlayGrid));
            }
        }


        private Visibility afterGameStatsVisibility { get; set; } = Visibility.Collapsed;
        public Visibility AfterGameStatsVisibility
        {
            get { return afterGameStatsVisibility; }
            set
            {
                afterGameStatsVisibility = value;
                OnPropertyChanged(nameof(afterGameStatsVisibility));
            }
        }

        private Visibility playGamePauseVisibility {  get; set; } = Visibility.Collapsed;
        public Visibility PlayGamePauseVisibility
        {
            get { return playGamePauseVisibility; }
            set
            {
                playGamePauseVisibility = value;
                OnPropertyChanged(nameof(playGamePauseVisibility));
            }
        }

        private Visibility beforeGameTimerVisibility {  get; set; } = Visibility.Visible;
        public Visibility BeforeGameTimerVisibility
        {
            get { return beforeGameTimerVisibility; }
            set
            {
                beforeGameTimerVisibility = value;
                OnPropertyChanged(nameof(beforeGameTimerVisibility));
            }
        }


        private Visibility afterGameAdditionalStatsVisibility { get; set; } = Visibility.Collapsed;
        public Visibility AfterGameAdditionalStatsVisibility
        {
            get { return afterGameAdditionalStatsVisibility; }
            set
            {
                afterGameAdditionalStatsVisibility = value;
                OnPropertyChanged(nameof(afterGameAdditionalStatsVisibility));
            }
        }

        private Visibility isHighscore { get; set; } = Visibility.Collapsed;
        public Visibility IsHighscore
        {
            get { return isHighscore; }
            set
            {
                isHighscore = value;
                OnPropertyChanged(nameof(isHighscore));
            }
        }


        private Visibility firstTargetVisibility {  get; set; } = Visibility.Visible;
        public Visibility FirstTargetVisibility
        {
            get { return firstTargetVisibility; }
            set
            {
                firstTargetVisibility = value;
                OnPropertyChanged(nameof(firstTargetVisibility));
            }
        }

        private Visibility secondTargetVisibility {  get; set; } = Visibility.Visible;
        public Visibility SecondTargetVisibility { 
            get { return secondTargetVisibility; }
            set 
            {
                secondTargetVisibility = value;
                OnPropertyChanged(nameof(secondTargetVisibility));
            } 
        }

        private Visibility thirdTargetVisibility { get; set; } = Visibility.Visible;
        public Visibility ThirdTargetVisibility
        {
            get { return thirdTargetVisibility; }
            set
            {
                thirdTargetVisibility = value;
                OnPropertyChanged(nameof(thirdTargetVisibility));
            }
        }
    }
}
