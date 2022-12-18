using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;

public class Memory : MonoBehaviour
{
    
    [SerializeField] private Data[] dataList;
    private int index;

    private void Awake()
    {
        index = 0;
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
        
    }

    public void SetIndex(int index)
    {
        this.index = index;
    }
}