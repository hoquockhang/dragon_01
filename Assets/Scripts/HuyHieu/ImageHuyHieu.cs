using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.HuyHieu
{
    public class ImageHuyHieu
    {
        public static Image[] Images = new Image[8];
        public static string[] Name = new string[8] { "bronze", "silver", "gold", "emeral", "ruby", "diamond", "master", "champion" };
        public static void Init()
        {
            for (int i = 0; i < 8; i++)
            {
                Images[i] = GameCanvas.loadImage("/huyhieu/" + (22220 + i));
            }

        }

    }
}
