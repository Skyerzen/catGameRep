using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class GamePauser : MonoBehaviour {

    public GameObject options;

    void Start()
    {
        options = GameObject.Find("Options");
        options.SetActive(false);
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log("esc pressed");
            if(Time.timeScale == 1)
            {
                options.SetActive(true);
                Time.timeScale = 0;
                
            }
            else if(Time.timeScale == 0)
            {
                Time.timeScale = 1;
                options.SetActive(false);
            }
        }
	}
    public void ResumeOnClick()
    {
        Time.timeScale = 1;
        options.SetActive(false);
    }
}
