using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public static Action OnClock;
    [SerializeField] private float clock = 2f;

    private void Start()
    {
        InvokeRepeating("SendClockSignal", 0.1f, clock);
    }

    private void SendClockSignal()
    {
        OnClock?.Invoke();
    }
    
}
