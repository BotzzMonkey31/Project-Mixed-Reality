using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] elevatorButtons;
    public GameObject door;
    public AudioSource elevatorBell;
    public AudioSource elevatorHumm;
    public GameObject startvideoButton;
    private DoorOpenClose doorScript;
    private HighlightStartVideoButton startvideoButtonScript;

    private float delay = 3.0f;
    void Start()
    {
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
    }
}