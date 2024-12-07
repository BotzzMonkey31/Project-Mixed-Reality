using Unity.VisualScripting;
using UnityEngine;

public class DelayedActivation : MonoBehaviour
{
    public float delayTime = 5f;
    public float showTime = 2f;
    public string targetTag = "RedArrow";
    private GameObject targetObject;
    private float timer;
    public bool startActivation = false;
    private bool visible = false;

    void Start()
    {
        // Zoek naar het subcomponent met de opgegeven tag
        foreach (Transform child in transform)
        {
            if (child.CompareTag(targetTag))
            {
                targetObject = child.gameObject;
                break;
            }
        }

        // Zorg ervoor dat het object niet zichtbaar is aan het begin
        if (targetObject != null)
        {
            targetObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Geen object gevonden met tag: " + targetTag);
        }

    }

    void Update()
    {
        // Tel de tijd op
        timer += Time.deltaTime;

        // Controleer of de timer de vertragingstijd heeft bereikt
        if (startActivation && !visible)
        {
            Debug.Log("pijltje geactiveerd");
            targetObject.SetActive(true);
            visible = true;
            timer = 0f;
        }

        if (timer >= showTime && visible) {
            Debug.Log("nu destroyen van pijltje");
            Destroy(targetObject);
            Destroy(this);
        }
    }
}
