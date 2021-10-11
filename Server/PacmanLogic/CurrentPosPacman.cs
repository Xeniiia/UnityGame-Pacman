using DemoPacman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

  public class CurrentPosPacman
    {
        public static Point position = new Point();
        public CurrentPosPacman(int positionX, int positionY)
        {
            position.x = positionX;
            position.y = positionY;
        }
    }
