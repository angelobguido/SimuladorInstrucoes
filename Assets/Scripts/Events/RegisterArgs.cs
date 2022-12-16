using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Events
{

    public class RegisterArgs : ProcessorArgs
    {
        
        public RegisterType typeToSend;
        public RegisterArgs(RegisterType typeToSend)
        {
            this.typeToSend = typeToSend;
        }
        
    }
    
}
