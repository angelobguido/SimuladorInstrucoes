using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;

public enum MuxType
{
    M1,
    M2,
    M3,
    M4,
    M5,
    M6
}

public class Mux : MonoBehaviour, DataReceiver
{
    [SerializeField] private MuxType type;
    [SerializeField] private DataType receive;
    private DataSender dataSender;

    private void Awake()
    {
        dataSender = GetComponent<DataSender>();
    }

    private void OnEnable()
    {
        Controller.OnSend += ReceiveControllerSignal;
    }

    private void OnDisable()
    {
        Controller.OnSend -= ReceiveControllerSignal;
    }
    
    public void ReceiveData(Data data, DataType dataType)
    {
        if (dataType == receive)
        {
            dataSender.SendData(data);
        }
    }

    private void ReceiveControllerSignal(ProcessorArgs args)
    {
        
        if (args is MuxArgs)
        {
            if (type == ((MuxArgs)args).typeToSend)
            {
                receive = ((MuxArgs)args).typeToReceive;
            }
        }
    }

    private void Reset()
    {
        receive = DataType.Nothing;
    }

}
