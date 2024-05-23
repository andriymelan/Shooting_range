using System;
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
using System.Windows.Threading;

namespace Shooting_range.ViewModels
{
    public class PlayGameViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public PlayGameViewModel()
        {
            FirstTargetHitCommand = new RelayCommand(FirstTargetHit);
            SecondTargetHitCommand = new RelayCommand(SecindTargetHit);
            ThirdTargetHitCommand = new RelayCommand(ThirdTargetHit);
            CountAllLeftMouseClickCommand = new RelayCommand(CountAllLeftMouseClickMethod);
            GamePauseCommand = new RelayCommand(esc);
            StartLocationTarget();
            MusicInitialize();
            InitializeTimer();
        }


        public RelayCommand FirstTargetHitCommand { get; set; }
        public RelayCommand SecondTargetHitCommand { get; set; }
        public RelayCommand ThirdTargetHitCommand { get; set; }
        public RelayCommand CountAllLeftMouseClickCommand {  get; set; }
        public RelayCommand GamePauseCommand {  get; set; }

        private DispatcherTimer GameTimer = new DispatcherTimer();

        public string Crosshair { get; } = SettingsPropertyModel.CrosshairPath;

        public string Target { get; } = SettingsPropertyModel.TargetPath;

        private int canvasTopFirstTarget { get; set; } = 0;
        public int CanvasTopFirstTarget
        {
            get
            {
                return canvasTopFirstTarget;
            }
            set
            {
                canvasTopFirstTarget = value;
                OnPropertyChanged(nameof(canvasTopFirstTarget));
            }
        }

        private int canvasLeftFirstTarget { get; set; } = 0;
        public int CanvasLeftFirstTarget
        {
            get
            {
                return canvasLeftFirstTarget;
            }
            set
            {
                canvasLeftFirstTarget = value;
                OnPropertyChanged(nameof(canvasLeftFirstTarget));
            }
        }

        private int canvasTopSecondTarget { get; set; } = 0;
        public int CanvasTopSecondTarget
        {
            get
            {
                return canvasTopSecondTarget;
            }
            set
            {
                canvasTopSecondTarget = value;
                OnPropertyChanged(nameof(canvasTopSecondTarget));
            }
        }

        private int canvasLeftSecondTarget { get; set; } = 0;
        public int CanvasLeftSecondTarget
        {
            get { return canvasLeftSecondTarget; }
            set
            {
                canvasLeftSecondTarget = value;
                OnPropertyChanged(nameof(canvasLeftSecondTarget));
            }
        }

        private int canvasTopThirdTarget { get; set; } = 0;
        public int CanvasTopThirdTarget
        {
            get { return canvasTopThirdTarget; }
            set
            {
                canvasTopThirdTarget = value;
                OnPropertyChanged(nameof(canvasTopThirdTarget));
            }
        }

        private int canvasLeftThirdTarget { get; set; } = 0;
        public int CanvasLeftThirdTarget
        {
            get { return canvasLeftThirdTarget; }
            set
            {
                canvasLeftThirdTarget = value;
                OnPropertyChanged(nameof(canvasLeftThirdTarget));
            }
        }

        private int countAllLeftMouseClick { get; set; } = 0;
        public int CountAllLeftMouseClick
        {
            get { return countAllLeftMouseClick; }
            set
            {
                countAllLeftMouseClick = value;
                OnPropertyChanged(nameof(countAllLeftMouseClick));
            }
        }

        private int timerTime { get; set; } = 10;
        public int TimerTime
        {
            get { return timerTime; }
            set
            {
                timerTime = value;
                OnPropertyChanged(nameof(timerTime));
            }
        }

        private void FirstTargetHit(object sender)
        {
            TargetHitSoundInitialize();
            ChangeLocationTarget(0);
            CountAllLeftMouseClick += 5;
            while ((CanvasTopFirstTarget == CanvasTopSecondTarget && CanvasLeftFirstTarget == CanvasLeftSecondTarget) || (CanvasTopFirstTarget == CanvasTopThirdTarget && CanvasLeftFirstTarget == CanvasLeftThirdTarget))
                ChangeLocationTarget(0);
        }

        private void SecindTargetHit(object sender)
        {
            TargetHitSoundInitialize();
            ChangeLocationTarget(1);
            CountAllLeftMouseClick += 5;
            while ((CanvasTopSecondTarget == CanvasTopFirstTarget && CanvasLeftSecondTarget == CanvasLeftFirstTarget) || (CanvasTopSecondTarget == CanvasTopThirdTarget && CanvasLeftSecondTarget == CanvasLeftThirdTarget))
                ChangeLocationTarget(1);
        }
        private void ThirdTargetHit(object sender)
        {
            TargetHitSoundInitialize();
            ChangeLocationTarget(2);
            CountAllLeftMouseClick += 5;
            while ((CanvasTopThirdTarget == CanvasTopSecondTarget && CanvasLeftThirdTarget == CanvasLeftSecondTarget) || (CanvasTopThirdTarget == CanvasTopFirstTarget && CanvasLeftThirdTarget == CanvasLeftFirstTarget))
                ChangeLocationTarget(2);
        }
        
