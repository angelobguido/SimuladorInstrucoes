using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DataType
{
    PC,
    DATA_OUT
}
public class DataController : MonoBehaviour
{
    private Data data;
    private DataType type;
    
    public void LoadData(Data data, DataType type, Path path)
    {
        this.data = data;
        this.type = type;
        GetComponent<DataMovement>().SetCurrentPath(path);
    }
    
}
