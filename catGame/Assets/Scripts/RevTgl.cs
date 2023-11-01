using UnityEngine;
//using System.Collections;
using UnityEngine.UI;

public class RevTgl : MonoBehaviour {

    public bool reversa;
    public Toggle toggle;

    void Start()
    {
        if (PlayerPrefs.HasKey("Rev") == false)//this checks to see if there's a key in the player prefs called rev
        {
            PlayerPrefs.SetInt("Rev", 0);//this creates a new key called Rev and sets its default to 0
        }
        else
        {

            if (PlayerPrefs.GetInt("Rev") == 0)
            {
                Debug.Log(toggle.name + " on awake, rev was set to OFF " + PlayerPrefs.GetInt("Rev") + " in the playerprefs, setting the button in the main menu to OFF");
                toggle.isOn = false;
            }
            else if (PlayerPrefs.GetInt("Rev") == 1)
            {
                Debug.Log(toggle.name + " on awake, rev was set to ON " + PlayerPrefs.GetInt("Rev") + " in the playerprefs, setting the button in the main menu to ON");
                toggle.isOn = true;
            }
        }
    }

    public void RevOnClick()
    {
        if (toggle.isOn == true)
        {
            Debug.Log("On click! " + toggle.name + " Turns Reverse from ON to OFF!: " + PlayerPrefs.GetInt("Rev"));
            PlayerPrefs.SetInt("Rev", 1);
            PlayerPrefs.Save();
            reversa = true;
            //add code here that will change the reverse variable in the playermovement code
        }
        else if (toggle.isOn == false)
        {
            Debug.Log("one click! " + toggle.name + " Turns Reverse from OFF to ON!: " + PlayerPrefs.GetInt("Rev"));
            PlayerPrefs.SetInt("Rev", 0);
            PlayerPrefs.Save();
            reversa = false;
            //add code here that will change the reverse variable in the playermovement code
        }
    }
}
