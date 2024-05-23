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
            SetGameMode();
            CountMissClickCommand = new RelayCommand(CountMissClick);
            GamePauseCommand = new RelayCommand(GamePause);
            BackToMenuCommand = new RelayCommand(BackToMenu);
            RestartGameCommand = new RelayCommand(RestartGame);
            ResumePlayGameCommand = new RelayCommand(ResumePlayGame);
            OpenAdditionalStatsCommand = new RelayCommand(OpenAdditionalStats);
            CloseAdditionalStatsCommand = new RelayCommand(CloseAdditionalStats);
            StartLocationTarget();
            MusicInitialize();
            InitializeTimer();
        }


        public RelayCommand FirstTargetHitCommand { get; set; }
        public RelayCommand SecondTargetHitCommand { get; set; }
        public RelayCommand ThirdTargetHitCommand { get; set; }
        public RelayCommand CountMissClickCommand { get; set; }
        public RelayCommand GamePauseCommand { get; set; }
        public RelayCommand BackToMenuCommand { get; set; }
        public RelayCommand RestartGameCommand { get; set; }
        public RelayCommand ResumePlayGameCommand { get; set; }
        public RelayCommand OpenAdditionalStatsCommand { get; set; }
        public RelayCommand CloseAdditionalStatsCommand { get; set; }

        private DispatcherTimer GameTimer = new DispatcherTimer();

        public string Crosshair { get; } = SettingsPropertyModel.CrosshairPath;

        public string Target { get; } = SettingsPropertyModel.TargetPath;

        PlayGameModel playGameVisibility = new PlayGameModel();

        public PlayGameModel PlayGameVisibility
        {
            get
            {
                return playGameVisibility;
            }
            set
            {
                playGameVisibility = value;
                OnPropertyChanged(nameof(playGameVisibility));
            }
        }

        HighScoresModel highscoresModel = new HighScoresModel();

        public HighScoresModel HighscoresModel
        {
            get
            {
                return highscoresModel;
            }
            set
            {
                highscoresModel = value;
                OnPropertyChanged(nameof(highscoresModel));
            }
        }

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

        private int gameScore { get; set; } = 0;
        public int GameScore
        {
            get { return gameScore; }
            set
            {
                gameScore = value;
                if (gameScore < 0)
                    GameScore = 0;
                OnPropertyChanged(nameof(gameScore));
            }
        }

        private int gameScoreProgress { get; set; } = 200;
        public int GameScoreProgress
        {
            get { return gameScoreProgress; }
            set
            {
                gameScoreProgress = value;
                OnPropertyChanged(nameof(gameScoreProgress));
            }
        }

        private int countHitInRow { get; set; } = 0;
        public int CountHitInRow
        {
            get { return countHitInRow; }
            set
            {
                countHitInRow = value;
                OnPropertyChanged(nameof(countHitInRow));
            }
        }

        private int countMissInRow { get; set; } = 1;
        public int CountMissInRow
        {
            get { return countMissInRow; }
            set
            {
                countMissInRow = value;
                OnPropertyChanged(nameof(countMissInRow));
            }
        }

        private int countAllMissClick { get; set; } = 0;
        public int CountAllMissClick
        {
            get { return countAllMissClick; }
            set
            {
                countAllMissClick = value;
                OnPropertyChanged(nameof(countAllMissClick));
            }
        }

        private int countAllHitTarget { get; set; } = 0;
        public int CountAllHitTarget
        {
            get { return countAllHitTarget; }
            set
            {
                countAllHitTarget = value;
                OnPropertyChanged(nameof(countAllHitTarget));
            }
        }

        private int totalShots {  get; set; } = 0;
        public int TotalShots
        {
            get { return totalShots; }
            set
            {
                totalShots = value;
                OnPropertyChanged(nameof(totalShots));
            }
        }

        private int timerTime { get; set; } = GameModeSettingsModel.GameTimer;
        public int TimerTime
        {
            get { return timerTime; }
            set
            {
                timerTime = value;
                OnPropertyChanged(nameof(timerTime));
            }
        }

        public int Highscore {  get; set; }
        private void SetGameMode()
        {
            switch (GameModeSettingsModel.TypeGameMode)
            {
                case "GridShot":
                    FirstTargetHitCommand = new RelayCommand(FirstTargetHit);
                    SecondTargetHitCommand = new RelayCommand(SecondTargetHit);
                    ThirdTargetHitCommand = new RelayCommand(ThirdTargetHit);
                    switch (GameModeSettingsModel.GameTimer)
                    {
                        case 15:
                            Highscore = HighScoresModel.GridShot15SecHighscore;
                            break;
                        case 30:
                            Highscore = HighScoresModel.GridShot30SecHighscore;
                            break;
                        case 60:
                            Highscore = HighScoresModel.GridShot60SecHighscore;
                            break;
                        default:
                            break;
                    }
                    break;
                case "SpyderShot":
                    break;
                case "MotionShot":
                    break;
                default:
                    break;
            }
        }

        private void FirstTargetHit(object sender)
        {
            TargetHitSoundInitialize();
            ChangeLocationTarget(0);
            GameScore += GameScoreProgress;
            CountHitInRow++;
            CountMissInRow = 1;
            CountAllHitTarget++;
            TotalShots++;
            if (CountHitInRow % 3 == 0)
            {
                GameScoreProgress += 20;
                CountHitInRow = 0;
            }
            while ((CanvasTopFirstTarget == CanvasTopSecondTarget && CanvasLeftFirstTarget == CanvasLeftSecondTarget)
                || (CanvasTopFirstTarget == CanvasTopThirdTarget && CanvasLeftFirstTarget == CanvasLeftThirdTarget))
                ChangeLocationTarget(0);
        }

        private void SecondTargetHit(object sender)
        {
            TargetHitSoundInitialize();
            ChangeLocationTarget(1);
            GameScore += GameScoreProgress;
            CountHitInRow++;
            CountAllHitTarget++;
            TotalShots++;
            CountMissInRow = 1;
            if (CountHitInRow % 3 == 0)
            {
                GameScoreProgress += 20;
                CountHitInRow = 0;
            }
            while ((CanvasTopSecondTarget == CanvasTopFirstTarget && CanvasLeftSecondTarget == CanvasLeftFirstTarget)
                || (CanvasTopSecondTarget == CanvasTopThirdTarget && CanvasLeftSecondTarget == CanvasLeftThirdTarget))
                ChangeLocationTarget(1);
        }
        private void ThirdTargetHit(object sender)
        {
            TargetHitSoundInitialize();
            ChangeLocationTarget(2);
            GameScore += GameScoreProgress;
            CountHitInRow++;
            CountMissInRow = 1;
            CountAllHitTarget++;
            TotalShots++;
            if (CountHitInRow % 3 == 0)
            {
                GameScoreProgress += 20;
                CountHitInRow = 0;
            }
            while ((CanvasTopThirdTarget == CanvasTopSecondTarget && CanvasLeftThirdTarget == CanvasLeftSecondTarget)
                || (CanvasTopThirdTarget == CanvasTopFirstTarget && CanvasLeftThirdTarget == CanvasLeftFirstTarget))
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

        private void GamePause(object sender)
        {
            PlayGameVisibility.PlayGamePauseVisibility = Visibility.Visible;
            PlayGameVisibility.IsEnablePlayGrid = false;
            GameTimer.Stop();
        }

        private void ResumePlayGame(object sender)
        {
            PlayGameVisibility.PlayGamePauseVisibility = Visibility.Collapsed;
            PlayGameVisibility.IsEnablePlayGrid = true;
            GameTimer.Start();
        }

        private void CountMissClick(object sender)
        {
            CountAllMissClick++;
            CountHitInRow = 0;
            TotalShots++;
            GameScore -= 25 * CountMissInRow;
            if (CountMissInRow <= 5)
                CountMissInRow++;
        }

        private void InitializeTimer()
        {
            GameTimer.Interval = TimeSpan.FromSeconds(1);
            GameTimer.Tick += GameTimerTicker;
            GameTimer.Start();
        }

        private void GameTimerTicker(object sender, EventArgs e)
        {
            if (TimerTime != 0)
                TimerTime--;
            else
            {
                GameTimer.Stop();
                if (Highscore < GameScore)
                {
                    PlayGameVisibility.IsHighscore = Visibility.Visible;
                    HighScoresModel.MotionShotEasy15SecHighscore = GameScore;
                }
                PlayGameVisibility.IsEnablePlayGrid = false;
                PlayGameVisibility.AfterGameStatsVisibility = Visibility.Visible;
            }
        }

        private void OpenAdditionalStats(object sender)
        {
            PlayGameVisibility.AfterGameStatsVisibility = Visibility.Collapsed;
            PlayGameVisibility.AfterGameAdditionalStatsVisibility = Visibility.Visible;
        }


        private void CloseAdditionalStats(object sender)
        {
            PlayGameVisibility.AfterGameStatsVisibility = Visibility.Visible;
            PlayGameVisibility.AfterGameAdditionalStatsVisibility = Visibility.Collapsed;
        }

        private void BackToMenu(object sender)
        {
            PlayGameMusic.Stop();
            var StartMenuWindow = new StartMenu();
            var PlayGameWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            StartMenuWindow.Show();
            PlayGameWindow.Close();
        }

        private void RestartGame(object sender)
        {
            CountAllMissClick = 0;
            GameScore = 0;
            CountMissInRow = 1;
            CountHitInRow = 0;
            GameScoreProgress = 200;
            CountAllHitTarget = 0;
            TotalShots = 0;
            TimerTime = GameModeSettingsModel.GameTimer;
            GameTimer.Tick -= GameTimerTicker;
            InitializeTimer();
            StartLocationTarget();
            PlayGameVisibility.IsEnablePlayGrid = true;
            PlayGameVisibility.AfterGameStatsVisibility = Visibility.Collapsed;
            PlayGameVisibility.PlayGamePauseVisibility = Visibility.Collapsed;
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
