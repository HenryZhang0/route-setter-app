using System.Collections;
using System.Collections.Generic;
using UnityEditor.Sprites;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListButton : MonoBehaviour
{
    public void SetText(PlayerProfile_user_climbs climbData, GameObject button)
    {
        Text[] texts = button.GetComponentsInChildren<Text>();

        texts[0].text = climbData.VGrade;
        texts[1].text = climbData.ClimbName;
        texts[2].text = climbData.DateCompleted;
    }

    public void SetTextFriend(string username, PlayerProfile_user_climbs climbData, GameObject button)
    {
        Text[] texts = button.GetComponentsInChildren<Text>();

        texts[0].text = username;
        texts[1].text = climbData.VGrade;
        texts[2].text = climbData.ClimbName;
        texts[3].text = climbData.DateCompleted;

    }
}
