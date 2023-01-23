using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class timerScript : MonoBehaviour
{
    public Text timerText;
    public float secondsCount;
    public int minuteCount;
    public float restartDelay = 9f;         // Time to wait before restarting the level
    Animator anim;                          // Reference to the animator component.
    float restartTimer;
    public GameObject GoalObj;

    void Awake()
    {
        anim = GetComponent<Animator>();
        GoalObj = GameObject.FindGameObjectWithTag("exit");
    }

    void Update()
    {
        if (GoalObj.GetComponent<ExitLevel>().goalReached == true)
        {
            return;
        }
        if (minuteCount >= 5)
        {
            //restart game after 5 minutes
            Restart();
            return;
        }
        UpdateTimerUI();
    }

    //call this on update
    public void UpdateTimerUI()
    {
        //set timer UI
        secondsCount += Time.deltaTime;
        timerText.text = minuteCount + ":" + (int)secondsCount;
        if(secondsCount <= 10)//adds a 0 to the seconds counter for visual only
        {
            timerText.text = minuteCount + ":" + "0" + (int)secondsCount;
        }

        if (secondsCount >= 60)
        {
            minuteCount++;
            secondsCount = 0;
        }
        if (minuteCount >= 4)
        {
            timerText.color = Color.yellow;
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
}

