using UnityEngine;
using UnityEngine.InputSystem;

public class ActivateKCells : MonoBehaviour
{
    private GameObject subObject1; // Eerste subobject
    private GameObject subObject2; // Tweede subobject
    private bool finished = false; // Zorgt ervoor dat activatie slechts één keer plaatsvindt
    public bool startActivation = false;

    void Start()
    {
        // Zoek subobjecten op basis van hun namen
        subObject1 = transform.Find("KCel 1")?.gameObject;
        subObject2 = transform.Find("KCel 2")?.gameObject;

        // Zorg dat ze standaard uit staan
        if (subObject1 != null)
            subObject1.SetActive(false);
        if (subObject2 != null)
            subObject2.SetActive(false);
    }

    void Update()
    {
        if (startActivation)
        {
            ActivateSubObjects();
            finished = true;
            Destroy(this); // Verwijder het script na activatie
        }
    }

    private void ActivateSubObjects()
    {
        if (subObject1 != null)
            subObject1.SetActive(true);

        if (subObject2 != null)
            subObject2.SetActive(true);

        Debug.Log("Subobjecten geactiveerd!");
    }
}
