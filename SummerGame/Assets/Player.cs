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

    [SerializeField]  
    Axe axe;
    Rigidbody axeBody;

    bool thrown=false;
    bool hit=false;
    [SerializeField]
    float throwStrength=50;

    float xAxisCameraSpeed;
    float yAxisCameraSpeed;

    private void Start() {
        body=GetComponent<Rigidbody>();
        anim=GetComponent<Animator>();
        lookAt=playerCamera.LookAt.gameObject;
        xAxisCameraSpeed=playerCamera.m_XAxis.m_MaxSpeed;
        yAxisCameraSpeed=playerCamera.m_YAxis.m_MaxSpeed;

        axeBody=axe.GetComponent<Rigidbody>();
    }

    private void Update() {
        vertical=Input.GetAxis("Vertical");
        horizontal=Input.GetAxis("Horizontal");

        anim.SetFloat("WalkSpeed",vertical);
        anim.SetFloat("StrafeSpeed",horizontal);
        
        
        //body.AddRelativeForce(new Vector3(horizontal,0,Mathf.Clamp(vertical,-0.3f,1))*speed);
        
        
        if(vertical!=0 || horizontal!=0)
        {   
            Vector3 direction=Camera.main.transform.forward;
            direction.y=0;
            //transform.forward=direction;
            transform.forward=Vector3.Lerp(transform.forward,direction,0.5f);
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

        if(Input.GetButtonUp("Fire1")){
            if(!thrown)
                Throw();
            else if(axe.hit){
                Pull();
            }
        }

    }

    void Pull(){
        
    }

    void Throw(){
            Debug.Log("Throwing Axe");
            anim.SetTrigger("Throw");
            body.constraints=RigidbodyConstraints.FreezeAll;
    }

    public void AxeMotion(){
        if(!thrown){
        thrown=true;
        axe.transform.parent=null;
        axeBody.isKinematic=false;
        axeBody.AddForce(Camera.main.transform.forward.normalized*throwStrength,ForceMode.VelocityChange);
        axeBody.AddRelativeTorque(1000,0,0,ForceMode.VelocityChange);
        axe.GetComponentInChildren<BoxCollider>().enabled=true;
        }
        
    }

    public void ThrowEnd(){
        if(!thrown)
            body.constraints=RigidbodyConstraints.FreezeRotation;
    }

    

    private void FixedUpdate() {
        body.AddRelativeForce(new Vector3(horizontal,0,Mathf.Clamp(vertical,-0.3f,1))*speed);
    }
}
