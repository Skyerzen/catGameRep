using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class reverseOption : MonoBehaviour {

    float reverse; //this turns the controller into reverse controller
    public Toggle revToggle;

    void Awake () {
        if (PlayerPrefs.HasKey("Rev") == false)//this sets the default to music playing
        {
            PlayerPrefs.SetInt("Rev", 0);
            PlayerPrefs.Save();
        }


        if (PlayerPrefs.GetInt("Rev") == 1) //this sets the reverse option box to whatever it was set last.
        {
            revToggle.isOn = false;
        }
        else if (PlayerPrefs.GetInt("Rev") == 0)
        {
            revToggle.isOn = true;
        }
    }

    public void ReverseOnClick()
    {
        if (revToggle.isOn == false)
        {
            PlayerPrefs.SetInt("Rev", 1);
            PlayerPrefs.Save();
        }
        else if (revToggle.isOn)
        {
            PlayerPrefs.SetInt("Rev", 0);
            PlayerPrefs.Save();
        }
    }


}
