using UnityEngine;

public class KCelMovement : MonoBehaviour
{
    public float timeToReach = 5f; // Tijd in seconden voordat het script stopt
    private GameObject targetObject; // Het object waar het van weg moet bewegen (Elevator)
    public float movementSpeed = 1f; // Snelheid van de beweging (zowel neerwaarts als zijwaarts)
    private Vector3 startPosition;
    private float elapsedTime = 0f; // Tijd verstreken sinds start

    void Start()
    {
        // Zoek het "Elevator" object op naam
        targetObject = GameObject.Find("Elevator");

        // Controleer of het object daadwerkelijk gevonden is
        if (targetObject == null)
        {
            Debug.LogError("Elevator object niet gevonden in de scene!");
            return;
        }

        // Beginpositie is de huidige positie van het object
        startPosition = transform.position;
    }

    void Update()
    {
        // Verhoog de tijd die verstreken is
        elapsedTime += Time.deltaTime;

        // Beweeg het object
        MoveObject();

        // Stop het script en vernietig het object als de tijd is verstreken
        if (elapsedTime >= timeToReach)
        {
            Destroy(gameObject); // Vernietig het object zelf
            Destroy(this); // Verwijder dit script
        }
    }

    void MoveObject()
    {
        // Beweeg omlaag (neerwaarts)
        float downwardMovement = movementSpeed * Time.deltaTime; // Neerwaarts per frame

        // Bereken de richting weg van het targetObject (Elevator), maar negeer de Y-as
        Vector3 directionAwayFromTarget = (transform.position - targetObject.transform.position); // Van het object naar de Elevator
        directionAwayFromTarget.y = 0; // Negeer de Y-as voor zijwaartse beweging
        directionAwayFromTarget.Normalize(); // Normaliseer de vector voor consistente snelheid

        // Beweeg zijwaarts
        Vector3 sidewaysMovement = directionAwayFromTarget * movementSpeed * Time.deltaTime;

        // Bereken de nieuwe positie
        Vector3 newPosition = transform.position - Vector3.up * downwardMovement + sidewaysMovement;

        // Zet de nieuwe positie in het object
        transform.position = newPosition;

        // Draai het object in een willekeurige richting
        // We gebruiken de movementSpeed om de rotatiesnelheid te bepalen
        float rotationAmount = movementSpeed * Time.deltaTime * 140f; // 100f zorgt voor een duidelijke rotatie

        // Draai het object rond de Y-as (of een andere as indien gewenst)
        transform.Rotate(Vector3.up * rotationAmount); // Dit zorgt voor rotatie rond de Y-as
    }
}
