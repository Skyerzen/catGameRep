using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class reverseOption : MonoBehaviour {

    float reverse; //this turns the controller into reverse controller
    public Toggle revToggle;

    void Awake () {
        if (PlayerPrefs.HasKey("Rev") == false)//this if statement creates a new key called REV to store the reverse value
        {
            PlayerPrefs.SetInt("Rev", 0);//default to 0, or OFF
            PlayerPrefs.Save();
        }


        if (PlayerPrefs.GetInt("Rev") == 1) 
        {
            Debug.Log("on awake, reverse is ON in playerprefs so setting the option button to off?");
            revToggle.isOn = false; //if the rev option is on in playerprefs, shouldn't this button be set to on as well?
        }
        else if (PlayerPrefs.GetInt("Rev") == 0)
        {
            Debug.Log("on awake, reverse is off in playerprefs so setting the option button to ON?");
            revToggle.isOn = true; //if the rev option is set to off in the playerprefs, shouldn't this be setting the button to off as well?
        }
    }

    public void ReverseOnClick()
    {
        if (revToggle.isOn == false)
        {
            Debug.Log("on click, reverse button was OFF so setting the playerprefs to ON");
            PlayerPrefs.SetInt("Rev", 1);
            PlayerPrefs.Save();
        }
        else if (revToggle.isOn)
        {
            Debug.Log("on click, reverse button was ON so setting the playerprefs to OFF");
            PlayerPrefs.SetInt("Rev", 0);
            PlayerPrefs.Save();
        }
    }


}
