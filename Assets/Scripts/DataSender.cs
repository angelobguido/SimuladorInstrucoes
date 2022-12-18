using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSender : MonoBehaviour
{
    [SerializeField] private DataType typeToSend;
    [SerializeField] private GameObject dataClone;
    [SerializeField] private Path[] pathsToSend;

    public void SendData(Data data)
    {
        foreach (var path in pathsToSend)
        {
            GameObject dataObject = Instantiate(dataClone, transform.position, Quaternion.identity);
            dataObject.GetComponent<DataController>().LoadData(data, typeToSend, path, name);                        
        }

    }
}
