using UnityEngine;
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour
{
    VideoPlayer player;
    public GameObject gameManager;
    private GameManager gameManagerScript;
    public GameObject elevatorDoor;
    void Start()
    {
        player = GetComponent<VideoPlayer>();
        if(player == null)
        {
            Debug.Log("no video player component found");
        }
        else
        {
            player.loopPointReached += OnVideoEnd;
        }
        if (gameManager != null)
        {
            gameManagerScript = gameManager.GetComponent<GameManager>();
        }
        else
        {
            Debug.LogError("can't find gamemanager in video plane");
        }
        if(elevatorDoor == null)
        {
            Debug.LogError("elevator door is not assigned");
        }
    }
    public void StartVideo()
    {
        if(player != null && !player.isPlaying)
        {
            player.Play();
        }
    }
    private void OnVideoEnd(VideoPlayer vp)
    {
        if(gameManagerScript != null)
        {
            gameManagerScript.StartAudio();
        }
        if(elevatorDoor != null)
        {
            elevatorDoor.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        if (player != null)
        {
            player.loopPointReached -= OnVideoEnd;
        }
    }
}
