using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public int ID;
    public int quantity;
    public bool Clicked = false;
    private LevelEditorManager editor;

    Ray ray;
    RaycastHit hit;
    void Start()
    {
        editor = GameObject.FindGameObjectWithTag("LevelEditorManager").GetComponent<LevelEditorManager>();
    }

    public void ButtonClicked()
    {
        Clicked = true;
        editor.CurrentButtonPressed = ID;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            Instantiate(editor.ItemImage[ID], hit.point, Quaternion.identity);
        }
    }
}
