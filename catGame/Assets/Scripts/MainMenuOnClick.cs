using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuOnClick : MonoBehaviour
{
    private int currentLevel;
    public GameObject goal;

    void Awake()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex -1;
        goal = GameObject.FindGameObjectWithTag("exit");
    }

    public void LoadMainMenu()
    {
        if (goal.GetComponent<ExitLevel>().goalReached == false && ProgressSaver.scoreSaver.lvlScores[currentLevel] == " ")
        {
            ProgressSaver.scoreSaver.lvlScores[currentLevel] = "D";
            ProgressSaver.scoreSaver.Save();
        }
        Time.timeScale = 1;
        SceneManager.LoadScene("menuSelect");

    }
}