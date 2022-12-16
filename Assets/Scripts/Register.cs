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

public class Register : MonoBehaviour
{
    [SerializeField] private RegisterType type;
    private int value;
    private bool isEnabled;

    private void Awake()
    {
        value = 0;
        isEnabled = false;
    }

    private void OnEnable()
    {
        Controller.OnSend += Receive;
    }

    private void OnDisable()
    {
        Controller.OnSend -= Receive;
    }

    private void Receive(ProcessorArgs args)
    {
        if (args is RegisterArgs)
        {
            if (type == ((RegisterArgs)args).typeToSend)
            {
                DoOperation();
            }
        }
    }
    
    private void DoOperation(){}
}
