using Unity.VRTemplate;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Doorhandle : XRBaseInteractable
{
    private Outline outline;
    public GameObject parent;
    private DoorOpenClose parentScript;
    public bool enabled = true;
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
        if(parent == null)
        {
            Debug.LogError("door handle script couldn't find open close script on parent");
        }
        else
        {
            parentScript = parent.GetComponent<DoorOpenClose>();
        }
    }

    protected override void OnDisable()
    {
        hoverEntered.RemoveListener(OnHover);
        selectEntered.RemoveListener(OnGrab);
        base.OnDisable();
    }

    public void Enable()
    {
        enabled = true;
    }

    public void Disable()
    {
        enabled = false;
    }

    private void OnHover(HoverEnterEventArgs args)
    {
        if (outline != null && enabled)
        {
            outline.enabled = true;
        }
    }
    private void OnHoverExited(HoverExitEventArgs args)
    {
        if (outline != null && enabled)
        {
            outline.enabled = false;
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if(enabled)
        {
            parentScript.ToggleDoor();
        }
    }
}
