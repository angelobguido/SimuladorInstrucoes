using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public static Action OnClock;
    [SerializeField] private float clock = 2f;
    private SpriteRenderer renderer;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating("SendClockSignal", 0.1f, clock);
    }

    private void SendClockSignal()
    {
        OnClock?.Invoke();
        StartCoroutine(DoClick());
    }

    private IEnumerator DoClick()
    {
        renderer.color = Color.black;
        yield return new WaitForSeconds(clock*0.33f);
        renderer.color = Color.white;
        
    }
    
}
