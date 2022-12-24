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

    private void Start()
    {
        StartCoroutine(ChangeState());
    }

    public void ReceiveData(Data data, DataType dataType)
    {
        if (loadIR == false) return;
        if (currentData == data) return;

        if (data is OperationData)
        {

            currentData = data;
            StartCoroutine(ChangeState());
            
        }
        
    }

    private IEnumerator ChangeState()
    {
        yield return new WaitForSeconds(2);
        
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
        
        yield return new WaitForSeconds(2);
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
            case OperationType.Noop: StartCoroutine(ChangeState());
                break;
            
            case OperationType.Loadn:
                DoLoadnTranslation();
                StartCoroutine(ChangeState());
                break;
            
            default: StartCoroutine(ChangeState());
                break;
            
        }
    }

    private void StartExecution()
    {
        loadIR = false;
        
        switch ( ((OperationData)currentData).operation )
        {
            case OperationType.Noop: StartCoroutine(ChangeState());
                break;
            
            case OperationType.Loadn: StartCoroutine(ChangeState());
                break;
            
            default: StartCoroutine(ChangeState());
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
