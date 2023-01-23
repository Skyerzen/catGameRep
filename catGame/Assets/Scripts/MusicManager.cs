using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    private AudioSource musicSource;
    private AudioSource clearSource;
    private AudioSource foundSource;

    public Toggle musicManToggle;

    void Awake()
    {
        AudioSource[] musicArray = GetComponents<AudioSource>();
        musicSource = musicArray[0];
        clearSource = musicArray[1];
        foundSource = musicArray[2];


        if (PlayerPrefs.GetInt("Music") == 0) //this sets the music option to whatever it was set last
        {
            musicManToggle.isOn = true;
            musicSource.mute = false;
            clearSource.mute = false;
            foundSource.mute = false;
        }
        else if (PlayerPrefs.GetInt("Music") == 1)
        {
            musicManToggle.isOn = false;
            musicSource.mute = true;
            clearSource.mute = true;
            foundSource.mute = true;
        }
    }

    void playGameOver()
    {
        musicSource.Stop();
        foundSource.Play();
    }

    void playCleared()
    {
        musicSource.Stop();
        clearSource.Play();
    }



    public void MusicOnClick()
{
    if (musicManToggle.isOn == false)
    {
        PlayerPrefs.SetInt("Music", 1);
        PlayerPrefs.Save();
        musicSource.mute = true;
        clearSource.mute = true;
        foundSource.mute = true;
    }
    else if (musicManToggle.isOn)
    {
        PlayerPrefs.SetInt("Music", 0);
        PlayerPrefs.Save();
        musicSource.mute = false;
        clearSource.mute = false;
        foundSource.mute = false;
    }
}


}
