using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class testTgl : MonoBehaviour
{
    public Toggle muteToggle;

    public void TestOnClick()
    {
        Debug.Log("Hello?");
        if (muteToggle.isOn == false)
        {
            /*
            PlayerPrefs.SetInt("Mute", 1);
            PlayerPrefs.Save();
            AudioListener.pause = false;
            */
            //Debug.Log("test OFF");
        }
        else if (muteToggle.isOn == true)
        {
            
            /*PlayerPrefs.SetInt("Mute", 0);
            PlayerPrefs.Save();
            AudioListener.pause = true;
            */
            //Debug.Log("test ON");
        }
    }
}
