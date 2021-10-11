using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoPacman
{
    public class PacmanLogic
    {
        PacmanLogic() { }
        public static bool Move (string move, List<Point> listWithWalls)   // move = L  R  U  D = запрос на перемещение (от нажатой клавиши клиента)
        {
            Point stepPacman = new Point(CurrentPosPacman.position.x, CurrentPosPacman.position.x);

            switch (move)
            {
                case "L":
                    stepPacman.Set(CurrentPosPacman.position.x - 1, CurrentPosPacman.position.y);
                    break;
                case "R":
                    stepPacman.Set(CurrentPosPacman.position.x + 1, CurrentPosPacman.position.y);
                    break;
                case "U":
                    stepPacman.Set(CurrentPosPacman.position.x, CurrentPosPacman.position.y + 1);
                    break;
                case "D":
                    stepPacman.Set(CurrentPosPacman.position.x, CurrentPosPacman.position.y - 1);
                    break;
            }
            return GetWall(stepPacman, listWithWalls);
        }

        //Узнать где стена
        public static bool GetWall (Point step, List<Point> listWithWalls)
        {
            //Ищем стену в списке клеток "стен"
            Point filtered = listWithWalls.Find(x => x.x == step.x && x.y == step.y);
            if (filtered == null)           //Если не нашел
            {                               //Двигаться можно
                CurrentPosPacman.position.x = step.x;  //Обновляем позицию
                CurrentPosPacman.position.y = step.y;
                return true;                //Да, пакмен подвинулся
            }
            else return false;              //Иначе нет - не подвинулся
        }

    }
}
