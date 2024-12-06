using UnityEngine;

public class RotatingArrow : MonoBehaviour
{

    public float rotationSpeed = 30f;
    public float amplitude = 0.3f;
    public float frequency = 5f;

    private Vector3 initialPosition;
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
                // Rotatie rond de y-as
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

        // Sinusbeweging op de y-as
        float sineOffset = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(initialPosition.x, initialPosition.y+sineOffset, initialPosition.z);
    }
}
