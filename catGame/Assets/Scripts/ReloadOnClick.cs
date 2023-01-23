using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ReloadOnClick : MonoBehaviour
{
	public void ReloadScene ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
