using ProtoBuf;
using System.Collections.Generic;

    [ProtoContract]
    public class AllData
    {
        [ProtoMember(1)]
        public MoveProtobufClass MoveClass;
        [ProtoMember(2)]
        public PacmanStartPosProtobufClass StartPosClass;
        [ProtoMember(3)]
        public LevelProtobufClass LevelClass;

        public AllData(MoveProtobufClass protoClass)
        {
            MoveClass = protoClass;
            StartPosClass = null;
            LevelClass = null;
        }

        public AllData() { }

        public AllData(PacmanStartPosProtobufClass protoClass)
        {
            MoveClass = null;
            StartPosClass = protoClass;
            LevelClass = null;
        }

        public AllData(LevelProtobufClass protoClass)
        {
            MoveClass = null;
            StartPosClass = null;
            LevelClass = protoClass;
        }

        public string GetField()
        {
            if (MoveClass != null) return "Move";
            else if (StartPosClass != null) return "StartPos";
            else if (LevelClass != null) return "LevelInfo";
            else return "Unrecognized data type";
        }
    }

    [ProtoContract]
    public class MoveProtobufClass
    {
        [ProtoMember(1)]
        public bool can_step_proto { get; set; }

        [ProtoMember(2)]
        public string direction_step_proto { get; set; }

        public MoveProtobufClass() { }
        public MoveProtobufClass(bool answer)
        {
            can_step_proto = answer;
        }
        public MoveProtobufClass(string direction)
        {
            direction_step_proto = direction;
        }
        public MoveProtobufClass(bool answer, string direction)
        {
            can_step_proto = answer;
            direction_step_proto = direction;
        }
    }

    [ProtoContract]
    public class PacmanStartPosProtobufClass
    {
        [ProtoMember(1)]
        public int startPosX_proto { get; set; }
        [ProtoMember(2)]
        public int startPosY_proto { get; set; }

        public PacmanStartPosProtobufClass() { }

        public PacmanStartPosProtobufClass(int x, int y)
        {
            startPosX_proto = x;
            startPosY_proto = y;
        }
    }


    [ProtoContract]
    public class LevelProtobufClass
    {
        [ProtoMember(1)]
        public int sizeX_proto { get; set; }
        [ProtoMember(2)]
        public int sizeY_proto { get; set; }

        [ProtoMember(3)]
        public List<ClientNamespace.Point> wall { get; set; }

        //Конструкторы
        public LevelProtobufClass () { }
        public LevelProtobufClass(int sizeX, int sizeY, List<ClientNamespace.Point> list)
        {
            sizeX_proto = sizeX;
            sizeY_proto = sizeY;
            wall = list;
        }
    }