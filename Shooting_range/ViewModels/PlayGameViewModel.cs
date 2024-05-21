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
            TargetHitCommand = new RelayCommand(TargetHit);
            TargetHitCommand1 = new RelayCommand(TargetHit1);
            TargetHitCommand2 = new RelayCommand(TargetHit2);

            MusicInitialize();
        }


        public RelayCommand TargetHitCommand { get; set; }
        public RelayCommand TargetHitCommand1 { get; set; }
        public RelayCommand TargetHitCommand2 { get; set; }

        public string Crosshair { get; } = SettingsProperty.CrosshairPath;

        public string Target { get; } = SettingsProperty.TargetPath;

        private long canvasTop { get; set; } = 0;
        public long CanvasTop
        {
            get
            {
                return canvasTop;
            }
            set
            {
                canvasTop = value;
                OnPropertyChanged(nameof(canvasTop));
            }
        }

        private long canvasLeft { get; set; } = 0;
        public long CanvasLeft
        {
            get
            {
                return canvasLeft;
            }
            set
            {
                canvasLeft = value;
                OnPropertyChanged(nameof(canvasLeft));
            }
        }

        private long canvasTop1 { get; set; } = 0;
        public long CanvasTop1
        {
            get
            {
                return canvasTop1;
            }
            set
            {
                canvasTop1 = value;
                OnPropertyChanged(nameof(canvasTop1));
            }
        }

        private long canvasLeft1 { get; set; } = 0;
        public long CanvasLeft1
        {
            get { return canvasLeft1;}
            set
            {
                canvasLeft1 = value;
                OnPropertyChanged(nameof(canvasLeft1));
            }
        }

        private long canvasTop2 { get; set; } = 0;
        public long CanvasTop2
        {
            get { return canvasTop2;}
            set
            {
                canvasTop2 = value;
                OnPropertyChanged(nameof(canvasTop2));
            }
        }

        private long canvasLeft2 { get; set; } = 0;
        public long CanvasLeft2
        {
            get { return canvasLeft2; }
            set
            {
                canvasLeft2 = value;
                OnPropertyChanged(nameof(canvasLeft2));
            }
        }

        private void TargetHit(object sender)
        {
            TargetHitSoundInitialize();
            ChangeLocationTarget(0);
        }

        private void TargetHit1(object sender)
        {
            TargetHitSoundInitialize();
            ChangeLocationTarget(1);
        }
        private void TargetHit2(object sender)
        {
            TargetHitSoundInitialize();
            ChangeLocationTarget(2);
        }

        private void StartLocationTarget(int targetNumber)
        {
            Random randomStartLocation = new Random();

        }
        private void ChangeLocationTarget(int targetNumber)
        {
            Random CanvasRandom = new Random();
            switch (targetNumber)
            {
                case 0:
                    CanvasTop = (CanvasRandom.Next(1001) % 6) * 100;
                    CanvasLeft = (CanvasRandom.Next(1001) % 13) * 100;
                    if ((CanvasTop == CanvasTop1 && CanvasLeft == CanvasLeft1)||(CanvasTop == CanvasTop2 && CanvasLeft == CanvasLeft2))
                        ChangeLocationTarget(targetNumber);
                    break;
                case 1:
                    CanvasTop1 = (CanvasRandom.Next(1001) % 6) * 100;
                    CanvasLeft1 = (CanvasRandom.Next(1001) % 13) * 100;
                    if ((CanvasTop1 == CanvasTop && CanvasLeft1 == CanvasLeft) || (CanvasTop1 == CanvasTop2 && CanvasLeft1 == CanvasLeft2))
                        ChangeLocationTarget(targetNumber);
                    break;
                case 2:
                    CanvasTop2 = (CanvasRandom.Next(1001) % 6) * 100;
                    CanvasLeft2 = (CanvasRandom.Next(1001) % 13) * 100;
                    if ((CanvasTop2 == CanvasTop1 && CanvasLeft2 == CanvasLeft1) || (CanvasTop2 == CanvasTop && CanvasLeft2 == CanvasLeft))
                        ChangeLocationTarget(targetNumber);
                    break;
                default: 
                    break;
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
            TargetHitSound.Volume = (SettingsProperty.SoundVolume) / 100;
            TargetHitSound.Play();
        }
        private void MusicInitialize()
        {
            PlayGameMusic = new MediaPlayer();
            PlayGameMusic.MediaEnded += MusicEnd;
            PlayGameMusic.Open(new Uri(PlayGameMusicPath));
            PlayGameMusic.Volume = (SettingsProperty.MusicVolume) / 100;
            PlayGameMusic.Play();
        }
        private void MusicEnd(object sender, EventArgs e)
        {
            PlayGameMusic.Position = TimeSpan.MinValue;
        }
        #endregion
    }
}
