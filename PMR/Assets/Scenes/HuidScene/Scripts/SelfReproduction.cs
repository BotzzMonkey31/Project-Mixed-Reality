using UnityEngine;
using System.Collections;

public class SelfReproduction : MonoBehaviour
{
    // Instelbare variabelen
    public float spawnDistanceY = .3f;  // Afstand onder het object op de Y-as
    public float maxDeviationX = .3f;    // Maximale afwijking op de X-as
    public float maxDeviationZ = .3f;    // Maximale afwijking op de Z-as
    public float spawnChance = 0.5f;    // Kans om zichzelf te reproduceren (0.0 - 1.0)
    public float spawnDelay = 0.7f;     // Wachttijd per poging in seconden (standaard 200 ms)
    private float activeDelay;
    public int spawnTries = 3;          // Aantal pogingen om te reproduceren
    public float spawnReduction = 0.1f; // Hoeveel de spawnkans afneemt per nieuwe instantie

    public float scaleDuration = 1f;    // Duur in seconden om van schaal 0 naar de originele schaal te gaan

    public Vector3 originalScale = new Vector3(1f, 1f, 1f); // Zet een standaard waarde voor originele schaal

    // Start wordt eenmaal aangeroepen bij het starten van het script
    void Start()
    {
        // Sla de oorspronkelijke schaal op als deze niet via de inspector is ingesteld
        if (originalScale == Vector3.zero)
        {
            originalScale = transform.localScale;  // Gebruik de huidige schaal als originele schaal
        }

        // Randomize de rotatie van het object bij het starten
        transform.rotation = Random.rotation;

        // Zet de schaal gelijk aan Vector3.zero, zodat de schaal altijd wordt aangepast in de coroutine
        transform.localScale = Vector3.zero;

        // Start de coroutine die de schaal verandert naar de originele schaal
        StartCoroutine(ScaleFromZeroToOriginal());

        // Start de coroutine die meerdere pogingen probeert om te reproduceren met wachttijden tussen de pogingen
        StartCoroutine(TryReproduceWithDelay());
    }

    // Coroutine om de schaal van het object van 0 naar de originele schaal te veranderen
    IEnumerator ScaleFromZeroToOriginal()
    {
        // Start het schalen naar de originele schaal
        float elapsedTime = 0f;
        while (elapsedTime < scaleDuration)
        {
            // Schaal het object proportioneel tussen schaal 0 en de originele schaal
            transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, elapsedTime / scaleDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Zorg ervoor dat het object de originele schaal bereikt
        transform.localScale = originalScale;
    }

    // Coroutine die meerdere pogingen maakt met een wachttijd tussen de pogingen
    IEnumerator TryReproduceWithDelay()
    {
        // Maak meerdere pogingen, afhankelijk van spawnTries
        for (int i = 0; i < spawnTries; i++)
        {
            activeDelay = Random.Range(spawnDelay - (spawnDelay / 2f), spawnDelay + (spawnDelay / 2f));
            // Wacht voor de ingestelde spawnDelay tijd
            yield return new WaitForSeconds(activeDelay);

            // Probeer de reproductie
            TryReproduce();
        }
    }

    // Methode om te proberen het object te reproduceren
    void TryReproduce()
    {
        // Controleer of het object zichzelf moet reproduceren
        if (Random.value <= spawnChance)
        {
            // Bepaal de spawnpositie
            Vector3 spawnPosition = new Vector3(
                transform.position.x + Random.Range(-maxDeviationX, maxDeviationX),  // Willekeurige afwijking in X
                transform.position.y + Random.Range(-spawnDistanceY, 0f),  // Willekeurige afwijking naar beneden in Y
                transform.position.z + Random.Range(-maxDeviationZ, maxDeviationZ)   // Willekeurige afwijking in Z
            );

            // Maak een nieuwe instantie van dit object op de berekende positie
            GameObject newObject = Instantiate(gameObject, spawnPosition, transform.rotation);

            // Geef de nieuwe instantie een lagere spawnChance via de constructor
            newObject.GetComponent<SelfReproduction>().Initialize(spawnChance - spawnReduction);

            // Start de schaalovergang voor het gekloonde object
            newObject.GetComponent<SelfReproduction>().StartCoroutine(newObject.GetComponent<SelfReproduction>().ScaleFromZeroToOriginal());
        }
    }

    // Methode om de spawnChance in te stellen bij een nieuwe instantie
    public void Initialize(float newSpawnChance)
    {
        spawnChance = Mathf.Max(0f, newSpawnChance);  // Zorg ervoor dat de spawnChance niet negatief wordt
    }
}
