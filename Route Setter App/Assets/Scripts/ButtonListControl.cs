using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonListControl : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;
    private DatabaseAccess databaseAccess;

    void Start() 
    {
        databaseAccess = GameObject.FindGameObjectWithTag("DatabaseAccess").GetComponent<DatabaseAccess>();

        Invoke("DisplayClimbs", 1f); // waits 1 sec to connect to database before calling DisplayClimbs
    }

    private async void DisplayClimbs()
    {
        var task = databaseAccess.GetClimbsFromDataBase();
        var result = await task;
        
        foreach (var climb in result)
        {
            GameObject button = Instantiate(buttonTemplate);
            button.SetActive(true);

            button.GetComponent<ButtonListButton>().SetText(climb);

            button.transform.SetParent(buttonTemplate.transform.parent, false);
        }
    }
}
