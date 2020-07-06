using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody body;
    Animator anim;

    float vertical;
    float horizontal;
    
    [SerializeField]
    float speed=1;

    private void Start() {
        body=GetComponent<Rigidbody>();
        anim=GetComponent<Animator>();
    }

    private void Update() {
        vertical=Input.GetAxis("Vertical");
        horizontal=Input.GetAxis("Horizontal");
        Debug.Log(horizontal);
        anim.SetFloat("WalkSpeed",vertical);
        anim.SetFloat("StrafeSpeed",horizontal);
        if(vertical!=0 || horizontal!=0)
        {    
            anim.SetBool("Walk",true);
        }
        else{
            anim.SetBool("Walk",false);
        }

    }

    private void FixedUpdate() {
        //body.AddRelativeForce(Vector3.forward*Mathf.Clamp(vertical,-0.3f,1));
        body.AddRelativeForce(new Vector3(horizontal,0,Mathf.Clamp(vertical,-0.3f,1))*speed);
        
    }
}
