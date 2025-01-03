using UnityEngine;
using UnityEngine.InputSystem; // Gebruik de nieuwe Input System package

public class LightSwitcherWithToggle : MonoBehaviour
{
    // Publieke array voor de GameObjects
    public GameObject[] gameObjects;

    // Twee materialen: normal en light
    public Material normalMaterial;
    public Material lightMaterial;

    // Tijd tussen elke stap (in seconden)
    public float switchInterval = 1f;

    // Boolean om het proces aan/uit te schakelen
    public bool isSwitching = true;
    public bool switchDirection = false;

    private int currentIndex = 0; // Houdt bij welk object aan de beurt is
    private Coroutine switchCoroutine; // Referentie naar de actieve Coroutine

    void Start()
    {
        // Start de Coroutine als switching actief is
        if (isSwitching)
        {
            switchCoroutine = StartCoroutine(SwitchMaterials());
        }
    }

    void Update()
    {
        // Controleer of de boolean is gewijzigd
        if (isSwitching && switchCoroutine == null)
        {
            // Start de loop opnieuw als deze gestopt was
            switchCoroutine = StartCoroutine(SwitchMaterials());
        }
        else if (!isSwitching && switchCoroutine != null)
        {
            // Stop de loop als switching wordt uitgeschakeld
            StopCoroutine(switchCoroutine);
            switchCoroutine = null;

            // Reset het laatste actieve object naar normaal materiaal
            ResetLastMaterial();
        }

        // Controleer de richting en wijzig de volgorde indien nodig
        if (Keyboard.current.lKey.wasPressedThisFrame) // Gebruik de nieuwe Input System syntax
        {
            ToggleDirection();
        }
    }

    private System.Collections.IEnumerator SwitchMaterials()
    {
        while (true)
        {
            // Controleer of er objecten zijn
            if (gameObjects.Length > 0)
            {
                // Reset alle objecten naar normaal materiaal om te voorkomen dat meerdere objecten licht materiaal hebben
                foreach (var obj in gameObjects)
                {
                    obj.GetComponent<Renderer>().material = normalMaterial;
                }

                // Stel het huidige object in op licht materiaal
                gameObjects[currentIndex].GetComponent<Renderer>().material = lightMaterial;

                // Ga naar het volgende object afhankelijk van de richting
                currentIndex = switchDirection ? (currentIndex - 1 + gameObjects.Length) % gameObjects.Length : (currentIndex + 1) % gameObjects.Length;
            }

            // Wacht voor de volgende stap
            yield return new WaitForSeconds(switchInterval);
        }
    }

    private void ResetLastMaterial()
    {
        if (gameObjects.Length > 0 && currentIndex >= 0)
        {
            // Zorg dat alle objecten weer normaal materiaal krijgen
            foreach (var obj in gameObjects)
            {
                obj.GetComponent<Renderer>().material = normalMaterial;
            }
        }
    }

    private void ToggleDirection()
    {
        // Wissel de waarde van switchDirection
        switchDirection = !switchDirection;

        // Keer de array om
        System.Array.Reverse(gameObjects);

        // Reset de currentIndex om in de juiste richting verder te gaan
        currentIndex = switchDirection ? gameObjects.Length - 1 : 0;
    }
}
