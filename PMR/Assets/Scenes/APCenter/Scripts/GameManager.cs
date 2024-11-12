using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] elevatorButtons;
    public AudioSource elevatorBell;
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
    }

    void Update()
    {

    }
}
