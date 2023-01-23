using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteManager : MonoBehaviour
{
    // Metadata
    public int Grade = 0;
    public string Date = "Mon Nov 14, 2022";

    // Geometry data
    public List <Wall> Walls;
    public List <Hold> Holds;

    

    // public void AddHold(Hold h) {
    //     Instantiate (h, h.position, h.rotation);
    //     Holds.Add(h);
    // }

    // public void DeleteHold(Hold h) {
    //     Holds.Remove(h);
    // }

    // public void AddWall(Wall w) {
    //     Instantiate (Wall, w.position, w.rotation);
    //     Walls.Add(w);
    // }

    // public void DeleteWall(Hold h) {
    //     Holds.Remove(h);
    // }

    // public void LoadRoute(Wall[] walls, Hold[] holds) { // TEMPLATE CODE

    //     foreach (Wall wall in walls) {
    //         AddWall(wall);
    //     }

    //     foreach (Hold hold in holds) {
    //         AddHold(hold);
    //     }
    // }
}
