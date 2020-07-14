using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{

    Rigidbody body;
    public bool hit;
    // Start is called before the first frame update
    void Start()
    {
        body=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {  
        if(hit){
            
        }
        
    }

    private void OnCollisionEnter(Collision other) {
        body.constraints=RigidbodyConstraints.FreezeAll;
        hit=true;
    }

}
