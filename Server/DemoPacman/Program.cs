using System;
using System.Collections.Generic;
using System.Threading;

namespace DemoPacman
{
    class Program
    {
        //public static PacmanLogic pacman;
        static void Main(string[] args)
        {
            string filename = "walls.txt";

            //List<Point> walls;
            ParserLevel parsLevel = new ParserLevel();
            Point levelSize;

            WallsList.walls = parsLevel.Pars(filename);
            levelSize = parsLevel.GetLevelSize();
            //Старт сервера
            Server.SetupServer();

            LevelProtobufClass level = new LevelProtobufClass(levelSize.x, levelSize.y, WallsList.walls); //Создаем ПротоКласс с информацией о уровне
            AllData levelDataForSend = new AllData(level);                                      //Оборачиваем в универсальный класс

            
            byte[] serializeBytesLevel = ProtoSerialize.Serialize<AllData>(levelDataForSend);   //Сериализуем для дальнейшей отправки
            Server.connectDone.WaitOne();                               //Ожидаем, пока к серверу подключится клиент
            Server.SendDataTo(serializeBytesLevel);                     //Отправляем клиенту

            Thread.Sleep(1000); //Ждем

            CurrentPosPacman pacman = new CurrentPosPacman(levelSize.x - 2, 1);   //Устанавливаем начальную позицию пакмена


            //Создаем ПротоКласс с информацией о стартовой позиции и отправляем:
            PacmanStartPosProtobufClass pacmanStartPos = new PacmanStartPosProtobufClass(CurrentPosPacman.position.x + 1, CurrentPosPacman.position.y + 1);
            AllData startPosForSend = new AllData(pacmanStartPos);      //Оборачиваем в универсальный класс
            byte[] serializePacmanStartPosition = ProtoSerialize.Serialize<AllData>(startPosForSend);

            Server.SendDataTo(serializePacmanStartPosition);

            //Подождать тут


            Server.levelInfoSend.Set();

            //Для отладки////////////////////////////////////////////////////////////////////////////////////////////
            while (true)
            {
                string move = Console.ReadLine();   //Прием сообщения о перемещении
                if (move == "") break;
                MoveProtobufClass canMove = new MoveProtobufClass(PacmanLogic.Move(move, WallsList.walls), move);
                AllData classCanMove = new AllData(canMove);
                byte[] serializeCanMove = ProtoSerialize.Serialize<AllData>(classCanMove);
                Server.SendDataTo(serializeCanMove);

            }
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }


    }
}
