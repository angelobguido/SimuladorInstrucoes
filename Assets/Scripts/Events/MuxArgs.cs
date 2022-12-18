using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Events
{

    public class MuxArgs : ProcessorArgs
    {
        public MuxType typeToSend;
        public DataType typeToReceive;
        public MuxArgs(MuxType typeToSend, DataType typeToReceive)
        {
            this.typeToSend = typeToSend;
            this.typeToReceive = typeToReceive;
        }
    }

    
}
