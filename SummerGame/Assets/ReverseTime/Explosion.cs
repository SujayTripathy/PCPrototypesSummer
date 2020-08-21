using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        body=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space)){
            Debug.Log("Explode");
            body.AddExplosionForce(1000,transform.position,5,0.2f,ForceMode.Acceleration);
        }
    }
}
