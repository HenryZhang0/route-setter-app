using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxFun : MonoBehaviour
{
    public float min=2f;
    public float max=3f;
    // Use this for initialization
    void Start () {
       
        min=transform.position.x-5;
        max=transform.position.x+10;
   
    }
   
    // Update is called once per frame
    void Update () {
       
       
        transform.position =new Vector3(Mathf.PingPong(Time.time*2,max-min)+min, transform.position.y, transform.position.z);
        transform.Rotate (0,0,50*Time.deltaTime);
    }
}
