using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NextLevelOnClick : MonoBehaviour
{
    int CurrentScene;
    public void LoadNextScene()
    {
        CurrentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentScene + 1);
    }
}