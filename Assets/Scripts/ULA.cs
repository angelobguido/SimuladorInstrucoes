using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ULAOperation
{
    Add
}

public class ULA : MonoBehaviour
{
    public  Queue<Data> first = new Queue<Data>();
    public Queue<Data> second = new Queue<Data>();
    private ULAOperation currentOperation = ULAOperation.Add;
    private DataSender dataSender;

    private void OnEnable()
    {
        Controller.OnReset += Reset;
    }

    private void OnDisable()
    {
        Controller.OnReset -= Reset;
    }

    private void Awake()
    {
        dataSender = GetComponent<DataSender>();
    }

    private void Update()
    {
        if (first.Count != 0 && second.Count != 0)
        {
            DoOperation(first.Dequeue(), second.Dequeue());
        }
    }

    private void Reset()
    {
        first.Clear();
        second.Clear();
        currentOperation = ULAOperation.Add;
    }

    private void DoOperation(Data a, Data b)
    {
        if (a is InfoData && b is InfoData)
        {
            switch (currentOperation)
            {
                case ULAOperation.Add: dataSender.SendData(AddValues((InfoData)a, (InfoData)b));
                    break;
            }
        }
    }

    private Data AddValues(InfoData a, InfoData b)
    {
        InfoData data = ScriptableObject.CreateInstance<InfoData>();
        data.info = a.info + b.info;

        return data;
    }
    
}
