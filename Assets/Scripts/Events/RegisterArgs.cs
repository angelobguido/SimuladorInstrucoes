using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Events
{

    public class RegisterArgs : ProcessorArgs
    {
        
        public RegisterType typeToSend;
        public bool load;
        public bool add;
        
        public RegisterArgs(RegisterType typeToSend, bool load, bool add)
        {
            this.typeToSend = typeToSend;
            this.load = load;
            this.add = add;
        }
        
    }
    
}
