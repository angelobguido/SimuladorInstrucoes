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
    private string senderTag;
    
    public void LoadData(Data data, DataType type, Path path, string senderTag)
    {
        this.data = data;
        this.type = type;
        this.senderTag = senderTag;
        GetComponent<DataMovement>().SetCurrentPath(path);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.CompareTag(senderTag)) return;
        
        col.gameObject.GetComponent<DataReceiver>().ReceiveData(data, type);
        
        Destroy(gameObject);

    }
}
