using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;

public enum RegisterType
{
    R0,
    R1,
    R2,
    R3,
    R4,
    R5,
    R6,
    R7,
    MAR,
    PC,
    SP,
    FR
}

public class Register : MonoBehaviour, DataReceiver
{
    [SerializeField] private RegisterType type;
    private DataSender dataSender;
    private int currentValue;
    private int nextValue;
    private bool isEnabled;

    private void OnEnable()
    {
        Controller.OnSend += ReceiveControllerSignal;
        Controller.OnReset += Reset;
        Clock.OnClock += ReceiveClock;
    }

    private void OnDisable()
    {
        Controller.OnSend -= ReceiveControllerSignal;
        Controller.OnReset -= Reset;
        Clock.OnClock -= ReceiveClock;
    }
    
    private void Awake()
    {
        dataSender = GetComponent<DataSender>();
        currentValue = 0;
        isEnabled = false;
    }

    private void Start()
    {
        InvokeRepeating("SendData", 0.1f, 0.5f);
    }

    private void ReceiveClock()
    {
        currentValue = nextValue;
    }
    
    public void ReceiveData(Data data, DataType dataType)
    {
        if(isEnabled == false) return;
        if (data is OperationData) return;

        nextValue = ((InfoData)data).info;
        Debug.Log(currentValue);

    }

    private void SendData()
    {
        InfoData data = ScriptableObject.CreateInstance<InfoData>();
        data.info = currentValue;
        dataSender.SendData(data);
    }

    private void ReceiveControllerSignal(ProcessorArgs args)
    {
        
        if (args is RegisterArgs)
        {
            if (type == ((RegisterArgs)args).typeToSend)
            {
                isEnabled = ((RegisterArgs)args).load;
                if (((RegisterArgs)args).add)
                {
                    Debug.Log("Add");
                    nextValue = currentValue + 1;
                }
            }
        }
    }
    
    private void Reset()
    {
        isEnabled = false;
    }

}
