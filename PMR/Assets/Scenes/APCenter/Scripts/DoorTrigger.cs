using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger hit by: {other.gameObject.name}");
    }
}
