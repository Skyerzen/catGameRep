using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;

public class DeleteProgress : MonoBehaviour {

    public void delProg() //figure out why it only works when you just started the scene for the first time.
    {
        File.Delete(Application.persistentDataPath + "/progressData.dat");
        PlayerPrefs.DeleteAll();
        Destroy(GameObject.Find("scoreKeeper"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
