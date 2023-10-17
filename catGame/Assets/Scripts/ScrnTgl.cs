using UnityEngine;
//using System.Collections;
using UnityEngine.UI;

public class ScrnTgl : MonoBehaviour {

    public Toggle toggle;

    void Awake()
    {
        if (PlayerPrefs.HasKey("Scrn") == false)
        {
            PlayerPrefs.SetInt("Scrn", 0);
        }

            if (PlayerPrefs.GetInt("Scrn") == 0)
        {
            toggle.isOn = false;
        }
        else if (PlayerPrefs.GetInt("Scrn") == 1)
        {
            toggle.isOn = true;
        }
    }

    public void ScrnOnClick()
    {
        if (toggle.isOn)
        {
            PlayerPrefs.SetInt("Scrn", 1);
            PlayerPrefs.Save();
        }
        else if (toggle.isOn == false)
        {
            PlayerPrefs.SetInt("Scrn", 0);
            PlayerPrefs.Save();
        }
    }
}
