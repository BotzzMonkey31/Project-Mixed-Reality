using UnityEngine;
using UnityEngine.InputSystem;

public class KCellenSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject object1; // Het eerste object om te spawnen
    public GameObject object2; // Het tweede object om te spawnen
    public float spawnInterval = 0.3f; // Interval in seconden tussen spawns (300 ms standaard)
    public float spawnDuration = 10f; // Totale tijd in seconden dat spawns plaatsvinden
    public float minSpawnDistance = 3f; // Minimale spawn afstand (in meters)
    public float maxSpawnDistance = 6f; // Maximale spawn afstand (in meters)

    private float elapsedTime = 0f; // Tijd die is verstreken sinds de start van de spawns
    private float nextSpawnTime = 0f; // Tijd tot de volgende spawn
    public bool startSpawning = false; // om te starten met spawnen


    void Update()
    {
        if (startSpawning) {
            // Controleer of de spawnduur is verlopen
            if (elapsedTime >= spawnDuration)
            {
                Destroy(this); // Verwijder dit script, het is niet meer nodig
                return;
            }

            elapsedTime += Time.deltaTime;

            // Controleer of het tijd is om een nieuw object te spawnen
            if (elapsedTime >= nextSpawnTime)
            {
                SpawnObject(object1);
                SpawnObject(object2);

                // Stel de tijd in voor de volgende spawn
                nextSpawnTime += spawnInterval;
            }
        }
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame) {
            startSpawning = true;
        }
    }

    private void SpawnObject(GameObject obj)
    {
        if (obj == null)
        {
            Debug.LogWarning("Geen object toegewezen om te spawnen!");
            return;
        }

        // Bereken een willekeurige hoek en afstand voor de spawnlocatie
        float randomAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad; // Willekeurige hoek in radialen
        float randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance); // Willekeurige afstand

        // Bereken de spawnpositie
        Vector3 spawnPosition = new Vector3(
            transform.position.x + Mathf.Cos(randomAngle) * randomDistance,
            transform.position.y, // Hoogte blijft hetzelfde
            transform.position.z + Mathf.Sin(randomAngle) * randomDistance
        );

        // Spawn het object
        Instantiate(obj, spawnPosition, Quaternion.identity);
    }
}
