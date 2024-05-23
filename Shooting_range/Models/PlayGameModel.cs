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

        private bool isEnablePlayGrid { get; set; } = true;
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
    }
}
