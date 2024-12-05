using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerTutorial : MonoBehaviour
{
    public GameObject XROrigin;
    public GameObject HardCodedStartPosition;
    void Start()
    {
        if(XROrigin != null &&  HardCodedStartPosition != null)
        {
            XROrigin.transform.position = HardCodedStartPosition.transform.position;
            XROrigin.transform.rotation = HardCodedStartPosition.transform.rotation;
        }
    }

    void Update()
    {
        
    }
    public void VideoFinishedPlaying()
    {
        Debug.Log("video is finished");
    }
    public void QuitTutorial()
    {
        SceneManager.LoadScene("CombinedScene");
    }
}
