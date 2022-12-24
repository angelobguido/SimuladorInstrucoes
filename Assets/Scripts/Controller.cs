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
    private State currentState;
    private State nextState = State.Search;

    private void OnEnable()
    {
        Clock.OnClock += ReceiveClock;
        Memory.OnProgramEnd += Stop;
    }

    private void OnDisable()
    {
        Clock.OnClock -= ReceiveClock;
        Memory.OnProgramEnd -= Stop;
    }

    private void ReceiveClock()
    {
        currentState = nextState;
        UpdateState();
    }

    public void ReceiveData(Data data, DataType dataType)
    {
        if (loadIR == false) return;
        if (currentData == data) return;

        if (data is OperationData)
        {

            currentData = data;
            nextState = State.Translation;

        }
        
    }

    private void Stop()
    {
        nextState = State.None;
    }

    private void UpdateState()
    {
        OnReset?.Invoke();
        
        switch (currentState)
        {
            case State.Search:
                StartSearch();
                break;
            
            case State.Translation:
                StartTranslation();
                break;
            
            case State.Execution:
                StartExecution();
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
                nextState = State.Search;
                break;
            
            case OperationType.Loadn:
                DoLoadnTranslation();
                nextState = State.Search;
                break;
            
            case OperationType.Load:
                DoLoadTranslation();
                nextState = State.Execution;
                break;
            
            default: 
                nextState = State.Search;
                break;
            
        }
    }

    private void StartExecution()
    {
        loadIR = false;
        
        switch ( ((OperationData)currentData).operation )
        {
            case OperationType.Load:
                DoLoadExecution();
                nextState = State.Search;
                break;
            
            default: 
                nextState = State.Search;
                break;
            
        }
    }

    private void DoLoadnTranslation()
    {
        OnSend?.Invoke(new MuxArgs(MuxType.M1, DataType.PC));
        OnSend?.Invoke(new RegisterArgs(RegisterType.PC, false, true));
        OnSend?.Invoke(new MuxArgs(MuxType.M2, DataType.DATA_OUT));
        OnSend?.Invoke(new RegisterArgs( ((OperationData)currentData).registers[0] , true, false));
    }
    
    private void DoLoadTranslation()
    {
        OnSend?.Invoke(new MuxArgs(MuxType.M1, DataType.PC));
        OnSend?.Invoke(new RegisterArgs(RegisterType.PC, false, true));
        OnSend?.Invoke(new RegisterArgs( RegisterType.MAR , true, false));
    }
    
    private void DoLoadExecution()
    {
        OnSend?.Invoke(new MuxArgs(MuxType.M1, DataType.MAR));
        OnSend?.Invoke(new MuxArgs(MuxType.M2, DataType.DATA_OUT));
        OnSend?.Invoke(new RegisterArgs( ((OperationData)currentData).registers[0] , true, false));
    }
}
