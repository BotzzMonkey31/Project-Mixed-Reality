using UnityEngine;

public class HighlightStartVideoButton : MonoBehaviour
{
    private Outline outline;
    private float timer = 0f;
    private float toggleInterval = 1f;
    private bool isBlinking = false;
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
        if (outline == null || !isBlinking) return;

        timer += Time.deltaTime;
        if (timer >= toggleInterval)
        {
            outline.enabled = !outline.enabled;
            timer = 0f;
        }
    }

    public void EnableBlinking()
    {
        isBlinking = true;
        timer = 0f;
    }

    public void DisableBlinking()
    {
        isBlinking = false;
        outline.enabled = false;
    }
}
