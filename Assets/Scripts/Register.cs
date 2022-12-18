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
    private int value;
    private bool isEnabled;

    private void Awake()
    {
        dataSender = GetComponent<DataSender>();
        value = 0;
        isEnabled = false;
    }

    private void Start()
    {
        InvokeRepeating("SendData", 0.1f, 0.5f);
    }
    
    public void ReceiveData(Data data, DataType dataType)
    {
        if(isEnabled == false) return;
        if (data is OperationData) return;

        value = ((InfoData)data).info;
        Debug.Log(value);

    }

    private void OnEnable()
    {
        Controller.OnSend += ReceiveControllerSignal;
    }

    private void OnDisable()
    {
        Controller.OnSend -= ReceiveControllerSignal;
    }

    private void SendData()
    {
        InfoData data = ScriptableObject.CreateInstance<InfoData>();
        data.info = value;
        dataSender.SendData(data);
    }

    private void ReceiveControllerSignal(ProcessorArgs args)
    {
        
        Reset();
        
        if (args is RegisterArgs)
        {
            if (type == ((RegisterArgs)args).typeToSend)
            {
                isEnabled = ((RegisterArgs)args).load;
                if (((RegisterArgs)args).add)
                {
                    Debug.Log("Add");
                    value++;
                }
            }
        }
    }
    
    private void Reset()
    {
        isEnabled = false;
    }

}
