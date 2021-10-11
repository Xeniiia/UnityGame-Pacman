using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace ClientNamespace
{
    [ProtoContract]
    public class Point
    {
        [ProtoMember(1)]
        public int x;

        [ProtoMember(2)]
        public int y;

        public Point() { }
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public void Set(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
