using System.Collections;
using UnityEngine;

public class DoorOpenClose : MonoBehaviour
{
    private Transform doorChild;
    public Vector3 closedPosition;
    public Vector3 openPosition;

    private bool isOpen = true;
    private bool isMoving = false;

    [Range(1f, 10f)]
    public float doorMoveDurationInSeconds = 1f;
    public AudioSource doorAudioSource;

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
        if (doorAudioSource == null)
        {
            Debug.LogError("AudioSource for door sound is not assigned.");
        }
    }

    public void ToggleDoor()
    {
        if (isMoving) return;

        if (isOpen)
        {
            StartCoroutine(MoveDoor(closedPosition));
            isOpen = false;
        }
        else
        {
            StartCoroutine(MoveDoor(openPosition));
            isOpen = true;
        }
    }

    private IEnumerator MoveDoor(Vector3 targetPosition)
    {
        isMoving = true;

        Vector3 startPosition = doorChild.position;
        float elapsedTime = 0;

        if (doorAudioSource != null)
        {
            doorAudioSource.Play();
        }

        while (elapsedTime < doorMoveDurationInSeconds)
        {
            doorChild.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / doorMoveDurationInSeconds);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        doorChild.position = targetPosition;

        if (doorAudioSource != null)
        {
            doorAudioSource.Stop();
        }

        isMoving = false;
    }
}
