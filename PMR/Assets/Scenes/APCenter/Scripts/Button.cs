using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Button : XRBaseInteractable
{
    public GameObject gameManager;
    private GameManager gameManagerScript;
    private Outline outline;
    public GameObject ScreenPlane;
    private HighlightStartVideoButton highlightParentScript;
    public void Start()
    {
        if(ScreenPlane == null)
        {
            Debug.LogError("no screen plane defined for button");
        }
        highlightParentScript = GetComponentInParent<HighlightStartVideoButton>();
        if(highlightParentScript == null )
        {
            Debug.LogError("can't find highlight script on parent of button");
        }
        if(gameManager != null)
        {
            gameManagerScript = gameManager.GetComponent<GameManager>();
        }
        else
        {
            Debug.LogError("can't find gamemanager in button");
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
        if(ScreenPlane != null)
        {
            try
            {
                ScreenPlane.GetComponent<PlayVideo>().StartVideo();
            }
            catch
            {
                Debug.LogError("couldn't start playing video on specified gamobject for screen plane");
            }
        }
        if(highlightParentScript != null)
        {
            highlightParentScript.DisableBlinking();
        }
        if(gameManagerScript != null)
        {
            gameManagerScript.StopAudio();
        }
    }
}
