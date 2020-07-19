using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(Vector3.up,180);
        body=GetComponent<Rigidbody>();
        body.AddForce(-transform.forward*20,ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision other) {
        body.constraints=RigidbodyConstraints.FreezeAll;
    }
}
