using UnityEngine;
using System.Collections;

public class guardDogSounds : MonoBehaviour
{
    private AudioSource barkSource;
    private AudioSource stepSource;

    void Awake()
    {
        AudioSource[] soundArray = GetComponents<AudioSource>();//gets the audio to be played
        barkSource = soundArray[0];
        stepSource = soundArray[1];
    }

    void playStep()
    {
        stepSource.Stop();
        stepSource.Play();
    }

    void playBark()//plays the bark. Manual loop override.
    {
        barkSource.Play();
    }
}
