using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonListControlFriend : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;

    public void Start()
    {
        if (RealmController.Instance.isRealmReady())
        {
            Invoke("DisplayFriendsClimbs", 1f); // waits 1 sec to connect to database before calling DisplayClimbs
        }

    }

    public void DisplayFriendsClimbs()
    {
        if (RealmController.Instance.isRealmReady())
        {
            var players = RealmController.Instance.GetFriendsData();

            foreach (var player in players)
            {
                foreach (var climb in player.UserClimbs)
                {
                    GameObject button = Instantiate(buttonTemplate);
                    button.SetActive(true);

                    button.GetComponent<ButtonListButton>().SetTextFriend(player.Username, climb, button);

                    button.transform.SetParent(buttonTemplate.transform.parent, false);
                }
            }
        }
    }
}
