using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonListControl : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;

    void Start() 
    {
        if (RealmController.Instance.isRealmReady())
        {
            Invoke("DisplayClimbs", 1f); // waits 1 sec to connect to database before calling DisplayClimbs
        }
       
    }

    public void DisplayClimbs()
    {
        if (RealmController.Instance.isRealmReady())
        {
            var climbs = RealmController.Instance.GetClimbData();

            foreach (var climb in climbs)
            {
                GameObject button = Instantiate(buttonTemplate);
                button.SetActive(true);

                button.GetComponent<ButtonListButton>().SetText(climb, button);

                button.transform.SetParent(buttonTemplate.transform.parent, false);
            }
        }
    }

    public void UpdateClimbs()
    {
        if (RealmController.Instance.isRealmReady())
        {
            var climb = RealmController.Instance.GetNewestClimbData();

            GameObject button = Instantiate(buttonTemplate);
            button.SetActive(true);

            button.GetComponent<ButtonListButton>().SetText(climb, button);

            button.transform.SetParent(buttonTemplate.transform.parent, false);
        }
    }
}
