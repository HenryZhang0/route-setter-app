using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{
#if UNITY_IOS || UNITY_ANDROID
    public Camera Camera;
    public GameObject target;
    public GameObject DefaultPosition;
    public GameObject p;

    protected Plane Plane; // camera plane
    public bool Rotate;

    public float rotationSensitivity = 2.5f;
    public float scrollSensitivity = 1f;
    public float zoomSensitivity = 1f;
    Vector2 orbitAngles = new Vector2(45f, 0f);

    private void Update()
    {
        //Update Plane
        if (Input.touchCount >= 1){
            // Vector3 normal = (target.transform.position - Camera.transform.position).normalized;
            // Vector3 position = target.transform.position + normal;
            Vector3 normal = Camera.transform.forward;
            Vector3 position = Camera.transform.position + normal*5;
            Plane.SetNormalAndPosition(normal, position);
            p.transform.position = Camera.transform.position + normal*5;
            p.transform.rotation = Quaternion.LookRotation(Plane.normal);
            p.transform.Rotate(-90,0,0);
        }

        if (Input.touchCount == 1)
        {
            Vector2 input = Input.GetTouch(0).deltaPosition;
            input = new Vector2(input.y*-1, input.x);
            input *= rotationSensitivity;
            // OrbitCamera(input);
        }


        var Delta1 = Vector3.zero;
        var Delta2 = Vector3.zero;

        //Scroll
        if (Input.touchCount >= 2)
        {
            Delta1 = PlanePositionDelta(Input.GetTouch(0));
            if (Input.GetTouch(0).phase == TouchPhase.Moved){
                Debug.Log("Scroll");
                Camera.transform.Translate(Delta1, Space.World);
            }
        }

        //Pinch
        if (Input.touchCount >= 2)
        {
            Vector3 pos1  = PlanePosition(Input.GetTouch(0).position);
            Vector3 pos2  = PlanePosition(Input.GetTouch(1).position);
            Vector3 pos1b = PlanePosition(Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition);
            Vector3 pos2b = PlanePosition(Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition);

            //calc zoom
            var zoom = Vector3.Distance(pos1, pos2) /
                       Vector3.Distance(pos1b, pos2b);

            //edge case
            if (zoom == 0 || zoom > 10)
                return;

            // if(zoom > 1 && Mathf.Abs(Plane.GetDistanceToPoint(Camera.transform.position)) < 1) return;

            //Move cam amount the mid ray
            Camera.transform.position = Vector3.LerpUnclamped(pos1, Camera.transform.position, 1 / zoom);

            if (Rotate && pos2b != pos2)
                Camera.transform.RotateAround(pos1, Plane.normal, Vector3.SignedAngle(pos2 - pos1, pos2b - pos1b, Plane.normal));
        }
    }

    protected Vector3 PlanePositionDelta(Touch touch)
    {
        //not moved
        if (touch.phase != TouchPhase.Moved)
            return Vector3.zero;

        //delta
        var rayBefore = Camera.ScreenPointToRay(touch.position - touch.deltaPosition);
        var rayNow = Camera.ScreenPointToRay(touch.position);
        if (Plane.Raycast(rayBefore, out var enterBefore) && Plane.Raycast(rayNow, out var enterNow))
            return rayBefore.GetPoint(enterBefore) - rayNow.GetPoint(enterNow);

        //not on plane
        Debug.Log("Not on plane");
        return Vector3.zero;
    }

    protected Vector3 PlanePosition(Vector2 screenPos)
    {
        //position
        var rayNow = Camera.ScreenPointToRay(screenPos);
        if (Plane.Raycast(rayNow, out var enterNow))
            return rayNow.GetPoint(enterNow);

        return Vector3.zero;
    }

    public void OrbitCamera(Vector2 input)
    {
        Vector3 focusPoint = target.transform.position;
        const float e = 0.001f;

        Vector2 cameraAngle = Camera.transform.eulerAngles; 
		if (input.x < -e || input.x > e || input.y < -e || input.y > e) {
			orbitAngles += rotationSensitivity * Time.unscaledDeltaTime * input;
            cameraAngle += rotationSensitivity * Time.unscaledDeltaTime * input;
		}
        
        Quaternion lookRotation = Quaternion.Euler(orbitAngles);
		Vector3 lookDirection = lookRotation * Vector3.forward * Vector3.Distance(target.transform.position, Camera.transform.position);
		Vector3 lookPosition = focusPoint - lookDirection;
        // Camera.transform.SetPositionAndRotation(lookPosition, Quaternion.Euler(cameraAngle));
        Vector3 rotateAmount = new Vector3(input.x, input.y, 0.0f);
        // Camera.transform.rotation = Quaternion.Euler(Camera.transform.eulerAngles + rotationSensitivity * Time.unscaledDeltaTime * rotateAmount);
        Camera.transform.position = lookPosition;
    }

    public void ResetCamera()
    {
        Camera.transform.SetPositionAndRotation(DefaultPosition.transform.position, Quaternion.Euler(DefaultPosition.transform.eulerAngles));
    }
#endif
}
