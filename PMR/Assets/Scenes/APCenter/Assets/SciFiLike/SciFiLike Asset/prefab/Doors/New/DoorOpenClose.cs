using UnityEngine;

public class DoorOpenClose : MonoBehaviour
{
    private Transform doorChild;
    public Vector3 closedPosition;
    public Vector3 openPosition;

    private bool isOpen = false;

    void Start()
    {
        if (transform.childCount > 0)
        {
            doorChild = transform.GetChild(0);
        }
        else
        {
            Debug.LogError("movable part of door couldn't be found");
        }
        if(doorChild != null)
        {
            openPosition = doorChild.position;
            float doorHeight = doorChild.GetComponent<Renderer>().bounds.size.y;
            closedPosition = openPosition - new Vector3(0, doorHeight, 0);
        }
    }

    public void ToggleDoor()
    {
        if(isOpen == true)
        {
            isOpen = false;
            Close();
        }
        else
        {
            isOpen = true;
            Open();
        }
    }
    private void Open()
    {
        if (doorChild != null)
        {
            doorChild.position = openPosition;
            Debug.Log("Door opened");
        }
    }
    private void Close()
    {
        if (doorChild != null)
        {
            doorChild.position = closedPosition;
            Debug.Log("Door closed");
        }
    }
}
