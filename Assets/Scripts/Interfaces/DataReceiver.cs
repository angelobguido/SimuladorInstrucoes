using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DataReceiver
{
    public void ReceiveData(Data data, DataType dataType);
}
