using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;

public class Controller : MonoBehaviour, DataReceiver
{

    public static Action<ProcessorArgs> OnSend;
    private Data currentData;
    
    public void ReceiveData(Data data, DataType dataType)
    {
        if (currentData == data) return;

        currentData = data;
        
        if (data is OperationData)
        {
            switch ( ((OperationData)data).operation )
            {
                
                case OperationType.Noop: DoNoop();
                    break;
                default: break;
            }
        }
        
        else if (data is InfoData)
        {
            
        }
    }

    private void DoNoop()
    {
        OnSend?.Invoke(new MuxArgs(MuxType.M1, DataType.PC));
        OnSend?.Invoke(new RegisterArgs(RegisterType.PC, false, true));
    }
}
