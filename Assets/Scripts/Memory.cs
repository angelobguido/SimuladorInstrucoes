using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;
using UnityEngine.Serialization;

public class Memory : MonoBehaviour, DataReceiver
{
    
    [SerializeField] private Data[] dataList;
    [SerializeField] private DataSender dataSender;
    private int currentIndex;
    private DataDisplay display;
    private NumberDisplayController indexDisplay;
    
    private void Awake()
    {
        currentIndex = 20;
        display = GetComponent<DataDisplay>();
        indexDisplay = GetComponent<NumberDisplayController>();
    }

    private void Start()
    {
        InvokeRepeating("SendData", 0.1f, 0.1f);
    }
    
    private void SendData()
    {
        if (currentIndex >= dataList.Length)
        {
            Debug.Log("End of program");
            return;
        }        
        
        dataSender.SendData(dataList[currentIndex]);
        display.UpdateTextWithData(dataList[currentIndex]);
        indexDisplay.ChangeNumber(currentIndex);
    }

    public void ReceiveData(Data data, DataType dataType)
    {
        if (data is InfoData)
        {
            currentIndex = ((InfoData)data).info;
        }
    }
    

}