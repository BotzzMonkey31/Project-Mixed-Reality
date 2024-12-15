using UnityEngine;
using UnityEngine.UIElements;

public class DoorTrigger : MonoBehaviour
{
    public GameObject pannel;
    private bool hitBefore = false;
    void Start()
    {
        if(pannel == null)
        {
            Debug.LogError("panel to toggle in glass elevator is not assigned");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(pannel != null && !hitBefore)
        {
            pannel.SetActive(true);
        }
        hitBefore = true;
    }
}
