using UnityEngine;
using UnityEngine.Video;

public class Video : MonoBehaviour
{
    public GameObject gameManager;
    private GameManagerTutorial GameManager;
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        if (videoPlayer == null)
        {
            Debug.LogError("No VideoPlayer component found on this GameObject.");
            return;
        }
        videoPlayer.loopPointReached += OnVideoFinished;

        if (gameManager != null)
        {
            GameManager = gameManager.GetComponent<GameManagerTutorial>();
        }
        if(GameManager == null)
        {
            Debug.LogError("couldn't get gamemanager script");
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        if(GameManager != null)
        {
            GameManager.VideoFinishedPlaying();
        }        
    }

    private void OnDestroy()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoFinished;
        }
    }
}
