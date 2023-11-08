using UnityEngine;
//using System.Collections;
using UnityEngine.UI;

public class ScrnTgl : MonoBehaviour {

    public bool pantalla;
    public Toggle toggle;

    void Start()
    {
        if (PlayerPrefs.HasKey("Scn") == false)//this checks to see if there's a key in the player prefs called rev
        {
            PlayerPrefs.SetInt("Scn", 0);//this creates a new key called Rev and sets its default to 0
        }
        else
        {

            if (PlayerPrefs.GetInt("Scn") == 0)
            {
                //Debug.Log(toggle.name + " on awake, scn was set to OFF " + PlayerPrefs.GetInt("Scn") + " in the playerprefs, setting the button in the main menu to OFF");
                toggle.isOn = false;
            }
            else if (PlayerPrefs.GetInt("Scn") == 1)
            {
                //Debug.Log(toggle.name + " on awake, Scn was set to ON " + PlayerPrefs.GetInt("Scn") + " in the playerprefs, setting the button in the main menu to ON");
                toggle.isOn = true;
            }
        }
    }

    public void ScrnOnClick()
    {
        if (toggle.isOn == true)
        {
            //Debug.Log("On click! " + toggle.name + " Turns Screen from ON to OFF!: " + PlayerPrefs.GetInt("Scn"));
            PlayerPrefs.SetInt("Scn", 1);
            PlayerPrefs.Save();
            pantalla = false;
            //add code here that will change the reverse variable in the playermovement code
        }
        else if (toggle.isOn == false)
        {
            //Debug.Log("one click! " + toggle.name + " Turns Screen from OFF to ON!: " + PlayerPrefs.GetInt("Scn"));
            PlayerPrefs.SetInt("Scn", 0);
            PlayerPrefs.Save();
            pantalla = true;
            //add code here that will change the reverse variable in the playermovement code
        }
    }
}