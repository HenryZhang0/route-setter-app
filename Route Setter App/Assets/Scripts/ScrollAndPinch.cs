using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollAndPinch : MonoBehaviour {
    Vector3 touchStart;
    public float sensitivity = 0.1f;    
	private Vector2 targetDifference;
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0)){
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if(Input.touchCount == 1){
            float y = Input.GetTouch(0).deltaPosition.x;
            float x = Input.GetTouch(0).deltaPosition.y;
            Debug.Log(x + ":" + y);
            Vector3 rotateValue = new Vector3(x, y * -1, 0);
            Camera.main.transform.eulerAngles -= rotateValue * sensitivity;
        }

        if(Input.touchCount == 2){
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            zoom(difference * 0.01f);
            
            Vector2 targetCenter = touchZero.position + touchOne.position;
            targetCenter /= 2;
            // targetCenter.x *= -1;

            Vector2 targetCenterPrev = touchZeroPrevPos + touchOnePrevPos;
            targetCenterPrev /= 2;
            // targetCenterPrev.x *= -1;

            targetDifference = targetCenter - targetCenterPrev;
            Debug.Log(targetDifference);
            // Debug.Log(touchZeroPrevPos.y);
            // Vector2 differenceVector = prevVector - currentVector;
            // Debug.Log(differenceVector);
        }else if(Input.GetMouseButton(0)){
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
        }
        //zoom(Input.GetAxis("Mouse ScrollWheel"));
	}

    void zoom(float increment){
        Camera.main.transform.Translate(Vector3.forward * increment);
        Camera.main.transform.position -= (Vector3)targetDifference * sensitivity;
        // Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }

}



// public class ScrollAndPinch : MonoBehaviour {
//     private Vector3 touchStart;
//     public Camera cam;
//     public float groundZ = 0;

// 	void Update () {
//         if (Input.GetMouseButtonDown(0)){
//             touchStart = GetWorldPosition(groundZ);
//         }
//         if (Input.GetMouseButton(0)){
//             Vector3 direction = touchStart - GetWorldPosition(groundZ);
//             cam.transform.position += direction;
//         }
//     }
//     private Vector3 GetWorldPosition(float z){
//         Ray mousePos = cam.ScreenPointToRay(Input.mousePosition);
//         Plane ground = new Plane(Vector3.forward, new Vector3(0,0,z));
//         float distance;
//         ground.Raycast(mousePos, out distance);
//         return mousePos.GetPoint(distance);
//     }
// }