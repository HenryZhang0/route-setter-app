using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour
{
   
    Ray ray;
    RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider.tag != "Wall") {
                return;
            }
            transform.position = hit.point;
        }
    }
}
