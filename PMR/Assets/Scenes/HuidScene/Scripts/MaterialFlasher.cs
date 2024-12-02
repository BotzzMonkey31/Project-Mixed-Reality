using System.Collections;
using UnityEngine;

public class MaterialFlasher : MonoBehaviour
{
    // Het originele materiaal wordt automatisch gezocht
    private Material originalMaterial;

    // Het glow materiaal is publiek instelbaar
    [SerializeField] private Material glowMaterial;

    // Instelbare parameters
    [SerializeField] private int repetitions = 3; // Aantal herhalingen
    [SerializeField] private float flashDuration = 0.1f; // Snelheid in seconden (100 ms)

    // Boolean om de trigger te activeren
    public bool trigger = false;

    private Renderer objectRenderer;

    private void Start()
    {
        // Haal de Renderer op en sla het originele materiaal op
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalMaterial = objectRenderer.material;
        }
    }

    private void Update()
    {
        // Check of de trigger aan staat
        if (trigger)
        {
            Debug.Log("trigger of flash");
            trigger = false; // Zet trigger uit om herhaling te voorkomen
            StartCoroutine(FlashMaterial());
        }
    }

    private IEnumerator FlashMaterial()
    {
        for (int i = 0; i < repetitions; i++)
        {
            // Wissel naar glow materiaal
            if (glowMaterial != null)
            {
                objectRenderer.material = glowMaterial;
            }
            yield return new WaitForSeconds(flashDuration);

            // Wissel terug naar origineel materiaal
            if (originalMaterial != null)
            {
                objectRenderer.material = originalMaterial;
            }
            yield return new WaitForSeconds(flashDuration);
        }
    }
}
