using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameOverManager : MonoBehaviour
{
    public GameObject[] Enemies;            //this one holds all of the enemies
    public GameObject GoalObj;              //this holds the goal object
    public float restartDelay = 9f;         // Time to wait before restarting the level
    Animator anim;                          // Reference to the animator component.
    float restartTimer;                     // Timer to count up to restarting the level
    public bool seen = false;               //if the character has been seen
    public Text gText;                      //this is the score card
    public GameObject PowObj;               //this is the power up
    public float timeA;                     //part of the timer
    public float timeB;                     //part of the timer
    public int currentLevel;                //the index number of the current level.

    //functions
    void CheckIfPlayerFound()//maybe all other scripts can read seen instead
    {
        foreach (GameObject currentEnemy in Enemies)
        {
            if (currentEnemy.GetComponent<Detection>().seeyou) //Just to avoid confusion in the future, this script goes through all the Detection scripts running in the scene...
            {
                seen = true;
                if (seen == true)
                {
                    Restart();
                }
            }
        }
    }

    void Restart()
    {
        // this animates the game over screen.
        anim.SetTrigger("GameOverTrig");

        // .. increment a timer to count up to restarting.
        restartTimer += Time.deltaTime;

        // .. if it reaches the restart delay...
        if (restartTimer >= restartDelay) //The level will restart on it's own, but there's also a button.
        {
            // .. then reload the currently loaded level.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //new way of reloading level.
        }
    }

    void saveProgress()
    {
        if(ProgressSaver.scoreSaver.lvlScores[currentLevel]== " ")
        {
            ProgressSaver.scoreSaver.lvlScores[currentLevel] = "D";
        }
        if (string.Compare(gText.text, ProgressSaver.scoreSaver.lvlScores[currentLevel]) <= 0)//this just makes sure high scores are not overwritten by lower scores.
        {
            ProgressSaver.scoreSaver.lvlScores[currentLevel] = gText.text;
        }
        //ProgressSaver.scoreSaver.lvlUnlocks[currentLevel +1] = true;
        if(currentLevel != ProgressSaver.scoreSaver.totalLvls -1) //This makes sure you cannot go past the last level. this is only temporary. In the final game, the last level will be a movie level.
        {
            Debug.Log("current Level " + currentLevel);
            Debug.Log("Total Levels " + (ProgressSaver.scoreSaver.totalLvls -1));
            ProgressSaver.scoreSaver.lvlUnlocks[currentLevel + 1] = true;
        }

        ProgressSaver.scoreSaver.Save();
    }


    void GoalClear()
    {
        float timeStamp = (gameObject.GetComponent<timerScript>().minuteCount*60f + (int)gameObject.GetComponent<timerScript>().secondsCount);

        if (timeStamp <= timeA & PowObj.activeSelf == false)//quickest time and with can
        {
            gText.text = "A";
        }
        else if (timeStamp <= timeA || timeStamp >= timeA & timeStamp <= timeB & PowObj.activeSelf == false)//quickest time with no can OR second quickest time with can.
        {
            gText.text = "B";
        }
        else if(timeStamp >=timeA & timeStamp <= timeB || timeStamp >= timeB & PowObj.activeSelf == false)//second quickest time with no can OR slowest time with can.
        {
            gText.text = "C";
        }
        else if(timeStamp >= timeB)//slowest time no can.
        {
            gText.text = "D";
        }

        saveProgress();
        anim.SetTrigger("LevelClearTrig");
    }

    void Awake()
    {
        //TO DO: Maybe try to achieve some of this functionality with static functions instead of the findObject
        anim = GetComponent<Animator>();
        Enemies = GameObject.FindGameObjectsWithTag("enemy");
        GoalObj = GameObject.FindGameObjectWithTag("exit");
        PowObj = GameObject.FindGameObjectWithTag("powerUp");
        currentLevel = SceneManager.GetActiveScene().buildIndex -1;
    }

    void Update()
    {
        if (GoalObj.GetComponent<ExitLevel>().goalReached == true)
        {
            GoalClear();
        }
        else
        {
            CheckIfPlayerFound();
        }
    }
}