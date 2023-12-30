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
            Debug.Log("Scn Created");
            PlayerPrefs.SetInt("Scn", 0);//this creates a new key called Rev and sets its default to 0 if one doesn't exist
        }
        //if the above statement is true, then the Scn pref is left alone

        if (PlayerPrefs.GetInt("Scn") == 0) //this checks to see if the pref off, if Off, it then sets the toggle button to off to reflect the state of the pref value
        {
            toggle.isOn = false;               //here is the toggle being set to off
            pantalla = false;                  //this is the variable that gets sent to playerMovement to then change the controls
            Debug.Log("poop! I'm running at the beginning of the level! " + "Scn:" + PlayerPrefs.GetInt("Scn") + "   toggle:" + toggle.isOn + "  pantalla:" + pantalla);
        }
        else if (PlayerPrefs.GetInt("Scn") == 1) //if the pref is set to ON, then the toggle button is set to match.
        {
            toggle.isOn = true;                 //this is the toggle being set to ON
            pantalla=true;                      //this is the variable that gets sent to playerMovement.
            Debug.Log("am I running too??? " + "Scn:" + PlayerPrefs.GetInt("Scn") + "   toggle:" + toggle.isOn + "  pantalla:" + pantalla);

        }
    }

    public void ScrnOnClick()
    {
        if (toggle.isOn == true)
        {
            PlayerPrefs.SetInt("Scn", 1);
            PlayerPrefs.Save();
            pantalla = true;
            Debug.Log("click on!! " + "Scn:" + PlayerPrefs.GetInt("Scn") + "   pantalla:" + pantalla);
            //add code here that will change the reverse variable in the playermovement code
        }
        else if (toggle.isOn == false)
        {
            PlayerPrefs.SetInt("Scn", 0);
            PlayerPrefs.Save();
            pantalla = false;
            Debug.Log("click OFF!! " + "Scn:" + PlayerPrefs.GetInt("Scn") + "   pantalla:" + pantalla);
            //add code here that will change the reverse variable in the playermovement code
        }
    }
}