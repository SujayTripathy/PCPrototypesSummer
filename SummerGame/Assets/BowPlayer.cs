using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class BowPlayer : MonoBehaviour
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

    [SerializeField]
    float jumpPower=10;

    [SerializeField]
    float groundRay=100;


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
        
        
        //body.AddRelativeForce(new Vector3(horizontal,0,Mathf.Clamp(vertical,-0.3f,1))*speed);
        RaycastHit hit1;
        if(Physics.Raycast(transform.position,-transform.up,out hit1,groundRay)){ 
            Debug.Log(hit1.distance);
           if(hit1.transform.tag=="Ground"&& hit1.distance<0.0004){
               anim.SetBool("Fall",false);
                if(vertical!=0 || horizontal!=0)
                {   
                    Vector3 direction=Camera.main.transform.forward;
                    direction.y=0;
                    transform.forward=Vector3.Lerp(transform.forward,direction,0.5f);
                    anim.SetBool("Walk",true);
                }
                else{
                    anim.SetBool("Walk",false);
                }

                if(Input.GetButtonUp("Jump")){
                anim.SetTrigger("Jump");
                }
           }
            else if(hit1.distance>0.0007){     
                if(!anim.GetBool("Fall"))                 {            
                anim.SetBool("Fall",true);
            }
            anim.SetBool("Walk",false);
            //Debug.Log("In DA AIR");
        }
        }


        if(Input.GetButtonUp("LockOn")){
            if(lockedOn){
                lockedOn=false;
                playerCamera.LookAt=lookAt.transform;
                playerCamera.m_YAxis.m_MaxSpeed=yAxisCameraSpeed;
                playerCamera.m_XAxis.m_MaxSpeed=xAxisCameraSpeed;
                body.freezeRotation=true;
                lockOnGroup.RemoveMember(lockOnGroup.m_Targets[1].target.transform);
            }
            else{
                RaycastHit hit;
                Debug.Log("Called");
                if(Physics.SphereCast(head.transform.position,1,-head.transform.forward,out hit,100)){
                    if(hit.transform.tag=="Target"){
                        Debug.Log(hit.transform.name);
                        //playerCamera.LookAt=hit.transform;
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

   public void Jump(){
            Debug.Log("Jumping");
            body.AddRelativeForce(new Vector3(0,jumpPower,0),ForceMode.VelocityChange);
   }
    
    private void FixedUpdate() {
        RaycastHit hit1;
        if(Physics.Raycast(transform.position,-transform.up,out hit1,groundRay)){ 
            if(hit1.transform.tag=="Ground" && hit1.distance<0.0004){
            //Debug.Log("On Ground");
            if(vertical>0.1||vertical<-0.1||horizontal>0.1||horizontal<-0.1){                                       //Deadzone
            body.AddRelativeForce(new Vector3(horizontal,0,Mathf.Clamp(vertical,-0.5f,1))*speed,ForceMode.Acceleration);
            }
        }
        }
    }
}

