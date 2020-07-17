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
    Cinemachine.CinemachineTargetGroup lockOnGroup;

    [SerializeField]
    Head head;


    float xAxisCameraSpeed;
    float yAxisCameraSpeed;

    private void Start() {
        body=GetComponent<Rigidbody>();
        anim=GetComponent<Animator>();

        //Camera Stuff
        lookAt=playerCamera.LookAt.gameObject;
        xAxisCameraSpeed=playerCamera.m_XAxis.m_MaxSpeed;
        yAxisCameraSpeed=playerCamera.m_YAxis.m_MaxSpeed;

        
    }

    private void Update() {
        vertical=Input.GetAxis("Vertical");
        horizontal=Input.GetAxis("Horizontal");

        anim.SetFloat("WalkSpeed",vertical);
        anim.SetFloat("StrafeSpeed",horizontal);
        
        
        
        if(vertical!=0 || horizontal!=0)                    //Walking 
        {   
            Vector3 direction=Camera.main.transform.forward;
            direction.y=0;
            transform.forward=Vector3.Lerp(transform.forward,direction,0.5f);
            anim.SetBool("Walk",true);
        }
        else{
            anim.SetBool("Walk",false);
        }

        if(Input.GetButtonUp("LockOn")){                //LockOn CameraStuff
            if(lockedOn){
                lockedOn=false;
                playerCamera.LookAt=lookAt.transform;
                playerCamera.m_YAxis.m_MaxSpeed=yAxisCameraSpeed;
                playerCamera.m_XAxis.m_MaxSpeed=xAxisCameraSpeed;
                body.freezeRotation=true;
                lockOnGroup.RemoveMember(lockOnGroup.m_Targets[1].target.transform);
            }
            else{                                                                                   //LockOn Logic
                RaycastHit hit;
                Debug.Log("Called");
                if(Physics.SphereCast(head.transform.position,1,-head.transform.forward,out hit,100)){
                    if(hit.transform.tag=="Target"){
                        Debug.Log(hit.transform.name);
                        lockOnGroup.AddMember(hit.transform,1,0);
                        playerCamera.m_YAxis.m_MaxSpeed=0;
                        playerCamera.m_XAxis.m_MaxSpeed=0;
                        lockedOn=true;
                        
                    }
                    else{
                        Debug.Log("Nothing targetable");
                    }
                }               
            }
        }
   }
    
    private void FixedUpdate() {
        if(vertical>0.1||vertical<-0.1||horizontal>0.1||horizontal<-0.1){                                       //Deadzone
        body.AddRelativeForce(new Vector3(horizontal,0,Mathf.Clamp(vertical,-0.5f,1))*speed,ForceMode.Acceleration);
        }
    }
}
