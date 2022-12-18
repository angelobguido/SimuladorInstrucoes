using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;

public enum MuxType
{
    M1,
    M2
}

public class Mux : MonoBehaviour
{
    [SerializeField] private MuxType type;
    [SerializeField] private DataType receive;

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
        if (args is MuxArgs)
        {
            if (type == ((MuxArgs)args).typeToSend)
            {
                receive = ((MuxArgs)args).typeToReceive;
            }
        }
    }

}
