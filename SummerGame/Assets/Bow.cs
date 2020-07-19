using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{

    [SerializeField] GameObject spawnPoint;

    [SerializeField] GameObject Arrow;

    public void Fire(){
        Debug.Log("Firing");
        Instantiate(Arrow,spawnPoint.transform.position,transform.rotation);
    }

}
