using System.Collections;
using UnityEngine;

public class MoveRoutine : MonoBehaviour
{
    public float moveDistance = 5f;  // De afstand die het object beweegt
    public float duration = 2f;      // De duur van de beweging
    public AnimationCurve movementCurve; // De curve voor de beweging

    public float rotationAngle = 90f;  // De hoek waar naartoe geroteerd wordt (bijv. 90 graden)
    public Vector3 rotationAxis = Vector3.up; // De as waarlangs je object moet roteren (bijvoorbeeld Vector3.up voor Y-as)

    private Vector3 startPos;  // Startpositie van het object
    private Vector3 endPos;    // Eindpositie van het object
    private Quaternion startRotation;  // Beginrotatie
    private Quaternion endRotation;    // Eindrotatie

    void Start()
    {
        // Sla de startpositie en rotatie op en bereken de eindpositie
        startPos = transform.position;
        endPos = startPos + new Vector3(moveDistance, 0f, 0f);  // Verplaatsing over de X-as

        startRotation = transform.rotation;  // Startrotatie (het huidige rotatie van het object)
        endRotation = Quaternion.Euler(startRotation.eulerAngles + rotationAxis * rotationAngle); // Eindrotatie, gebaseerd op de huidige rotatie

        StartCoroutine(MoveObject());
    }

    private IEnumerator MoveObject()
    {
        float timeElapsed = 0f;
        Vector3 startPoint = startPos;
        Vector3 endPoint = endPos;

        // Beweging naar rechts met rotatie naar eindhoek
        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / duration; // Bereken de voortgang van de beweging
            float smoothT = movementCurve.Evaluate(t); // Pas de curve toe voor een vloeiendere beweging

            transform.position = Vector3.Lerp(startPoint, endPoint, smoothT);

            // Roteren naar de eindhoek met dezelfde curve
            Quaternion currentRotation = Quaternion.Lerp(startRotation, endRotation, smoothT);
            transform.rotation = currentRotation;

            yield return null;
        }

        // Beweging naar links met rotatie terug naar de beginhoek
        timeElapsed = 0f;
        startPoint = endPos;
        endPoint = startPos;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / duration; // Bereken de voortgang van de beweging
            float smoothT = movementCurve.Evaluate(t); // Pas de curve toe voor een vloeiendere beweging

            transform.position = Vector3.Lerp(startPoint, endPoint, smoothT);

            // Roteren terug naar de beginhoek met dezelfde curve
            Quaternion currentRotation = Quaternion.Lerp(endRotation, startRotation, smoothT);
            transform.rotation = currentRotation;

            yield return null;
        }

        // Herstart de beweging na een volledige cyclus
        StartCoroutine(MoveObject());
    }
}
