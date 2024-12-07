using UnityEngine;
using System.Collections;

public class TriggerHuidStuff : MonoBehaviour
{
    private ElevatorControllerXR elevatorController;
    private bool isRunning = false; // Om te voorkomen dat coroutines meerdere keren starten
    private bool finished1 = false;
    private bool finished2 = false;
    private bool finished3 = false;
    private VlekkenManager vlekkenManager;
    private RedArrowManager redArrowManager;
    private FlashManager flashManager;
    private ActivateKCells[] activateKCellsArray; // Array om alle ActivateKCells te bevatten
    private KCellenSpawner kCellenSpawner;

    void Start()
    {
        // Zoek het object met het ElevatorControllerXR-script
        elevatorController = FindFirstObjectByType<ElevatorControllerXR>();
        vlekkenManager = FindFirstObjectByType<VlekkenManager>();
        redArrowManager = FindFirstObjectByType<RedArrowManager>();
        flashManager = FindFirstObjectByType<FlashManager>();
        activateKCellsArray = FindObjectsOfType<ActivateKCells>(); // Vind alle ActivateKCells objecten
        kCellenSpawner = FindFirstObjectByType<KCellenSpawner>();
    }

    void Update()
    {
        if (elevatorController != null && !isRunning)
        {
            switch (elevatorController.huidLevel)
            {
                case 1:
                    if (!finished1)
                    {
                        isRunning = true;
                        StartCoroutine(HandleCase1());
                    }
                    break;
                case 2:
                    if (!finished2)
                    {
                        isRunning = true;
                        StartCoroutine(HandleCase2());
                    }
                    break;
                case 3:
                    if (!finished3)
                    {
                        isRunning = true;
                        StartCoroutine(HandleCase3());
                    }
                    break;
            }
        }
    }

    private IEnumerator HandleCase1()
    {
        Debug.Log("Case 1 triggered");
        yield return new WaitForSeconds(12);
        vlekkenManager.startActivation = true;
        yield return new WaitForSeconds(13);
        redArrowManager.startActivation = true;
        finished1 = true; // Pas hier op true zetten
        isRunning = false;
    }

    private IEnumerator HandleCase2()
    {
        Debug.Log("Case 2 triggered");
        yield return new WaitForSeconds(4.5f);
        flashManager.activateAders = true;
        yield return new WaitForSeconds(1.5f);
        flashManager.activateZenuwen = true;
        yield return new WaitForSeconds(1.5f);
        flashManager.activateZweetklieren = true;
        yield return new WaitForSeconds(1.5f);
        flashManager.activateHaarvaten = true;
        yield return new WaitForSeconds(5);
        
        // Itereren over de array en startActivation op true zetten voor elk object
        foreach (var activateKCell in activateKCellsArray)
        {
            activateKCell.startActivation = true;
        }

        finished2 = true; // Pas hier op true zetten
        isRunning = false;
    }

    private IEnumerator HandleCase3()
    {
        Debug.Log("Case 3 triggered");
        yield return new WaitForSeconds(8);
        kCellenSpawner.startSpawning = true;
        finished3 = true; // Pas hier op true zetten
        isRunning = false;
        Destroy(gameObject); // TriggerHuidStuff is niet meer nodig en wordt vernietigd
    }
}
