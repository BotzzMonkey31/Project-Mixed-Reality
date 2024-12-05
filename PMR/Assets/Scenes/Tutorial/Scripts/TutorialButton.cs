using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TutorialButton : UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable
{
    private Outline outline;
    private HighlightStartVideoButton highlightParentScript;
    public void Start()
    {
        highlightParentScript = GetComponentInParent<HighlightStartVideoButton>();
        if (highlightParentScript == null)
        {
            Debug.LogError("can't find highlight script on parent of button");
        }
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        hoverEntered.AddListener(OnHover);
        hoverExited.AddListener(OnHoverExited);
        selectEntered.AddListener(OnGrab);

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

    protected override void OnDisable()
    {
        hoverEntered.RemoveListener(OnHover);
        selectEntered.RemoveListener(OnGrab);
        base.OnDisable();
    }

    private void OnHover(HoverEnterEventArgs args)
    {
        if (outline != null)
        {
            outline.enabled = true;
        }
    }
    private void OnHoverExited(HoverExitEventArgs args)
    {
        if (outline != null)
        {
            outline.enabled = false;
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log("push button");
        if (highlightParentScript != null)
        {
            highlightParentScript.DisableBlinking();
        }
    }
}
