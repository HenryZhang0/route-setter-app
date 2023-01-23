using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    // public TextAsset jsonFile;
 
    void Start()
    {
        // RouteData routeData = JsonUtility.FromJson<RouteData>(jsonFile.text);

    }

    public void LoadRoute(RouteData routeData)
    {
        foreach (Wall wall in routeData.Walls)
        {
            // Instantiate(Wall, wall.x, wall.y, wall.scale);
        }

        foreach (Hold hold in routeData.Holds)
        {
            // Instantiate(Hold, hold.x, hold.y, hold.scale);
        }
    }
}

public class Wall
{
    public Vector3 position = new Vector3(0,0,0);
    public float scale = 1;
    public Quaternion rotation = Quaternion.identity;
    public Vector3[] Corners;
    public Color Colour;
}

