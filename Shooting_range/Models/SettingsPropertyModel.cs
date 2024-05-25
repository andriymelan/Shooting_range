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
    public class SettingsPropertyModel
    {
        static private string targetPath { get; set; } = "../Targets/Aqua-Target.png";
        static public string TargetPath
        {
            get { return targetPath; }
            set
            {
                targetPath = value;
            }
        }
        static string cursorDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Crosshairs";
        static private string crosshairPath { get; set; } = $@"{cursorDirectory}\\Fine\\AquaFineCrosshair.cur";
        static public string CrosshairPath
        {
            get
            {
                return crosshairPath;
            }
            set
            {
                crosshairPath = $@"{cursorDirectory}\\{value}";
            }

        }


        static private double musicVolume { get; set; } = 30;
        static public double MusicVolume
        {
            get { return musicVolume; }
            set
            {
                musicVolume = value;
            }
        }


        static private double soundVolume { get; set; } = 30;
        static public double SoundVolume
        {
            get { return soundVolume; }
            set
            {
                soundVolume = value;
            }
        }
    }
}
