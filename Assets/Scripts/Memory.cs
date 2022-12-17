using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;

public class Memory : MonoBehaviour
{
    
    [SerializeField] private int currentData;
    
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
}