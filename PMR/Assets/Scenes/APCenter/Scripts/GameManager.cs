using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] elevatorButtons;
    public GameObject door;
    public AudioSource elevatorBell;

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
        if(door == null)
        {
            Debug.LogError("can't find elevator door");
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
        elevatorBell.Play();
    }
}