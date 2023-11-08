using UnityEngine;
using UnityEngine.UI;

public class MusicTgl : MonoBehaviour {

    private AudioSource musicSource;

    public Toggle musicToggle;

    void Awake()
    {
        AudioSource musicArray = GetComponent<AudioSource>();
        musicSource = musicArray;


        if (PlayerPrefs.GetInt("Music") == 0) //this sets the music option to whatever it was set last
        {
            musicToggle.isOn = true;
            musicSource.mute = false;
        }
        else if (PlayerPrefs.GetInt("Music") == 1)
        {
            musicToggle.isOn = false;
            musicSource.mute = true;
        }
    }

    public void MusicOnClick()
    {
        if (musicToggle.isOn == false)
        {
            PlayerPrefs.SetInt("Music", 1);
            PlayerPrefs.Save();
            musicSource.mute = true;
            //Debug.Log("music OFF");
        }
        else if (musicToggle.isOn)
        {
            PlayerPrefs.SetInt("Music", 0);
            PlayerPrefs.Save();
            musicSource.mute = false;
            //Debug.Log("music ON");
        }
    }
}
