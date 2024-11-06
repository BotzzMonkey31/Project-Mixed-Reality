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

    void Update()
    {
        // testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isOpen)
                Close();
            else
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
