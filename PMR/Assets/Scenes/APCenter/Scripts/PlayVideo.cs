using UnityEngine;
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour
{
    VideoPlayer player;
    void Start()
    {
        player = GetComponent<VideoPlayer>();
        if(player == null)
        {
            Debug.Log("no video player component found");
        }
    }
    public void StartVideo()
    {
        if(player != null && !player.isPlaying)
        {
            player.Play();
        }
    }
}
