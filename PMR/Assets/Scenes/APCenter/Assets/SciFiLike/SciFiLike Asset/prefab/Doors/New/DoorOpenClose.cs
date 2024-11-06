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
            closedPosition = doorChild.position;
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
        Debug.Log("Open");
    }
    private void Close()
    {
        Debug.Log("Close");
    }
}
