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
using System.IO.Packaging;
using System.Net.Http.Headers;

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
            GamePauseCommand = new RelayCommand(GamePause, CanExecutePause);
            BackToMenuCommand = new RelayCommand(BackToMenu);
            RestartGameCommand = new RelayCommand(RestartGame);
            ResumePlayGameCommand = new RelayCommand(ResumePlayGame);
            OpenAdditionalStatsCommand = new RelayCommand(OpenAdditionalStats);
            CloseAdditionalStatsCommand = new RelayCommand(CloseAdditionalStats);
            StartLocationTarget();
            MusicInitialize();
            InitializeBeforeGameTimer();
        }

        #region InitializeProperties


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


        private DispatcherTimer BeforeGameTimer = new DispatcherTimer();
        private DispatcherTimer GameTimer = new DispatcherTimer();
        private DispatcherTimer GameSpyderTimer = new DispatcherTimer();

        public string Crosshair { get; } = SettingsPropertyModel.CrosshairPath;

        public string Target { get; } = SettingsPropertyModel.TargetPath;

        PlayGameModel playGameVisibility = new PlayGameModel();

        public DispatcherTimer TargetMove = new DispatcherTimer();

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

        private int firstTargetSizeForSpyder { get; set; } = 100;
        public int FirstTargetSizeForSpyder
        {
            get { return firstTargetSizeForSpyder; }
            set
            {
                firstTargetSizeForSpyder = value;
                OnPropertyChanged(nameof(firstTargetSizeForSpyder));
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

        private int gameScoreProgress { get; set; }
        public int GameScoreProgress
        {
            get { return gameScoreProgress; }
            set
            {
                gameScoreProgress = value;
                OnPropertyChanged(nameof(gameScoreProgress));
            }
        }

        private int gameScoreRegress {  get; set; }
        public int GameScoreRegress 
        {
            get { return gameScoreRegress; }
            set
            {
                gameScoreRegress = value;
                OnPropertyChanged(nameof(gameScoreRegress));
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

        private double avgReaction {  get; set; }
        public double AvgReaction
        {
            get { return avgReaction; }
            set
            {
                avgReaction = Math.Round(value,3);
                OnPropertyChanged(nameof(avgReaction));
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

        private int beforeGameTimerTime { get; set; }
        public int BeforeGameTimerTime
        {
            get { return beforeGameTimerTime; }
            set
            {
                beforeGameTimerTime = value;
                OnPropertyChanged(nameof(beforeGameTimerTime));
            }
        }

        private int gameSpyderTimerProp { get; set; }
        public int GameSpyderTimerProp
        {
            get { return gameSpyderTimerProp; }
            set
            {
                gameSpyderTimerProp = value;
                OnPropertyChanged(nameof(gameSpyderTimerProp));
            }
        }

        private int speedMotionFirstTargetX { get; set; }
        public int SpeedMotionFirstTargetX
        {
            get { return speedMotionFirstTargetX; }
            set
            {
                speedMotionFirstTargetX = value;
                OnPropertyChanged(nameof(speedMotionFirstTargetX));
            }
        }
        private int speedMotionFirstTargetY { get; set; }
        public int SpeedMotionFirstTargetY
        {
            get { return speedMotionFirstTargetY; }
            set
            {
                speedMotionFirstTargetY = value;
                OnPropertyChanged(nameof(speedMotionFirstTargetY));
            }
        }





        private int speedMotionSecondTargetX { get; set; }
        public int SpeedMotionSecondTargetX
        {
            get { return speedMotionSecondTargetX; }
            set
            {
                speedMotionSecondTargetX = value;
                OnPropertyChanged(nameof(speedMotionSecondTargetX));
            }
        }
        private int speedMotionSecondTargetY { get; set; }
        public int SpeedMotionSecondTargetY
        {
            get { return speedMotionSecondTargetY; }
            set
            {
                speedMotionSecondTargetY = value;
                OnPropertyChanged(nameof(speedMotionSecondTargetY));
            }
        }





        private int speedMotionThirdTargetX { get; set; }
        public int SpeedMotionThirdTargetX
        {
            get { return speedMotionThirdTargetX; }
            set
            {
                speedMotionThirdTargetX = value;
                OnPropertyChanged(nameof(speedMotionThirdTargetX));
            }
        }
        private int speedMotionThirdTargetY { get; set; }
        public int SpeedMotionThirdTargetY
        {
            get { return speedMotionThirdTargetY; }
            set
            {
                speedMotionThirdTargetY = value;
                OnPropertyChanged(nameof(speedMotionThirdTargetY));
            }
        }

        private int []SaveSpeedMotionTargets = new int[6];

        private int highscore { get; set; }
        public int Highscore {
            get 
            {
                return highscore;
            }
            set 
            {
                highscore = value;
                OnPropertyChanged(nameof(highscore));
            } 
        }
        #endregion

        #region SetGameMode

        private void SetGameMode()
        {
            switch (GameModeSettingsModel.TypeGameMode)
            {
                case "GridShot":
                    CountMissClickCommand = new RelayCommand(CountMissClickGridAndMotion);
                    playGameVisibility.FirstTargetVisibility = Visibility.Visible;
                    playGameVisibility.SecondTargetVisibility = Visibility.Visible;
                    playGameVisibility.ThirdTargetVisibility = Visibility.Visible;
                    GameScoreProgress = 200;
                    GameScoreRegress = 25;
                    FirstTargetHitCommand = new RelayCommand(FirstTargetHitGrid);
                    SecondTargetHitCommand = new RelayCommand(SecondTargetHitGrid);
                    ThirdTargetHitCommand = new RelayCommand(ThirdTargetHitGrid);
                    if(GameModeSettingsModel.GameTimer==15)
                        Highscore = HighScoresModel.GridShot15SecHighscore;
                    else if (GameModeSettingsModel.GameTimer == 30)
                        Highscore = HighScoresModel.GridShot30SecHighscore;
                    else if (GameModeSettingsModel.GameTimer == 60)
                        Highscore = HighScoresModel.GridShot60SecHighscore;
                    break;
                case "SpyderShot":
                    GameScoreProgress = 300;
                    GameScoreRegress = 40;
                    FirstTargetHitCommand = new RelayCommand(FirstTargetHitSpyder);
                    CountMissClickCommand = new RelayCommand(CountMissClickSpyder);
                    playGameVisibility.SecondTargetVisibility = Visibility.Collapsed;
                    playGameVisibility.ThirdTargetVisibility = Visibility.Collapsed;
                    if (GameModeSettingsModel.GameTimer == 15)
                        Highscore = HighScoresModel.SpyderShot15SecHighscore;
                    else if (GameModeSettingsModel.GameTimer == 30)
                        Highscore = HighScoresModel.SpyderShot30SecHighscore;
                    else if (GameModeSettingsModel.GameTimer == 60)
                        Highscore = HighScoresModel.SpyderShot60SecHighscore;
                    break;
                case "MotionShot":
                    CountMissClickCommand = new RelayCommand(CountMissClickGridAndMotion);
                    GameScoreProgress = 200;
                    GameScoreRegress = 25;
                    playGameVisibility.FirstTargetVisibility = Visibility.Visible;
                    playGameVisibility.SecondTargetVisibility = Visibility.Visible;
                    playGameVisibility.ThirdTargetVisibility = Visibility.Visible;

                    FirstTargetHitCommand = new RelayCommand(FirstTargetHitMotion);
                    SecondTargetHitCommand = new RelayCommand(SecondTargetHitMotion);
                    ThirdTargetHitCommand = new RelayCommand(ThirdTargetHitMotion);

                    ChangeSpeedMotion(0);
                    ChangeSpeedMotion(1);
                    ChangeSpeedMotion(2);

                    InitialTargetMove();

                    if (GameModeSettingsModel.GameTimer == 15 && GameModeSettingsModel.DifficultOfGameModeMotionGrid == "Easy")
                        Highscore = HighScoresModel.MotionShotEasy15SecHighscore;
                    else if (GameModeSettingsModel.GameTimer == 30 && GameModeSettingsModel.DifficultOfGameModeMotionGrid == "Easy")
                        Highscore = HighScoresModel.MotionShotEasy30SecHighscore;
                    else if (GameModeSettingsModel.GameTimer == 60 && GameModeSettingsModel.DifficultOfGameModeMotionGrid == "Easy")
                        Highscore = HighScoresModel.MotionShotEasy60SecHighscore;
                    else if (GameModeSettingsModel.GameTimer == 15 && GameModeSettingsModel.DifficultOfGameModeMotionGrid == "Medium")
                        Highscore = HighScoresModel.MotionShotMedium15SecHighscore;
                    else if (GameModeSettingsModel.GameTimer == 30 && GameModeSettingsModel.DifficultOfGameModeMotionGrid == "Medium")
                        Highscore = HighScoresModel.MotionShotMedium30SecHighscore;
                    else if (GameModeSettingsModel.GameTimer == 60 && GameModeSettingsModel.DifficultOfGameModeMotionGrid == "Medium")
                        Highscore = HighScoresModel.MotionShotMedium60SecHighscore;
                    else if (GameModeSettingsModel.GameTimer == 15 && GameModeSettingsModel.DifficultOfGameModeMotionGrid == "Hard")
                        Highscore = HighScoresModel.MotionShotHard15SecHighscore;
                    else if (GameModeSettingsModel.GameTimer == 30 && GameModeSettingsModel.DifficultOfGameModeMotionGrid == "Hard")
                        Highscore = HighScoresModel.MotionShotHard30SecHighscore;
                    else if (GameModeSettingsModel.GameTimer == 60 && GameModeSettingsModel.DifficultOfGameModeMotionGrid == "hard")
                        Highscore = HighScoresModel.MotionShotHard60SecHighscore;
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region TargetHit

        private void FirstTargetHitGrid(object sender)
        {
            TargetHitSoundInitialize();
            ChangeLocationTarget(0);
            TargetHitGridAndMotion();
            while ((CanvasTopFirstTarget == CanvasTopSecondTarget && CanvasLeftFirstTarget == CanvasLeftSecondTarget)
                || (CanvasTopFirstTarget == CanvasTopThirdTarget && CanvasLeftFirstTarget == CanvasLeftThirdTarget))
                ChangeLocationTarget(0);
        }

        private void SecondTargetHitGrid(object sender)
        {
            TargetHitSoundInitialize();
            ChangeLocationTarget(1);
            TargetHitGridAndMotion();
            while ((CanvasTopSecondTarget == CanvasTopFirstTarget && CanvasLeftSecondTarget == CanvasLeftFirstTarget)
                || (CanvasTopSecondTarget == CanvasTopThirdTarget && CanvasLeftSecondTarget == CanvasLeftThirdTarget))
                ChangeLocationTarget(1);
        }
        private void ThirdTargetHitGrid(object sender)
        {
            TargetHitSoundInitialize();
            ChangeLocationTarget(2);
            TargetHitGridAndMotion();
            while ((CanvasTopThirdTarget == CanvasTopSecondTarget && CanvasLeftThirdTarget == CanvasLeftSecondTarget)
                || (CanvasTopThirdTarget == CanvasTopFirstTarget && CanvasLeftThirdTarget == CanvasLeftFirstTarget))
                ChangeLocationTarget(2);
        }

        private void FirstTargetHitSpyder(object sender)
        {
            TargetHitSoundInitialize();
            Random RandomTargetSize = new Random();
            FirstTargetSizeForSpyder = (5 + RandomTargetSize.Next(12))*10;
            TargetHitGridAndMotion();
            playGameVisibility.FirstTargetVisibility = Visibility.Collapsed;
            GameSpyderTimerProp = 500;
            GameSpyderTimer.Interval = TimeSpan.FromMilliseconds(250);
            GameSpyderTimer.Tick += GameSpyderTimerTicker;
            GameSpyderTimer.Start();
        }

        private void TargetHitGridAndMotion()
        {
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
        }
        private void CountMissClickGridAndMotion(object sender)
        {
            CountAllMissClick++;
            CountHitInRow = 0;
            TotalShots++;
            GameScore -= GameScoreRegress * CountMissInRow;
            if (CountMissInRow <= 5)
                CountMissInRow++;
        }

        private void CountMissClickSpyder(object sender)
        {
            CountAllMissClick++;
            TotalShots++;
            GameScore -= GameScoreRegress;
        }

        private void FirstTargetHitMotion(object sender)
        {
            TargerHitMotion(0);
        }
        private void SecondTargetHitMotion(object sender)
        {
            TargerHitMotion(1);
        }
        private void ThirdTargetHitMotion(object sender)
        {
            TargerHitMotion(2);
        }

        private void TargerHitMotion(int targetNumber)
        {
            TargetHitSoundInitialize();
            ChangeLocationTarget(targetNumber);
            ChangeSpeedMotion(targetNumber);
            TargetHitGridAndMotion();
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
                    CanvasTopFirstTarget = ((CanvasRandom.Next(1001) * CanvasRandom.Next(1001)) % 6) * 100;
                    CanvasLeftFirstTarget = (CanvasRandom.Next(1001) % 13) * 100;
                    break;
                case 1:
                    CanvasTopSecondTarget = ((CanvasRandom.Next(1001) * CanvasRandom.Next(1001) * CanvasRandom.Next(1001)) % 6) * 100;
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
        #endregion

        #region MotionGameMode
        private void ChangeSpeedMotion(int target)
        {
            int ChangeSpeedX = 0, ChangeSpeedY = 0;
            Random random = new Random();
            switch(GameModeSettingsModel.DifficultOfGameModeMotionGrid)
            {
                case "Easy":
                    ChangeSpeedX = 3 + random.Next(3);
                    ChangeSpeedY = 3 + random.Next(3);
                    break;
                case "Medium":
                    ChangeSpeedX = 6 + random.Next(3);
                    ChangeSpeedY = 6 + random.Next(3);
                    break;
                case "Hard":
                    ChangeSpeedX = 9 + random.Next(3);
                    ChangeSpeedY = 9 + random.Next(3);
                    break;
                default:
                    break;
            }
            if(random.Next(1001)%2==0)
                ChangeSpeedX = -ChangeSpeedX;
            if (random.Next(2001) % 2 == 0)
                ChangeSpeedY = -ChangeSpeedY;
            switch (target)
            {
                case 0:
                    SpeedMotionFirstTargetX = ChangeSpeedX;
                    SpeedMotionFirstTargetY = ChangeSpeedY;
                    break;
                case 1:
                    SpeedMotionSecondTargetX = ChangeSpeedX;
                    SpeedMotionSecondTargetY = ChangeSpeedY;
                    break;
                case 2:
                    SpeedMotionThirdTargetX = ChangeSpeedX;
                    SpeedMotionThirdTargetY = ChangeSpeedY;
                    break;
                default:
                    break;
            }
        }

        private void SaveSpeedMotionTargetsUntilPause(bool pause)
        {
            if (pause)
            {
                SaveSpeedMotionTargets[0] = SpeedMotionFirstTargetX;
                SaveSpeedMotionTargets[1] = SpeedMotionFirstTargetY;
                SaveSpeedMotionTargets[2] = SpeedMotionSecondTargetX;
                SaveSpeedMotionTargets[3] = SpeedMotionSecondTargetY;
                SaveSpeedMotionTargets[4] = SpeedMotionThirdTargetY;
                SaveSpeedMotionTargets[5] = SpeedMotionThirdTargetX;
                SpeedMotionFirstTargetX = 0;
                SpeedMotionFirstTargetY =0;
                SpeedMotionSecondTargetX = 0;
                SpeedMotionSecondTargetY = 0;
                SpeedMotionThirdTargetX= 0;
                SpeedMotionThirdTargetY = 0;
            }
            else
            {
                SpeedMotionFirstTargetX = SaveSpeedMotionTargets[0];
                SpeedMotionFirstTargetY = SaveSpeedMotionTargets[1];
                SpeedMotionSecondTargetX = SaveSpeedMotionTargets[2];
                SpeedMotionSecondTargetY = SaveSpeedMotionTargets[3];
                SpeedMotionThirdTargetX = SaveSpeedMotionTargets[4];
                SpeedMotionThirdTargetY = SaveSpeedMotionTargets[5];
            }
        }

        #endregion

        #region AllTimers

        private void InitializeBeforeGameTimer()
        {
            BeforeGameTimerTime = 3;
            PlayGameVisibility.BeforeGameTimerVisibility  = Visibility.Visible;
            BeforeGameTimer.Interval = TimeSpan.FromSeconds(1);
            BeforeGameTimer.Tick += BeforeGameTimerTicker;
            BeforeGameTimer.Start();
        }

        private void BeforeGameTimerTicker(object sender, EventArgs e)
        {
            if (BeforeGameTimerTime != 1)
                BeforeGameTimerTime--;
            else
            {
                BeforeGameTimerTime--;
                BeforeGameTimer.Stop();
                PlayGameVisibility.BeforeGameTimerVisibility = Visibility.Collapsed;
                PlayGameVisibility.IsEnablePlayGrid = true;
                InitializeTimer();
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
            if (TimerTime != 1)
                TimerTime--;
            else
            {
                TimerTime--;
                GameTimer.Stop();
                GameSpyderTimer?.Stop();
                AfterGameResults();
            }
        }
        private void GameSpyderTimerTicker(object sender, EventArgs e)
        {
            if (GameSpyderTimerProp != 0)
                GameSpyderTimerProp -= 500;
            else
            {
                GameSpyderTimer.Tick -= GameSpyderTimerTicker;
                GameSpyderTimer.Stop();
                playGameVisibility.FirstTargetVisibility = Visibility.Visible;
                ChangeLocationTarget(0);
            }
        }
        private void InitialTargetMove()
        {
            TargetMove.Interval = TimeSpan.FromMilliseconds(1);
            TargetMove.Tick += TargetsMoveTicker;
            TargetMove.Start();
        }
        private void TargetsMoveTicker(object sender, EventArgs e)
        {
            if (TimerTime == 0)
                TargetMove.Stop();
            if (CanvasLeftFirstTarget < 0 || CanvasLeftFirstTarget > 1300)
                SpeedMotionFirstTargetX = -SpeedMotionFirstTargetX;
            if (CanvasTopFirstTarget < 0 || CanvasTopFirstTarget > 600)
                SpeedMotionFirstTargetY = -SpeedMotionFirstTargetY;

            if (CanvasLeftSecondTarget < 0 || CanvasLeftSecondTarget > 1300)
                SpeedMotionSecondTargetX = -SpeedMotionSecondTargetX;
            if (CanvasTopSecondTarget < 0 || CanvasTopSecondTarget > 600)
                SpeedMotionSecondTargetY = -SpeedMotionSecondTargetY;

            if (CanvasLeftThirdTarget < 0 || CanvasLeftThirdTarget > 1300)
                SpeedMotionThirdTargetX = -SpeedMotionThirdTargetX;
            if (CanvasTopThirdTarget < 0 || CanvasTopThirdTarget > 600)
                SpeedMotionThirdTargetY = -SpeedMotionThirdTargetY;

            CanvasLeftFirstTarget += SpeedMotionFirstTargetX;
            CanvasTopFirstTarget += SpeedMotionFirstTargetY;
            CanvasLeftSecondTarget += SpeedMotionSecondTargetX;
            CanvasTopSecondTarget += SpeedMotionSecondTargetY;
            CanvasLeftThirdTarget += SpeedMotionThirdTargetX;
            CanvasTopThirdTarget += SpeedMotionThirdTargetY;
        }
        #endregion

        #region PauseButtons
        private void GamePause(object sender)
        {
            PlayGameVisibility.PlayGamePauseVisibility = Visibility.Visible;
            PlayGameVisibility.IsEnablePlayGrid = false;
            SaveSpeedMotionTargetsUntilPause(true);
            GameTimer.Stop();
            GameSpyderTimer?.Stop();
        }

        private void ResumePlayGame(object sender)
        {
            PlayGameVisibility.PlayGamePauseVisibility = Visibility.Collapsed;
            PlayGameVisibility.IsEnablePlayGrid = true;
            SaveSpeedMotionTargetsUntilPause(false);
            GameTimer.Start();
            GameSpyderTimer?.Start();
        }

        private bool CanExecutePause(object sender)
        {
            if (TimerTime == 0 || PlayGameVisibility.PlayGamePauseVisibility == Visibility.Visible || BeforeGameTimerTime != 0)
                return false;
            else
                return true;
        }
        #endregion

        #region AfterGameButtons
        private void AfterGameResults()
        {
            AvgReaction = (double)GameModeSettingsModel.GameTimer / CountAllHitTarget;
            if (AvgReaction == double.PositiveInfinity)
                AvgReaction = 0;
            PlayGameVisibility.IsEnablePlayGrid = false;
            PlayGameVisibility.AfterGameStatsVisibility = Visibility.Visible;
            if (Highscore < GameScore)
            {
                PlayGameVisibility.IsHighscore = Visibility.Visible;
                if (GameModeSettingsModel.GameTimer == 15 && GameModeSettingsModel.TypeGameMode == "GridShot")
                    HighScoresModel.GridShot15SecHighscore = GameScore;
                else if (GameModeSettingsModel.GameTimer == 30 && GameModeSettingsModel.TypeGameMode == "GridShot")
                    HighScoresModel.GridShot30SecHighscore = GameScore;
                else if (GameModeSettingsModel.GameTimer == 60 && GameModeSettingsModel.TypeGameMode == "GridShot")
                    HighScoresModel.GridShot60SecHighscore = GameScore;

                else if (GameModeSettingsModel.GameTimer == 15 && GameModeSettingsModel.TypeGameMode == "SpyderShot")
                    HighScoresModel.SpyderShot15SecHighscore = GameScore;
                else if (GameModeSettingsModel.GameTimer == 30 && GameModeSettingsModel.TypeGameMode == "SpyderShot")
                    HighScoresModel.SpyderShot30SecHighscore = GameScore;
                else if (GameModeSettingsModel.GameTimer == 60 && GameModeSettingsModel.TypeGameMode == "SpyderShot")
                    HighScoresModel.SpyderShot60SecHighscore = GameScore;

                else if (GameModeSettingsModel.GameTimer == 15 && GameModeSettingsModel.TypeGameMode == "MotionShot" && GameModeSettingsModel.DifficultOfGameModeMotionGrid == "Easy")
                    HighScoresModel.MotionShotEasy15SecHighscore = GameScore;
                else if (GameModeSettingsModel.GameTimer == 30 && GameModeSettingsModel.TypeGameMode == "MotionShot" && GameModeSettingsModel.DifficultOfGameModeMotionGrid == "Easy")
                    HighScoresModel.MotionShotEasy30SecHighscore = GameScore;
                else if (GameModeSettingsModel.GameTimer == 60 && GameModeSettingsModel.TypeGameMode == "MotionShot" && GameModeSettingsModel.DifficultOfGameModeMotionGrid == "Easy")
                    HighScoresModel.MotionShotEasy60SecHighscore = GameScore;

                else if (GameModeSettingsModel.GameTimer == 15 && GameModeSettingsModel.TypeGameMode == "MotionShot" && GameModeSettingsModel.DifficultOfGameModeMotionGrid == "Medium")
                    HighScoresModel.MotionShotMedium15SecHighscore = GameScore;
                else if (GameModeSettingsModel.GameTimer == 30 && GameModeSettingsModel.TypeGameMode == "MotionShot" && GameModeSettingsModel.DifficultOfGameModeMotionGrid == "Medium")
                    HighScoresModel.MotionShotMedium30SecHighscore = GameScore;
                else if (GameModeSettingsModel.GameTimer == 60 && GameModeSettingsModel.TypeGameMode == "MotionShot" && GameModeSettingsModel.DifficultOfGameModeMotionGrid == "Medium")
                    HighScoresModel.MotionShotMedium60SecHighscore = GameScore;

                else if (GameModeSettingsModel.GameTimer == 15 && GameModeSettingsModel.TypeGameMode == "MotionShot" && GameModeSettingsModel.DifficultOfGameModeMotionGrid == "Hard")
                    HighScoresModel.MotionShotHard15SecHighscore = GameScore;
                else if (GameModeSettingsModel.GameTimer == 30 && GameModeSettingsModel.TypeGameMode == "MotionShot" && GameModeSettingsModel.DifficultOfGameModeMotionGrid == "Hard")
                    HighScoresModel.MotionShotHard30SecHighscore = GameScore;
                else if (GameModeSettingsModel.GameTimer == 60 && GameModeSettingsModel.TypeGameMode == "MotionShot" && GameModeSettingsModel.DifficultOfGameModeMotionGrid == "Hard")
                    HighScoresModel.MotionShotHard60SecHighscore = GameScore;
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
            BeforeGameTimer.Tick -= BeforeGameTimerTicker;
            GameTimer.Tick -= GameTimerTicker;
            GameSpyderTimer.Tick -= GameTimerTicker;
            TargetMove.Tick -= TargetsMoveTicker;
            SetGameMode();
            PlayGameVisibility.IsHighscore = Visibility.Collapsed;
            InitializeBeforeGameTimer();
            StartLocationTarget();
            PlayGameVisibility.IsEnablePlayGrid = false;
            PlayGameVisibility.AfterGameStatsVisibility = Visibility.Collapsed;
            PlayGameVisibility.PlayGamePauseVisibility = Visibility.Collapsed;
        }
        #endregion

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
