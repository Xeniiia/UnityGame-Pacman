using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using System.Text;

//Класс управления поступившей информацией от сервера
//Определяет что за тип сообщения и направляет в нужный класс
namespace ClientNamespace
{

    public class HandleNetworkInformation
    {
        public static AllData message = new AllData();
        public void Handle(byte[] data)
        {

            message = ProtoSerialize.Deserialize<AllData>(data);

            //Логирование
            //FileStream file2 = File.OpenWrite("log.txt");
            //file2.Write(new UTF8Encoding(true).GetBytes(message.GetField()), 0, message.GetField().Length);
            //file2.Write(new UTF8Encoding(true).GetBytes("\n"), 0, 1);

            switch (message.GetField())
            {
                case "Move":
                    MoveController.can_step = message.MoveClass.can_step_proto;
                    MovePacman.FlagForMovePacman = true;
                    break;


                case "StartPos":
                    PacmanPos.FlagForPacmanStartPos = true;
                    break;


                case "LevelInfo":
                    Field.FlagForLevelInfoHandle = true;
                    break;


                case "Unrecognized data type":
                    break;
            }
        }

    }

}
