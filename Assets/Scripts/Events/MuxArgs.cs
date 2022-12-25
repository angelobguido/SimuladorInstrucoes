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
        
        public MuxArgs(MuxType typeToSend, RegisterType registerTypeToReceive)
        {
            this.typeToSend = typeToSend;
            this.typeToReceive = TranslateRegisterType(registerTypeToReceive);
        }

        private static DataType TranslateRegisterType(RegisterType type)
        {
            switch (type)
            {
                case RegisterType.R0:
                    return DataType.R0;

                case RegisterType.R1:
                    return DataType.R1;
                
                case RegisterType.R2:
                    return DataType.R2;
                
                case RegisterType.R3:
                    return DataType.R3;

                case RegisterType.R4:
                    return DataType.R4;

                case RegisterType.R5:
                    return DataType.R5;

                case RegisterType.R6:
                    return DataType.R6;

                case RegisterType.R7:
                    return DataType.R7;

            }

            return DataType.Nothing;
        }
    }

    
}
