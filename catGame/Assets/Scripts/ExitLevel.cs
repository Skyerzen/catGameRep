using UnityEngine;
//using System.Collections;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    public bool goalReached = false;

    void OnTriggerEnter(Collider Goal)
    {
        goalReached = true;
    }
}
