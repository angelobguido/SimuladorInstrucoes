using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessorUnit : MonoBehaviour
{

    [SerializeField] private Path[] pathsIn;
    [SerializeField] private Path[] pathsOut;
    [SerializeField] private GameObject dataClone;
    
    // Start is called before the first frame update
    void Start()
    {
        if (pathsOut.Length > 0)
        {
            Path path = pathsOut[0];
            GameObject data = Instantiate(dataClone, transform.position, Quaternion.identity);
            data.GetComponent<DataMovement>().SetCurrentPath(path);
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