        private void StartLocationTarget()
        {
            Random randomStartLocation = new Random();
            CanvasTopFirstTarget = (randomStartLocation.Next(1001) % 6) * 100;
            CanvasTopSecondTarget = (randomStartLocation.Next(1001) * randomStartLocation.Next(1001) % 6) * 100;
            CanvasTopThirdTarget = (randomStartLocation.Next(1001) * randomStartLocation.Next(1001) * randomStartLocation.Next(1001) % 6) * 100;
            CanvasLeftFirstTarget = (randomStartLocation.Next(1001) % 13) * 100;
            CanvasLeftSecondTarget = (randomStartLocation.Next(1001) * randomStartLocation.Next(1001) % 13) * 100;
            CanvasLeftThirdTarget = (randomStartLocation.Next(1001) * randomStartLocation.Next(1001) * randomStartLocation.Next(1001) % 13) * 100;
            while ((CanvasTopThirdTarget == CanvasTopSecondTarget && CanvasLeftThirdTarget == CanvasLeftSecondTarget) || 
                (CanvasTopThirdTarget == CanvasTopFirstTarget && CanvasLeftThirdTarget == CanvasLeftFirstTarget) || 
                (CanvasTopFirstTarget == CanvasTopSecondTarget && CanvasLeftFirstTarget == CanvasLeftSecondTarget))
            {
                ChangeLocationTarget(0);
                ChangeLocationTarget(1);
                ChangeLocationTarget(2);
            }
        }
        private void ChangeLocationTarget(int targetNumber)
        {
            Random CanvasRandom = new Random();
            switch (targetNumber)
            {
                case 0:
                    CanvasTopFirstTarget = (CanvasRandom.Next(1001) % 6) * 100;
                    CanvasLeftFirstTarget = (CanvasRandom.Next(1001) % 13) * 100;        
                    break;
                case 1:
                    CanvasTopSecondTarget = (CanvasRandom.Next(1001) % 6) * 100;
                    CanvasLeftSecondTarget = (CanvasRandom.Next(1001) % 13) * 100;
                    break;
                case 2:
                    CanvasTopThirdTarget = (CanvasRandom.Next(1001) % 6) * 100;
                    CanvasLeftThirdTarget = (CanvasRandom.Next(1001) % 13) * 100;
                    break;
                default: 
                    break;
            }
        }

        private void esc(object sender)
        {
            CountAllLeftMouseClick += 100;
        }

        private void CountAllLeftMouseClickMethod(object sender)
        {
            CountAllLeftMouseClick++;
        }

        private Visibility afterGameStats { get; set; } = Visibility.Collapsed;
        public Visibility AfterGameStats
        {
            get { return afterGameStats; }
            set
            {
                afterGameStats = value;
                OnPropertyChanged(nameof(afterGameStats));
            }
        }

        private void InitializeTimer()
        {
            GameTimer.Interval = TimeSpan.FromSeconds(1);
            GameTimer.Tick += GameTimerTicker;
            GameTimer.Start();
        }

        private void GameTimerTicker(object sender, EventArgs e)
        {
            if(TimerTime!=0)
                TimerTime--;
            else
            {
                GameTimer.Stop();
                IsEnablePlayGrid = false;
                AfterGameStats = Visibility.Visible;
            }
        }

        private bool isEnablePlayGrid {  get; set; } = true;
        public bool IsEnablePlayGrid
        {
            get { return isEnablePlayGrid; }
            set
            {
                isEnablePlayGrid = value;
                OnPropertyChanged(nameof(isEnablePlayGrid));
            }
        }

        #region MusicAndSound
        MediaPlayer TargetHitSound;
        MediaPlayer PlayGameMusic;

        static string FilePath = Assembly.GetExecutingAssembly().Location;
        static string DirectionPath = System.IO.Path.GetDirectoryName(FilePath);
        static string TargetHitPath = System.IO.Path.Combine(DirectionPath, "Sounds/SoundEffects/TargetHit.wav");
        static string PlayGameMusicPath = System.IO.Path.Combine(DirectionPath, "Sounds/Music/PlayGameMusic.wav");

        private void TargetHitSoundInitialize()
        {
            TargetHitSound = new MediaPlayer();
            TargetHitSound.Open(new Uri(TargetHitPath));
            TargetHitSound.Volume = (SettingsPropertyModel.SoundVolume) / 100;
            TargetHitSound.Play();
        }
        private void MusicInitialize()
        {
            PlayGameMusic = new MediaPlayer();
            PlayGameMusic.MediaEnded += MusicEnd;
            PlayGameMusic.Open(new Uri(PlayGameMusicPath));
            PlayGameMusic.Volume = (SettingsPropertyModel.MusicVolume) / 100;
            PlayGameMusic.Play();
        }
        private void MusicEnd(object sender, EventArgs e)
        {
            PlayGameMusic.Position = TimeSpan.MinValue;
        }
        #endregion
    }
}
