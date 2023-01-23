using UnityEngine;
using UnityEngine.UI;

public class MuteTgl : MonoBehaviour {

    public Toggle muteToggle;

    void Awake()
    {

        muteToggle = GameObject.Find("MuteToggle").GetComponent<Toggle>();  //fix this line of code!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        if(PlayerPrefs.HasKey("Mute") == false)//this sets the default to music playing
        {
            PlayerPrefs.SetInt("Mute", 1);
            PlayerPrefs.Save();
        }

        if (PlayerPrefs.GetInt("Mute") == 0) //this sets the mute option to whatever it was set last.
        {
            muteToggle.isOn = true;
            AudioListener.pause = true;
        }
        else if (PlayerPrefs.GetInt("Mute") == 1)
        {
            muteToggle.isOn = false;
            AudioListener.pause = false;
        }
    }

    public void MuteOnClick()
    {
        if (muteToggle.isOn == false)
        {
            PlayerPrefs.SetInt("Mute", 1);
            PlayerPrefs.Save();
            AudioListener.pause = false;
        }
        else if (muteToggle.isOn)
        {
            PlayerPrefs.SetInt("Mute", 0);
            PlayerPrefs.Save();
            AudioListener.pause = true;
        }
    }

}
