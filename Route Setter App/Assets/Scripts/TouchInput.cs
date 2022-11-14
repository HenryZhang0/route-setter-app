using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TouchInput : MonoBehaviour
{
#if UNITY_IOS || UNITY_ANDROID
    public Camera Camera;
    public GameObject target;
    public GameObject p;

    protected Plane Plane;
    public bool Rotate;

    private void Awake()
    {
        if (Camera == null)
            Camera = Camera.main;
    }

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

        var Delta1 = Vector3.zero;
        var Delta2 = Vector3.zero;

        //Scroll
        if (Input.touchCount >= 1)
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.up);
    }
#endif
}

// {
//     public Camera camera;
//     public GameObject target;
//     public float rotationSpeed = 1f;
//     Vector3 focusPoint;
//     Vector2 orbitAngles = new Vector2(45f, 0f);
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         focusPoint = target.transform.position;
//         OrbitControls();

//     }


//     private void OrbitControls()
//     {
//         if(Input.touchCount != 1) return;

//         Vector2 input = Input.GetTouch(0).deltaPosition;
//         input = new Vector2(input.y*-1, input.x);
// 		input *= rotationSpeed;
// 		OrbitCamera(input);
//     }

//     public void OrbitCamera(Vector2 input)
//     {
//         const float e = 0.001f;
// 		if (input.x < -e || input.x > e || input.y < -e || input.y > e) {
// 			orbitAngles += rotationSpeed * Time.unscaledDeltaTime * input;
// 		}
        
//         Quaternion lookRotation = Quaternion.Euler(orbitAngles);
// 		Vector3 lookDirection = lookRotation * Vector3.forward;
// 		Vector3 lookPosition = focusPoint - lookDirection;
//         camera.transform.SetPositionAndRotation(lookPosition, lookRotation);
//     }
// }
