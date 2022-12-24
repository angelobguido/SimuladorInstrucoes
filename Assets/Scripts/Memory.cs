using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;
using UnityEngine.Serialization;

public class Memory : MonoBehaviour, DataReceiver
{

    public static Action OnProgramEnd;
    
    [SerializeField] private Data[] dataList;
    [SerializeField] private DataSender dataSender;
    private bool isEnabled;
    private int nextIndex;
    private int currentIndex;
    private DataDisplay display;
    private NumberDisplayController indexDisplay;
    
    
    private void OnEnable()
    {
        Clock.OnClock += ReceiveClock;
    }

    private void OnDisable()
    {
        Clock.OnClock -= ReceiveClock;
    }
    
    private void Awake()
    {
        isEnabled = true;
        display = GetComponent<DataDisplay>();
        indexDisplay = GetComponent<NumberDisplayController>();
    }

    private void Start()
    {
        InvokeRepeating("SendData", 0.1f, 0.1f);
    }

    private void ReceiveClock()
    {
        currentIndex = nextIndex;
        if (currentIndex >= dataList.Length) return;
        display.UpdateTextWithData(dataList[currentIndex]);
        indexDisplay.ChangeNumber(currentIndex);
    }

    private void SendData()
    {
        if (currentIndex >= dataList.Length)
        {
            Debug.Log("End of program");
            OnProgramEnd?.Invoke();
            return;
        }
        
        if (dataList[currentIndex] == null)
        {
            Data dataToSend = ScriptableObject.CreateInstance<OperationData>();
            ((OperationData)dataToSend).operation = OperationType.Noop;
            dataList[currentIndex] = dataToSend;
        }

        dataSender.SendData(dataList[currentIndex]);
    }

    public void ReceiveData(Data data, DataType dataType)
    {
        if (!isEnabled) return;
        if (data is InfoData)
        {
            nextIndex = ((InfoData)data).info;
        }
    }
    

}