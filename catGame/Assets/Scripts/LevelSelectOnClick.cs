using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelSelectOnClick : MonoBehaviour
{
    public int levelNumber;
    public void LoadScene()
    {
        SceneManager.LoadScene(levelNumber);
    }
}
