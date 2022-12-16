using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorManager : MonoBehaviour
{
    // Menu stuff
    public ItemController[] ItemButtons;
    public GameObject[] ItemPrefabs;
    public GameObject[] ItemImage;
    public GameObject Wall;
    public int CurrentButtonPressed;

    // Hold settings
    private GameObject GameObjectHit;

    // Sliders
    Slider ScaleSlider;
    Slider RotationSlider;
    GameObject DeleteButton;

    Ray ray;
    RaycastHit hit;

    void Start()
    {
        // Sliders
        ScaleSlider = GameObject.Find("ScaleSlider").GetComponent<Slider>();
        RotationSlider = GameObject.Find("RotationSlider").GetComponent<Slider>();
        DeleteButton = GameObject.Find("Trash");
        ScaleSlider.gameObject.SetActive(false);
        RotationSlider.gameObject.SetActive(false);
        DeleteButton.gameObject.SetActive(false);
    }

    void Update()
    {
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(0))
        {
            if(hit.collider.tag == "Wall") {
                if(ItemButtons[CurrentButtonPressed].Clicked) {
                    ItemButtons[CurrentButtonPressed].Clicked = false;
                    Instantiate(ItemPrefabs[CurrentButtonPressed], hit.point, Quaternion.identity);
                    Destroy(GameObject.FindGameObjectWithTag("ItemImage"));
                    
                }
            }
            if(hit.collider.tag == "Hold") {
                Debug.Log("Selected Hold " + hit.transform.gameObject.name);
                if (GameObjectHit) GameObjectHit.GetComponent<Outline>().enabled = false;
                GameObjectHit = hit.transform.gameObject;
                GameObjectHit.GetComponent<Outline>().enabled = true;
                ControlsVisible(true);
            } else {
                GameObjectHit.GetComponent<Outline>().enabled = false;
                GameObjectHit = null;
                ControlsVisible(false);
            }

        }
    }

    public void ControlsVisible(bool visible) {
        ScaleSlider.gameObject.SetActive(visible);
        RotationSlider.gameObject.SetActive(visible);
        DeleteButton.gameObject.SetActive(visible);
    }

    public void Rotate(float amnt) {
        GameObjectHit.GetComponent<Hold>().Rotate(amnt);
    }

    public void Scale(float amnt) {
        GameObjectHit.GetComponent<Hold>().Scale(amnt);
    }

    public void Delete() {
        Destroy(GameObjectHit);
        ControlsVisible(false);
    }

    public void SpawnWall()
    {
        Instantiate(Wall, Vector3.zero, new Quaternion(-90,0,0,90));
    }
}
