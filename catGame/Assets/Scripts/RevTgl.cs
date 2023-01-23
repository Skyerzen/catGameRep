using UnityEngine;
//using System.Collections;
using UnityEngine.UI;

public class RevTgl : MonoBehaviour {

    public Toggle toggle;

    void Awake()
    {
        if (PlayerPrefs.HasKey("Rev") == false)
        {
            PlayerPrefs.SetInt("Rev", 0);
        }

            if (PlayerPrefs.GetInt("Rev") == 0)
        {
            toggle.isOn = false;
        }
        else if (PlayerPrefs.GetInt("Rev") == 1)
        {
            toggle.isOn = true;
        }
    }

    public void RevOnClick()
    {
        if (toggle.isOn)
        {
            PlayerPrefs.SetInt("Rev", 1);
            PlayerPrefs.Save();
        }
        else if (toggle.isOn == false)
        {
            PlayerPrefs.SetInt("Rev", 0);
            PlayerPrefs.Save();
        }
    }
}
