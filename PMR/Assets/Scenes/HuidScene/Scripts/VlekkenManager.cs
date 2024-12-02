using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem; // Zorg ervoor dat je het nieuwe Input System gebruikt.

public class VlekkenManager : MonoBehaviour
{
    public GameObject[] vlekkenArray; // Array van gameobjects met de beschreven structuur
    public float delayBetweenActivations = 1.0f; // Instelbare vertraging in seconden tussen activaties
    public bool activate = false; // Boolean om te controleren of het proces moet starten

    private bool hasStarted = false; // Boolean om ervoor te zorgen dat de Start-logica maar één keer wordt uitgevoerd

    private void Update()
    {
        // Controleer of 'activate' true is en de logica nog niet is gestart
        if (activate && !hasStarted)
        {
            hasStarted = true; // Markeer dat het proces is gestart
            StartCoroutine(ActivateVlekken()); // Start de coroutine éénmalig
        }

        // Controleer of de spacebar wordt ingedrukt
        if (Keyboard.current.spaceKey.isPressed)
        {
            activate = true;  // Zet de 'activate' op true wanneer de spatiebalk wordt ingedrukt
        }


    }

    private IEnumerator ActivateVlekken()
    {
        // Ga elk gameobject in de array af
        foreach (GameObject vlek in vlekkenArray)
        {
            // Zoek de subcomponenten
            ActivateParticleSystem activateParticleSystem = vlek.GetComponentInChildren<ActivateParticleSystem>();
            ScaleOverTime scaleOverTime = vlek.GetComponentInChildren<ScaleOverTime>();

            if (activateParticleSystem != null && scaleOverTime != null)
            {
                // Zet beide bools op true
                activateParticleSystem.activateParticles = true;
                scaleOverTime.startScaling = true;
            }
            else
            {
                Debug.LogWarning($"Vlek '{vlek.name}' mist een component.");
            }

            // Wacht de opgegeven vertraging voor de volgende activatie
            yield return new WaitForSeconds(delayBetweenActivations);
        }

        // Vernietig dit script na voltooiing
        Destroy(this);
    }
}
