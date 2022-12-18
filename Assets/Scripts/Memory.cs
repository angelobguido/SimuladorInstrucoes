using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;
using UnityEngine.Serialization;

public class Memory : MonoBehaviour, DataReceiver
{
    
    [SerializeField] private Data[] dataList;
    private int currentIndex;
    [SerializeField] private DataSender dataSender;

    private void Awake()
    {
        currentIndex = 0;
    }

    private void OnEnable()
    {
        Controller.OnSend += ReceiveControllerSignal;
    }

    private void OnDisable()
    {
        Controller.OnSend -= ReceiveControllerSignal;
    }

    private void ReceiveControllerSignal(ProcessorArgs args)
    {
        if (args is MemoryArgs)
        {
            SendData();
        }
    }
    
    private void SendData()
    {
        dataSender.SendData(dataList[currentIndex]);
    }

    public void ReceiveData(Data data, DataType dataType)
    {
        if (data is InfoData)
        {
            currentIndex = ((InfoData)data).info;
        }
    }
    

}