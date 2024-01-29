using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextLevelName = "YourNextLevelName";

    void Start()
    {
        // Subscribe to the videoPlayer's loopPointReached event
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        // Unsubscribe from the event to avoid multiple calls
        videoPlayer.loopPointReached -= OnVideoFinished;

        // Change the level after the video finishes
        ChangeLevel();
    }

    void ChangeLevel()
    {
        // Load the next level
        SceneManager.LoadScene(nextLevelName);
    }
}