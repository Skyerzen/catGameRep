using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class deletePrefs : MonoBehaviour {

public void DelPrefsOnClick()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
