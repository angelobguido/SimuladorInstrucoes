using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;

public enum State
{
    None,
    Search,
    Translation,
    Execution
}

public class Controller : MonoBehaviour, DataReceiver
{

    public static Action<ProcessorArgs> OnSend;
    public static Action OnReset;
    private Data currentData;
    private bool loadIR = false;
    private State currentState = State.None;

    private void OnEnable()
    {
        Clock.OnClock += ReceiveClock;
    }

    private void OnDisable()
    {
        Clock.OnClock -= ReceiveClock;
    }

    private void ReceiveClock()
    {
        ChangeState();
    }

    public void ReceiveData(Data data, DataType dataType)
    {
        if (loadIR == false) return;
        if (currentData == data) return;

        if (data is OperationData)
        {

            currentData = data;

        }
        
    }

    private void ChangeState()
    {
        switch (currentState)
        {
            case State.None:
                currentState = State.Search; 
                StartSearch();
                break;
            
            case State.Search:
                currentState = State.Translation;
                StartTranslation();
                break;
            
            case State.Translation:
                currentState = State.Execution;
                StartExecution();
                break;
            
            case State.Execution:
                currentState = State.Search;
                StartSearch();
                break;
        }
        
    }
    
    private void StartSearch()
    {
        OnSend?.Invoke(new MuxArgs(MuxType.M1, DataType.PC));
        OnSend?.Invoke(new RegisterArgs(RegisterType.PC, false, true));
        loadIR = true;
    }

    private void StartTranslation()
    {
        loadIR = false;

        switch ( ((OperationData)currentData).operation )
        {
            case OperationType.Noop: 
                break;
            
            case OperationType.Loadn:
                DoLoadnTranslation();
                break;
            
            default: 
                break;
            
        }
    }

    private void StartExecution()
    {
        loadIR = false;
        
        switch ( ((OperationData)currentData).operation )
        {
            case OperationType.Noop: 
                break;
            
            case OperationType.Loadn: 
                break;
            
            default: 
                break;
            
        }
    }

    private void DoLoadnTranslation()
    {
        Debug.Log("LoadN");
        OnReset?.Invoke();
        OnSend?.Invoke(new MuxArgs(MuxType.M1, DataType.PC));
        OnSend?.Invoke(new RegisterArgs(RegisterType.PC, false, true));
        OnSend?.Invoke(new MuxArgs(MuxType.M2, DataType.DATA_OUT));
        OnSend?.Invoke(new RegisterArgs( ((OperationData)currentData).registers[0] , true, false));
    }
}
