using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Input
{
    FIRST,
    SECOND
}

public class ULAInput : MonoBehaviour, DataReceiver
{
    [SerializeField] private Input currentInput;
    [SerializeField] private ULA ula;
    
    public void ReceiveData(Data data, DataType dataType)
    {
        switch (currentInput)
        {
            case Input.FIRST: ula.first.Enqueue(data);
                break;
            
            case Input.SECOND: ula.second.Enqueue(data);
                break;
        }
    }
}
