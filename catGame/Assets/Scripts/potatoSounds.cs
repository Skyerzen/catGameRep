using UnityEngine;
using System.Collections;

public class potatoSounds : MonoBehaviour
{
    private AudioSource hissSource;
    private AudioSource walkSource;

    void Awake()
    {
        AudioSource[] soundArray = GetComponents<AudioSource>();//gets the audio to be played
        hissSource = soundArray[0];
        walkSource = soundArray[1];
    }

    void playWalk()
    {
        walkSource.Stop();
        walkSource.Play();
    }

    void playHiss()//plays the bark. Manual loop override.
    {
        hissSource.Stop();
        hissSource.Play();
    }
}
