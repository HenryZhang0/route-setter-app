using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    // public TextAsset jsonFile;
 
    // void Start()
    // {
    //     Wall Walls = JsonUtility.FromJson<Wall>(jsonFile.text);
 
    //     foreach (Wall wall in Walls.wall)
    //     {
    //         Instantiate Wall(wall.x, wall.y, wall.scale);
    //     }

    //     foreach (Hold hold in Holds.hold)
    //     {
    //         Instantiate Hold(hold.x, hold.y, hold.scale);
    //     }

    //     Climb climb = new Climb(Walls, Holds);
    // }
}

public class Climb
{
    // Metadata
    public int Grade = 0;
    public string Date = "Mon Nov 14, 2022";

    // Geometry data
    public List <Wall> Walls;
    public List <Hold> Holds;

    

    public void AddHold(Hold h) {
        Holds.Add(h);
        // instantiate h;
    }

    public void DeleteHold(Hold h) {
        Holds.Remove(h);
    }

}

public class Wall
{
    public Vector3[] Corners;
    public Color Colour;
}

public class Hold
{
    public Vector3 position = new Vector3(0,0,0);
    public float scale = 1;
    public Quaternion rotation = Quaternion.identity;
    public Color Colour;
}