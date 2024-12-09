using UnityEngine;

public class RedArrowManager : MonoBehaviour
{
    public GameObject[] targetObjectsArray; // Array van GameObjects waarop we gaan zoeken naar subcomponenten met tag "RedArrow"

    public float showTime = 2f; // Tijd dat de "RedArrow" zichtbaar is voordat deze weer wordt uitgeschakeld en vernietigd
    public bool startActivation = false; // Boolean die de activatie triggert

    private bool hasStarted = false; // Boolean om ervoor te zorgen dat het proces maar één keer wordt gestart

    void Start()
    {
        // Zoek naar alle GameObjects in de array die een child hebben met de tag "RedArrow" en zet ze uit
        foreach (GameObject targetObject in targetObjectsArray)
        {
            Transform redArrow = targetObject.transform.Find("arrow");
            if (redArrow != null)
            {
                redArrow.gameObject.SetActive(false); // Zorg ervoor dat het object niet zichtbaar is aan het begin
            }
            else
            {
                Debug.LogWarning("Geen 'RedArrow' gevonden in: " + targetObject.name);
            }
        }
    }

    void Update()
    {
        // Als startActivation waar is en het proces nog niet is gestart
        if (startActivation && !hasStarted)
        {
            hasStarted = true; // Markeer dat het proces is gestart
            ActivateRedArrows(); // Activeer de RedArrow objecten direct
        }
    }

    private void ActivateRedArrows()
    {
        // Zet alle RedArrow componenten direct aan
        foreach (GameObject targetObject in targetObjectsArray)
        {
            Transform redArrow = targetObject.transform.Find("arrow");
            if (redArrow != null)
            {
                redArrow.gameObject.SetActive(true);
            }
        }

        // Wacht de opgegeven tijd (showTime) en zet daarna de objecten uit en vernietig het script
        Invoke("DeactivateAndDestroy", showTime);
    }

    private void DeactivateAndDestroy()
    {
        // Zet alle RedArrow componenten weer uit
        foreach (GameObject targetObject in targetObjectsArray)
        {
            Transform redArrow = targetObject.transform.Find("arrow");
            if (redArrow != null)
            {
                Destroy(redArrow.gameObject);
            }
        }

        // Vernietig het script nadat de objecten zijn gedeactiveerd
        Destroy(this);
    }
}
