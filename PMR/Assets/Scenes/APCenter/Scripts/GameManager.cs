using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] elevatorButtons;
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
    }

    void Update()
    {
        
    }
}
