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

    private void Start()
    {
        InvokeRepeating("SendData", 0.1f, 0.5f);
    }
    
    private void SendData()
    {
        if (currentIndex >= dataList.Length)
        {
            Debug.Log("End of program");
            return;
        }        
        
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