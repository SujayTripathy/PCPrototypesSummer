using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseTime : MonoBehaviour
{

    [SerializeField]
    public GameObject subject;
    // Start is called before the first frame update
    List<Vector3> positions= new List<Vector3>();
    float duration=0;
    int i;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        

        if(Input.GetKey(KeyCode.W)){
            subject.transform.position=Vector3.Lerp(positions[i],positions[i-1],1);
            positions.Remove(positions[i]);
            if(i>1)
                i--;
        }
        else{
            positions.Add(subject.transform.position);
            i=positions.Count-1;
        }
    }

}
