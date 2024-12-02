using UnityEngine;

[ExecuteAlways] // Zorgt dat het script werkt in zowel de Editor als Play Mode
public class ShaderDistanceUpdater : MonoBehaviour
{
    [Tooltip("Materialen die de shader gebruiken en bijgewerkt moeten worden.")]
    public Material[] materials; // Array van materialen die de shader gebruiken

    void Update()
    {
        // De wereldpositie van dit object ophalen
        Vector3 position = transform.position;

        // De positie doorgeven aan alle materialen in de array
        foreach (Material material in materials)
        {
            if (material.HasProperty("_TargetPosition"))
            {
                // Stuur de positie als een vector naar de shader
                material.SetVector("_TargetPosition", new Vector4(position.x, position.y, position.z, 1.0f));
            }
        }
    }
}
