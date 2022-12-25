using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;

    public void UpdateTextWithData(Data data)
    {
        if (data is InfoData)
        {
            text.text = ((InfoData)data).info.ToString();
        }

        if (data is OperationData)
        {
            OperationData op = (OperationData)data;
            switch (op.operation)
            {
                case OperationType.Loadn:
                    text.text = "LOADN" + " " + GetStringFromRegisterType(op.registers[0]);
                    break;

                case OperationType.Load:
                    text.text = "LOAD" + " " + GetStringFromRegisterType(op.registers[0]);
                    break;

                case OperationType.Noop:
                    text.text = "NOOP";
                    break;
                
                case OperationType.Add:
                    text.text = "ADD" + " " + GetStringFromRegisterType(op.registers[0]) + " " +
                                GetStringFromRegisterType(op.registers[1]) + " " + GetStringFromRegisterType(op.registers[2]);
                    break;
                
                default:
                    break;
            }
        }
    }

    private string GetStringFromRegisterType(RegisterType type)
    {
        switch (type)
        {
            case RegisterType.R0:
                return "R0";

            case RegisterType.R1:
                return "R1";
            
            case RegisterType.R2:
                return "R2";

            case RegisterType.R3:
                return "R3";

            case RegisterType.R4:
                return "R4";

            case RegisterType.R5:
                return "R5";

            case RegisterType.R6:
                return "R6";

            case RegisterType.R7:
                return "R7";

        }

        return null;
    }

}
