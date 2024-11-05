using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class HighlightInteractable : XRBaseInteractable
{
    private Outline outline;
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
        Debug.Log("enter hover on interactable");
        if (outline != null)
        {
            outline.enabled = true;
        }
    }
    private void OnHoverExited(HoverExitEventArgs args)
    {
        Debug.Log("exited hover on interactable");
        if (outline != null)
        {
            outline.enabled = false;
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log("grabbed interactable");
    }
}
