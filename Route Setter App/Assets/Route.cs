using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    // Metadata
    public int Grade = 0;
    public string Date = "Mon Nov 14, 2022";

    // Geometry data
    public Wall[] Walls;

}

public class RouteX
{
    public Wall[] Walls;
    public Hold[] Holds;

}

public class Wall
{
    public Vector3[] Corners;
    public Color Colour;
}

public class Hold
{
    public Vector3 position = new Vector3(0,0,0);
    public Color Colour;
}