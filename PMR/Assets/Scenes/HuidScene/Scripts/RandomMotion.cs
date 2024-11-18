using UnityEngine;
using System.Collections.Generic;

public class RandomScaleObject : MonoBehaviour
{
    public GameObject[] objects; // Array van GameObjects
    public float scaleMultiplier = 1.2f; // De factor waarmee de schaal moet worden vergroot
    public float duration = 1.0f; // De tijd die het kost om te schalen
    public float delayBeforeReset = 2.0f; // Tijd om te wachten voordat het terugschalen begint
    public AnimationCurve scaleCurve; // De curve voor een vloeiende overgang
    public float intervalMin = 0.5f; // Minimum tijd tussen het selecteren van nieuwe objecten
    public float intervalMax = 5.0f; // Maximum tijd tussen het selecteren van nieuwe objecten

    // Variabelen voor de rotatie
    public float rotationAngleMin = 15f; // Minimum draaihoek in graden
    public float rotationAngleMax = 45f; // Maximum draaihoek in graden
    public float rotationDuration = 1.0f; // Duur van de draai-animatie

    private List<int> busyIndex = new List<int>(); // Lijst van actieve objectindexen
    private bool isScaling = true;

    private void Start()
    {
        // Start de coroutine om willekeurige objecten te schalen en draaien
        StartCoroutine(RandomScaleRoutine());
    }

    private System.Collections.IEnumerator RandomScaleRoutine()
    {
        while (true)
        {
            int randomIndex = Random.Range(0, objects.Length);
            
            if (busyIndex.Contains(randomIndex))
            {
                yield return null;
                continue;
            }

            busyIndex.Add(randomIndex);

            GameObject selectedObject = objects[randomIndex];

            // Afhankelijk van isScaling gaan we het huidige object schalen of roteren
            // door juiste bijbehorende coroutine te starten
            if (isScaling) StartCoroutine(ScaleUpAndDown(selectedObject, randomIndex));
            else StartCoroutine(TurnRandomly(selectedObject, randomIndex));

            isScaling = !isScaling; // waarde van isScaling omdraaien

            float cycleInterval = Random.Range(intervalMin, intervalMax);
            yield return new WaitForSeconds(cycleInterval);
        }
    }

    private System.Collections.IEnumerator ScaleUpAndDown(GameObject targetObject, int index)
    {
        Vector3 originalScale = targetObject.transform.localScale;
        Vector3 targetScale = originalScale * scaleMultiplier;

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float smoothStep = scaleCurve.Evaluate(t);
            targetObject.transform.localScale = Vector3.Lerp(originalScale, targetScale, smoothStep);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        targetObject.transform.localScale = targetScale;

        yield return new WaitForSeconds(delayBeforeReset);

        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float smoothStep = scaleCurve.Evaluate(t);
            targetObject.transform.localScale = Vector3.Lerp(targetScale, originalScale, smoothStep);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        targetObject.transform.localScale = originalScale;

        busyIndex.Remove(index);
    }

    private System.Collections.IEnumerator TurnRandomly(GameObject targetObject, int index)
    {
        // Bepaal een willekeurige draaihoek
        float randomAngle = Random.Range(rotationAngleMin, rotationAngleMax);
        Vector3 randomRotationAxis = Random.insideUnitSphere.normalized; // Willekeurige draai-as

        Quaternion originalRotation = targetObject.transform.rotation;
        Quaternion targetRotation = originalRotation * Quaternion.AngleAxis(randomAngle, randomRotationAxis);

        // Draai naar de nieuwe positie met een curve
        float elapsedTime = 0f;
        while (elapsedTime < rotationDuration)
        {
            float t = elapsedTime / rotationDuration;
            float smoothStep = scaleCurve.Evaluate(t);
            targetObject.transform.rotation = Quaternion.Slerp(originalRotation, targetRotation, smoothStep);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Zorg ervoor dat de rotatie precies op de targetRotation eindigt
        targetObject.transform.rotation = targetRotation;
    }
}
