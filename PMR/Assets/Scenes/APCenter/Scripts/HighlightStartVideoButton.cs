using UnityEngine;

public class HighlightStartVideoButton : MonoBehaviour
{
    private Outline outline;
    private float timer = 0f;
    private float toggleInterval = 1f;
    void Start()
    {
        outline = GetComponent<Outline>();
        if (outline == null)
        {
            Debug.LogError("Outline component not found on the GameObject.");
        }
        else
        {
            outline.enabled = false;
        }
    }

    void Update()
    {
        if (outline == null) return;

        timer += Time.deltaTime;
        if (timer >= toggleInterval)
        {
            outline.enabled = !outline.enabled;
            timer = 0f;
        }
    }
}
