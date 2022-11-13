 using UnityEngine;
 using System.Collections;
 
 public class OpenTrello : MonoBehaviour 
 {
     Ray ray;
     RaycastHit hit;
     
     void Update()
     {
         ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         if(Physics.Raycast(ray, out hit))
         {
             if(Input.GetMouseButtonUp(0)) {
                 print(hit.collider.name);
                 if(hit.collider.name == "Cube") {
                    Application.OpenURL("https://trello.com/b/CHA5xMqr/climbing-app");
                 }
             }
         }
    }
 }