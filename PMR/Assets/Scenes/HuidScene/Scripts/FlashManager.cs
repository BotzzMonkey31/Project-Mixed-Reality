using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem; // Voor toetsenbordinput

public class FlashManager : MonoBehaviour
{
    public GameObject[] zenuwen, aders, zweetklieren, haarvaten;

    // Booleans voor het activeren van de glow-effecten
    public bool activateZenuwen, activateAders, activateZweetklieren, activateHaarvaten;

    // Glow materiaal en instellingen
    [SerializeField] private Material glowMaterial;
    [SerializeField] private float flashDuration = 0.1f; // Duur van één glow in seconden
    [SerializeField] private int repetitions = 3; // Aantal herhalingen

    private void Update()
    {
        // Toetsenbordinput om booleans te testen
        if (Keyboard.current.aKey.wasPressedThisFrame) activateZenuwen = true;
        if (Keyboard.current.zKey.wasPressedThisFrame) activateAders = true;
        if (Keyboard.current.eKey.wasPressedThisFrame) activateZweetklieren = true;
        if (Keyboard.current.rKey.wasPressedThisFrame) activateHaarvaten = true;

        // Controleer booleans en activeer respectieve glow-effecten
        if (activateZenuwen)
        {
            StartCoroutine(ApplyGlow(zenuwen));
            activateZenuwen = false;
        }

        if (activateAders)
        {
            StartCoroutine(ApplyGlow(aders));
            activateAders = false;
        }

        if (activateZweetklieren)
        {
            StartCoroutine(ApplyGlow(zweetklieren));
            activateZweetklieren = false;
        }

        if (activateHaarvaten)
        {
            StartCoroutine(ApplyGlow(haarvaten));
            activateHaarvaten = false;
        }
    }

    private IEnumerator ApplyGlow(GameObject[] objects)
    {
        // Opslaan van originele materialen
        Material[] originalMaterials = new Material[objects.Length];
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] != null)
            {
                Renderer renderer = objects[i].GetComponent<Renderer>();
                if (renderer != null)
                {
                    originalMaterials[i] = renderer.material;
                }
            }
        }

        // Glow-effect toepassen
        for (int r = 0; r < repetitions; r++)
        {
            foreach (GameObject obj in objects)
            {
                if (obj != null)
                {
                    Renderer renderer = obj.GetComponent<Renderer>();
                    if (renderer != null && glowMaterial != null)
                    {
                        renderer.material = glowMaterial;
                    }
                }
            }
            yield return new WaitForSeconds(flashDuration);

            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i] != null)
                {
                    Renderer renderer = objects[i].GetComponent<Renderer>();
                    if (renderer != null && originalMaterials[i] != null)
                    {
                        renderer.material = originalMaterials[i];
                    }
                }
            }
            yield return new WaitForSeconds(flashDuration);
        }
    }
}
