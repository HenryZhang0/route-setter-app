using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListButton : MonoBehaviour
{
    [SerializeField]
    private Text myText;

    public void SetText(ClimbData climbData)
    {
        // will fix, temp solution
        myText.text = string.Format(" {0,-40}{1,-65}{2,20} ", climbData.vGrade, climbData.climbName, climbData.dateCompleted);
    }
}
