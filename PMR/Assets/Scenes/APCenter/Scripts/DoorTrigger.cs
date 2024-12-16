using UnityEngine;
using UnityEngine.UIElements;

public class DoorTrigger : MonoBehaviour
{
    public GameObject pannel;
    private bool hitBefore = false;
    public GameObject gameManagerObject;
    private GameManager gameManager;
    void Start()
    {
        if(pannel == null)
        {
            Debug.LogError("panel to toggle in glass elevator is not assigned");
        }
        if(gameManagerObject == null)
        {
            Debug.LogError("game manager object is not assigned");
        }
        else
        {
            gameManager = gameManagerObject.GetComponent<GameManager>();
            if(gameManager == null)
            {
                Debug.LogError("game manager script is not assigned");
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(pannel != null && !hitBefore)
        {
            pannel.SetActive(true);
        }
        if(gameManager != null && !hitBefore)
        {
            gameManager.StopAudio();
        }
        hitBefore = true;
    }
}
