using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace DemoPacman
{
    [ProtoContract]
    public class SendInfo
    {
        [ProtoMember(1)]
        public byte[] message;
    }
}
