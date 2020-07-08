using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.forward=-Camera.main.transform.forward;
        //Debug.Log(transform.localRotation.eulerAngles.y);
        if(transform.localRotation.eulerAngles.y>270){
            transform.localRotation=Quaternion.Euler(transform.localRotation.x,260,transform.localRotation.z);
        }
        if(transform.localRotation.eulerAngles.y<90){       
            transform.localRotation=Quaternion.Euler(transform.localRotation.x,100,transform.localRotation.z);
        }
    }
}
