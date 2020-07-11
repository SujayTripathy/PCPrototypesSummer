using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Player : MonoBehaviour
{
    Rigidbody body;
    Animator anim;

    GameObject lookAt;
    float vertical;
    float horizontal;
    
    [SerializeField]
    float speed=1;

    [SerializeField]
    bool lockedOn=false;

    [SerializeField]
    CinemachineFreeLook playerCamera;
    [SerializeField]
    Head head;

    float xAxisCameraSpeed;
    float yAxisCameraSpeed;

    private void Start() {
        body=GetComponent<Rigidbody>();
        anim=GetComponent<Animator>();
        lookAt=playerCamera.LookAt.gameObject;
        xAxisCameraSpeed=playerCamera.m_XAxis.m_MaxSpeed;
        yAxisCameraSpeed=playerCamera.m_YAxis.m_MaxSpeed;
    }

    private void Update() {
        vertical=Input.GetAxis("Vertical");
        horizontal=Input.GetAxis("Horizontal");

        anim.SetFloat("WalkSpeed",vertical);
        anim.SetFloat("StrafeSpeed",horizontal);
        
        if(vertical!=0 || horizontal!=0)
        {    
            anim.SetBool("Walk",true);
        }
        else{
            anim.SetBool("Walk",false);
        }

        if(Input.GetButtonUp("LockOn")){
            if(lockedOn){
                lockedOn=false;
                playerCamera.LookAt=lookAt.transform;
                playerCamera.m_YAxis.m_MaxSpeed=yAxisCameraSpeed;
                playerCamera.m_XAxis.m_MaxSpeed=xAxisCameraSpeed;
                body.freezeRotation=true;
            }
            else{
                RaycastHit hit;
                Debug.Log("Called");
                if(Physics.SphereCast(head.transform.position,1,-head.transform.forward,out hit,100)){
                    if(hit.transform.tag=="Target"){
                        Debug.Log(hit.transform.name);
                        playerCamera.LookAt=hit.transform;
                        playerCamera.m_YAxis.m_MaxSpeed=0;
                        playerCamera.m_XAxis.m_MaxSpeed=0;
                        lockedOn=true;
                    }
                    else{
                        Debug.Log("Nothing targetable");
                    }
                }               
            }
            anim.SetBool("LockedOn",lockedOn);
        }

    }

    

    private void FixedUpdate() {
        if(lockedOn){
        body.AddRelativeForce(new Vector3(horizontal,0,Mathf.Clamp(vertical,-0.3f,1))*speed);
        }
        else{
        Vector3 direction=Camera.main.transform.forward;
        direction.y=0;
        direction.Normalize();
        if(vertical!=0||horizontal!=0){
            Debug.Log(new Vector3(horizontal,0,vertical).normalized);
            //Debug.Log((new Vector3(horizontal,0,vertical)+direction).normalized);
            body.AddRelativeForce((new Vector3(horizontal,0,vertical).normalized+direction).normalized*speed);
        }
        }
    }
}
