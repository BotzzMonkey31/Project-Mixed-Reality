using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ElevatorControllerXR : MonoBehaviour
{
    public Transform[] levels;
    public float moveSpeed = 2f;
    private int currentLevel = 0;
    private bool isMoving = false;

    void Update()
    {
        if (isMoving)
        {
            Transform target = levels[currentLevel];
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target.position) < 0.01f)
            {
                isMoving = false;
            }
        }
    }


    public void MoveDown()
    {
        if (!isMoving && currentLevel < levels.Length - 1)
        {
            currentLevel++;
            isMoving = true;
        }
    }

    public void MoveUp()
    {
        if (!isMoving && currentLevel > 0)
        {
            currentLevel--;
            isMoving = true;
        }
    }
}
