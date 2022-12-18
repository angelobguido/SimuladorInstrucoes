using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OperationType
{
    Noop,
    Loadn,
    Load,
    Pop,
    Push,
    Add
    
}

[CreateAssetMenu(fileName = "OperationData", menuName = "ScriptableObjects/Data/OperationData")]
public class OperationData : Data
{
    public OperationType operation;
    public RegisterType[] registers;
    
}
