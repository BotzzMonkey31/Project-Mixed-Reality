using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject XROrigin;
    public GameObject HardCodedStartPosition;

    public GameObject[] elevatorButtons;
    public GameObject door;
    public AudioSource elevatorBell;
    public AudioSource elevatorHumm;
    public GameObject startvideoButton;
    public AudioClip globalAudioClip;
    private AudioSource globalAudioSource;
    private DoorOpenClose doorScript;
    private HighlightStartVideoButton startvideoButtonScript;

    private float delay = 3.0f;
    void Start()
    {
        if(XROrigin != null && HardCodedStartPosition != null)
        {
            XROrigin.transform.position = HardCodedStartPosition.transform.position;
            XROrigin.transform.rotation = HardCodedStartPosition.transform.rotation;
        }
        foreach (GameObject button in elevatorButtons)
        {
            var outlineScript = button.GetComponent<Outline>();
            if(outlineScript != null)
            {
                outlineScript.enabled = false;
            }
        }
        if(elevatorBell == null)
        {
            Debug.LogError("can't find elevator bell");
        }
        if(elevatorHumm == null)
        {
            Debug.LogError("can't find elevator humm");
        }
        if(door == null)
        {
            Debug.LogError("can't find elevator door");
        }
        else
        {
            doorScript = door.GetComponent<DoorOpenClose>();
            doorScript.setDoorInactive();
            
        }
        if(elevatorHumm != null)
        {
            elevatorHumm.Play();
        }
        if(startvideoButton != null)
        {
            startvideoButtonScript = startvideoButton.GetComponent<HighlightStartVideoButton>();
            startvideoButtonScript.DisableBlinking();
        }
        else
        {
            Debug.LogError("couldn't find start video buttton in game manager");
        }
        if (globalAudioClip != null)
        {
            globalAudioSource = gameObject.AddComponent<AudioSource>();
            globalAudioSource.clip = globalAudioClip;
            globalAudioSource.loop = true;
        }
        else
        {
            Debug.LogError("Global audio clip is not set.");
        }
        StartCoroutine(ChangeButtonStatus());
    }

    void Update()
    {

    }
    IEnumerator ChangeButtonStatus()
    {
        int currentButtonIndex = 0;
        while (currentButtonIndex < elevatorButtons.Length)
        {
            yield return new WaitForSeconds(delay);

            if (currentButtonIndex > 0)
            {
                var previousOutlineScript = elevatorButtons[currentButtonIndex - 1].GetComponent<Outline>();
                if (previousOutlineScript != null)
                {
                    previousOutlineScript.enabled = false;
                }
            }

            var currentOutlineScript = elevatorButtons[currentButtonIndex].GetComponent<Outline>();
            if (currentOutlineScript != null)
            {
                currentOutlineScript.enabled = true;
            }

            currentButtonIndex++;
        }
        if (elevatorHumm != null)
        {
            elevatorHumm.Stop();
        }
        elevatorBell.Play();
        if(doorScript != null)
        {
            doorScript.setDoorActive();
        }
        if(startvideoButtonScript != null)
        {
            startvideoButtonScript.EnableBlinking();
        }
        StartAudio();
    }
    public void StartAudio()
    {
        if (globalAudioSource != null)
        {
            globalAudioSource.Play();
            Debug.Log("Global audio started.");
        }
        else
        {
            Debug.LogError("Global audio source is not initialized.");
        }
    }
    public void StopAudio()
    {
        if (globalAudioSource != null)
        {
            globalAudioSource.Stop();
            Debug.Log("Global audio stopped.");
        }
        else
        {
            Debug.LogError("Global audio source is not initialized.");
        }
    }
}