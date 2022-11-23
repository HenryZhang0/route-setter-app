using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorManager : MonoBehaviour
{
    public ItemController[] ItemButtons;
    public GameObject[] ItemPrefabs;
    public GameObject[] ItemImage;
    public GameObject Wall;
    public int CurrentButtonPressed;

    Ray ray;
    RaycastHit hit;
    // Update is called once per frame
    void Update()
    {
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider.tag != "Wall") {
                return;
            }

            if(Input.GetMouseButtonDown(0) && ItemButtons[CurrentButtonPressed].Clicked) {
                ItemButtons[CurrentButtonPressed].Clicked = false;
                Instantiate(ItemPrefabs[CurrentButtonPressed], hit.point, Quaternion.identity);
                Destroy(GameObject.FindGameObjectWithTag("ItemImage"));
                
            }
        }
    }

    public void SpawnWall()
    {
        Instantiate(Wall, Vector3.zero, new Quaternion(-90,0,0,90));
    }
}
