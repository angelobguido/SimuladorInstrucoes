using UnityEngine;

public enum DataType
{
    PC,
    DATA_OUT,
    Nothing
}
public class DataController : MonoBehaviour
{
    private Data data;
    private DataType type;
    private string senderName;
    
    public void LoadData(Data data, DataType type, Path path, string senderName)
    {
        this.data = data;
        this.type = type;
        this.senderName = senderName;
        GetComponent<DataMovement>().SetCurrentPath(path);

        if (data is InfoData)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == senderName) return;
        
        col.gameObject.GetComponent<DataReceiver>().ReceiveData(data, type);
        
        Destroy(gameObject);

    }
}
