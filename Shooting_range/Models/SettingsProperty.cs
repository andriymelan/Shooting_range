using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Shooting_range.Models
{
    public class SettingsProperty:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        private string targetPath { get; set; } = "../Targets/Aqua-Target.png";
        public string TargetPath
        {
            get { return targetPath; }
            set
            {
                targetPath = value;
                OnPropertyChanged(nameof(targetPath));
            }
        }
        static string cursorDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Crosshairs";
        private string crosshairPath { get; set; } = $@"{cursorDirectory}\\Fine\\AquaFineCrosshair.cur";
        public string CrosshairPath
        {
            get
            {
                return crosshairPath;
            }
            set
            {
                crosshairPath = $"{cursorDirectory}{value}";
                OnPropertyChanged(nameof(crosshairPath));
            }
        }
    }
}
