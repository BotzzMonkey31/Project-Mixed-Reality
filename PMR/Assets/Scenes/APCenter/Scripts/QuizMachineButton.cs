using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class QuizMachineButton : XRBaseInteractable
{
    public Answer answer;
    private Outline outline;
    private QuizMachine quizMachine;
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
        Transform grandparent = transform.parent?.parent;
        if(grandparent != null)
        {
            quizMachine = grandparent.GetComponent<QuizMachine>();
        }
        if(quizMachine == null)
        {
            Debug.LogError("couldn't find quiz machine that quiz machine button is on");
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
        quizMachine.ButtonPushed(answer);
    }
}
