using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ReturnMenuOnClick : MonoBehaviour
{

    public void GotoMainMenu()
    {
        SceneManager.LoadScene("menuSelect");
    }
}