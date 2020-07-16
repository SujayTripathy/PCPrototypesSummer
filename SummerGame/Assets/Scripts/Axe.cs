using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{

    Rigidbody body;
    public bool hit=false;
    // Start is called before the first frame update
    void Start()
    {
        body=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {   
    }

    private void OnCollisionEnter(Collision other) {
        body.constraints=RigidbodyConstraints.FreezeAll;
        hit=true;
    }

}
