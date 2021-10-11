using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoPacman
{
    public class ParserLevel
    {
        //Размер игрового поля:
            int sizeX;
            int sizeY;
            
        public Point GetLevelSize()
        {
            return new Point(sizeX, sizeY);
        }

        //Метод извлечения из txt информации о стенах
        public List<Point> Pars(string filename)
        {
            List<Point> listCoordinat = new List<Point>();  //Список со структурой Point с координатами стен
            string[] line = File.ReadAllLines(filename);    //Получить строки из файла

            for (int i = 0; i < line.Length; i++)           //Построчно проходимся по txt файлу
            {
                if (line[i] == "//Size")                    //Если это Size, то запишем в отдельные переменные
                {
                    string[] sizeLine = (line[i + 1]).Split(';');   //Разбираем строку, используя разделить полей
                    sizeX = int.Parse(sizeLine[0]);                 //Преобразуем значения в int
                    sizeY = int.Parse(sizeLine[1]);                 //Преобразуем значения в int
                    i += 2;                                         //Переход к следующим строкам
                }

                if (line[i][0] != '/')                             //Если строка не комментарий
                {
                    string[] splitedLine = line[i].Split(';');     //Разбираем строку, используя разделить полей
                    listCoordinat.Add(new Point(int.Parse(splitedLine[0]), int.Parse(splitedLine[1])));    //Добавляем в список
                }
            }
            return listCoordinat;
        }

    }
}
