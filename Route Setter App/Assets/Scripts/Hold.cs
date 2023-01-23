using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hold : MonoBehaviour
{
    public Vector3 Position = new Vector3(0,0,0);
    public float Size = 1;
    public float Rotation = 0;


    Bounds bounds;
    void Start()
    {
        bounds = GetComponent<MeshFilter>().mesh.bounds;
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = Position;
        transform.localScale = new Vector3(Size/bounds.size.x, Size/bounds.size.x, Size/bounds.size.x);
        transform.rotation = Quaternion.Euler(new Vector3(0,0,Rotation*360));
    }

    public void Scale(float amnt) {
        Size = amnt;
    }

    public void Rotate(float amnt) {
        Rotation = amnt;
    }
}

// public class Hold
// {
//     public Vector3 position = new Vector3(0,0,0);
//     public float scale = 1;
//     public Quaternion rotation = Quaternion.identity;
//     public Color Colour;


// }