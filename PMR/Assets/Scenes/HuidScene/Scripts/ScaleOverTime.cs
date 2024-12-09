using UnityEngine;
using System.Collections; // Voor het gebruik van coroutines

public class ScaleOverTime : MonoBehaviour
{
    public float duration = 2.0f; // Tijd in seconden om te schalen
    public float delayBeforeScaling = 2.0f; // Vertraging in seconden voordat het schalen begint

    private Vector3 targetScale; // Doelschaal (originele schaal)
    private Vector3 initialScale = Vector3.zero; // Beginschaal (altijd 0, 0, 0)
    private float elapsedTime = 0.0f; // Tijd die verstreken is
    public bool startScaling = false; // Om de schaling te starten na de vertraging

    private void Start()
    {
        // Sla de oorspronkelijke schaal op
        targetScale = transform.localScale;

        // Zet het object naar een schaal van 0
        transform.localScale = initialScale;
    }

    private void Update()
    {
        if (startScaling)
        {
            // Start de coroutine om de vertraging te verwerken voordat het schalen begint
            StartCoroutine(WaitAndScale()); // Hier komt de eerste coroutine die wacht
            startScaling = false; // Zorg ervoor dat deze alleen één keer wordt gestart
        }
    }

    public void ActivateScaling()
    {
        startScaling = true; // Zet startScaling op true om het proces te beginnen
    }

    private IEnumerator WaitAndScale()
    {
        // Wacht de opgegeven vertragingstijd
        yield return new WaitForSeconds(delayBeforeScaling);

        // Start het schalen na de vertraging
        elapsedTime = 0.0f; // Reset de verstreken tijd voor het schalen

        // Schaal tussen de beginschaal en de doelschaal
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / duration);
            transform.localScale = Vector3.Lerp(initialScale, targetScale, progress);

            yield return null; // Wacht tot de volgende frame
        }

        // Zorg ervoor dat het object de doelschaal bereikt
        transform.localScale = targetScale;
    }
}
