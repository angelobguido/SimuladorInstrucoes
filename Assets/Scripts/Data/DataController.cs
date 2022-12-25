using UnityEngine;

public enum DataType
{
    PC,
    MAR,
    SP,
    DATA_OUT,
    ULA,
    R0,
    R1,
    R2,
    R3,
    R4,
    R5,
    R6,
    R7,
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
            GetComponentInChildren<DataColor>().ChangeColor(Color.red);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == senderName) return;
        
        col.gameObject.GetComponent<DataReceiver>().ReceiveData(data, type);
        
        Destroy(gameObject);

    }
}
