using UnityEngine;
using UnityEngine.InputSystem; // Zorg ervoor dat je het nieuwe Input System gebruikt.

public class MoveObjectWithArrowKeys : MonoBehaviour
{
    public float moveSpeed = 1.0f;

    void Update()
    {
        // Gebruik de nieuwe InputSystem-methoden voor het lezen van toetsenbord input
        if (Keyboard.current.upArrowKey.isPressed)
        {
            // Beweeg het object omhoog
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }

        if (Keyboard.current.downArrowKey.isPressed)
        {
            // Beweeg het object omlaag
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }
    }
}