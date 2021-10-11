using DemoPacman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class HandleInfoFromClient
    {
        //PacmanLogic pacman = new PacmanLogic();
        public byte[] HandleInfo(AllData data)
        {
            MoveProtobufClass canMove = new MoveProtobufClass(PacmanLogic.Move(data.MoveClass.direction_step_proto, WallsList.walls), data.MoveClass.direction_step_proto);
            //Оборачиваем в универсальный класс
            AllData classCanMove = new AllData(canMove);
            byte[] serializeCanMove = ProtoSerialize.Serialize<AllData>(classCanMove);
            return serializeCanMove;
        }
    }