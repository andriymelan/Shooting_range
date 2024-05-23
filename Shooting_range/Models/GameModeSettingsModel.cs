using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Shooting_range.Models
{
    public class GameModeSettingsModel
    {
        private static int gameTimer {  get; set; }
        public static int GameTimer { 
            get 
            {
                return gameTimer;    
            }
            set 
            {
                gameTimer = value;
            }
        }

        private static string typeGameMode {  get; set; }
        public static string TypeGameMode
        {
            get { return typeGameMode; }
            set
            {
                typeGameMode = value;
            }
        }

        private static string difficultOfGameModeMotionGrid {  get; set; }
        public static string DifficultOfGameModeMotionGrid
        {
            get { return difficultOfGameModeMotionGrid; }
            set
            {
                difficultOfGameModeMotionGrid = value;
            }
        }
    }
}
