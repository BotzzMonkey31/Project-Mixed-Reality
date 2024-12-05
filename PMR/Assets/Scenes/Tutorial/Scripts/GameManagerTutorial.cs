using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerTutorial : MonoBehaviour
{
    public GameObject XROrigin;
    public GameObject HardCodedStartPosition;
    public GameObject startvideoButton;
    private HighlightStartVideoButton startvideoButtonScript;
    void Start()
    {
        if(XROrigin != null &&  HardCodedStartPosition != null)
        {
            XROrigin.transform.position = HardCodedStartPosition.transform.position;
            XROrigin.transform.rotation = HardCodedStartPosition.transform.rotation;
        }
        if (startvideoButton != null)
        {
            startvideoButtonScript = startvideoButton.GetComponent<HighlightStartVideoButton>();
            startvideoButtonScript.DisableBlinking();
        }
        else
        {
            Debug.LogError("couldn't find start video buttton in game manager");
        }
    }

    void Update()
    {
        
    }
    public void VideoFinishedPlaying()
    {
        Debug.Log("video is finished");
        if(startvideoButtonScript != null )
        {
            startvideoButtonScript.EnableBlinking();
        }
    }
    public void QuitTutorial()
    {
        SceneManager.LoadScene("CombinedScene");
    }
}
