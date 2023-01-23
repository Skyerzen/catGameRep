using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProgressSaver : MonoBehaviour
{
    public static ProgressSaver scoreSaver;
    public int totalLvls;
    public string[] lvlScores;
    public bool[] lvlUnlocks;
    public GameObject[] buttonsObj;
    public Button[] buttons;
    public GameObject[] gradesObj;
    public Text[] grades;

    void Awake()
    {
        totalLvls = SceneManager.sceneCountInBuildSettings; //get the total number of levels in the game.
        buttonsObj = GameObject.FindGameObjectsWithTag("lvlBttn"); //gather all of the objects in the scene with the button tag.
        gradesObj = GameObject.FindGameObjectsWithTag("lvlGrd");  //gather all of the objects in the scene with the grade tag.
        buttons = new Button[buttonsObj.Length];
        grades = new Text[gradesObj.Length];


        if (scoreSaver == null) //singleton script for scoreKeeper
        {
            DontDestroyOnLoad(gameObject);
            scoreSaver = this;
        }
        else if (scoreSaver != this)
        {
            Destroy(gameObject);
        }

        int i = 0;

        /////////////////////////////////////////////
        foreach (GameObject element in buttonsObj) //getting the button component from each gameobject
        {
            buttons[i] = element.GetComponent<Button>();
            i++;
        }

        i = 0;

        //////////////////////////////////////////////
        foreach (GameObject element in gradesObj) //getting the text component from each gameobject
        {
            grades[i] = element.GetComponent<Text>();
            i++;
        }

        //sorts the buttons by name so they are ordered correctly
        Array.Sort(buttons, delegate (Button bttn1, Button bttn2) { return bttn1.name.CompareTo(bttn2.name); });
        Array.Sort(grades, delegate (Text grade1, Text grade2) { return grade1.name.CompareTo(grade2.name); });
  
        if (File.Exists(Application.persistentDataPath + "/progressData.dat")) //this code finds out if the saved file exists and does things appropiately.
        {
            Load();
        }
        else
        {
            Create();
        }
        Labeler();
        Unlocker();
    }

    public void Unlocker()
    {
        for (int i = 0; i < totalLvls; i++)
        {
            buttons[i].interactable = lvlUnlocks[i];
        }
    }
    public void Labeler()
    {
        for (int i = 0; i < totalLvls; i++)
        {
            grades[i].text = lvlScores[i];

        }
    }

    public void Create()
    {   
        lvlScores = new string[totalLvls];
        for(int i = 0; i < lvlScores.Length; i++)
        {
            lvlScores[i] = " ";
        }
        
        lvlUnlocks = new bool[totalLvls];
        for(int i = 0; i < lvlUnlocks.Length; i++)
        {
            if(i >= 1)
            {
                lvlUnlocks[i] = false;
            }
            else
            {
                lvlUnlocks[i] = true;
            }
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/progressData.dat");

        GameData data = new GameData();

        data.scores = lvlScores;
        data.unlocks = lvlUnlocks;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/progressData.dat");

        GameData data = new GameData();

        data.scores = lvlScores;
        data.unlocks = lvlUnlocks;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/progressData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/progressData.dat", FileMode.Open);
            GameData data = (GameData)bf.Deserialize(file);
            file.Close();

            lvlScores = data.scores;
            lvlUnlocks = data.unlocks;
        }
    }
}

[Serializable]
class GameData
{
    public string[] scores;
    public bool[] unlocks;
}