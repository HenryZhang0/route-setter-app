using System;
using TMPro;
using UnityEngine;

public class AddNewClimb : MonoBehaviour
{
    public GameObject panel;
    private ButtonListControl buttonListControl;

    /*
    want to add: 
    sorting
    back button for myclimbs
    make sure only displays users (need to create authentication)
    edit, delete
    add climb route to button and display
    */
    public void AddClimb()
    {
        panel.SetActive(true);
        SetDefaultInputFeilds();
    }

    public void Add()
    {
        if (RealmController.Instance.isRealmReady())
        {
            buttonListControl = GameObject.FindGameObjectWithTag("ScrollClimbs").GetComponent<ButtonListControl>();

            var storeOutcomeText = GameObject.Find("StoreOutcomeText").GetComponent<TMP_Text>();

            try
            {
                var vScale = GetVScaleInput();
                var climbName = GetClimbNameInput();
                var date = GetDateInput();

                PlayerProfile_user_climbs newClimb = new PlayerProfile_user_climbs(vScale, climbName, date);
                RealmController.Instance.AddClimb(newClimb);

                buttonListControl.Invoke("UpdateClimbs", 1f); // need to update db
                ResetInputFields();
                Cancel();
            }
            catch (EmptyInputFieldException)
            {
                storeOutcomeText.text = "Input ALL Text Fields";
            }
            catch (Exception e)
            {
                Debug.LogError($"{e}, error adding new climb to database");
                storeOutcomeText.text = "did not store";
            }
        }
    }

    public void Cancel()
    {
        panel.SetActive(!panel.activeSelf);
        GameObject.Find("StoreOutcomeText").GetComponent<TMP_Text>().text = "";
    }

    private void SetDefaultInputFeilds()
    {
        GameObject.Find("DateInput").GetComponent<TMP_InputField>().placeholder.GetComponent<TMP_Text>().text = "YYYY/MM/DD";
    }

    private string GetVScaleInput()
    {
        var vScaleDropdown = GameObject.Find("VScaleDropDown").GetComponent<TMP_Dropdown>();
        return vScaleDropdown.options[vScaleDropdown.value].text;
    }
    private string GetClimbNameInput()
    {
        var climbNameInput = GameObject.Find("ClimbNameInput").GetComponent<TMP_InputField>().text;
        
        if (climbNameInput == "")
        {
            throw new EmptyInputFieldException();
        }
        return climbNameInput;
    }

    private string GetDateInput()
    {
        var dateInput = GameObject.Find("DateInput").GetComponent<TMP_InputField>().text;
        
        if (dateInput == "")
        {
            throw new EmptyInputFieldException();
        }
        return dateInput;
    }

    private void ResetInputFields()
    {
        var vScaleDropdown = GameObject.Find("VScaleDropDown").GetComponent<TMP_Dropdown>();
        var climbNameInput = GameObject.Find("ClimbNameInput").GetComponent<TMP_InputField>();
        var dateInput = GameObject.Find("DateInput").GetComponent<TMP_InputField>();

        vScaleDropdown.value = 0;
        climbNameInput.text = "";
        dateInput.text = "";
    }

    [Serializable] // MOVE
    public class EmptyInputFieldException : Exception { }
}

