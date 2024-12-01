using UnityEngine;

public class ActivateParticleSystem : MonoBehaviour
{
    private ParticleSystem particleSys; // Het ParticleSystem dat geactiveerd moet worden
    public bool activateParticles = false; // Boolean om deeltjes te activeren

    void Start()
    {
        // Stel het ParticleSystem in als het systeem dat op dit gameobject zit, als het nog niet is ingesteld
        if (particleSys == null)
        {
            particleSys = GetComponent<ParticleSystem>();
        }
    }

    void Update()
    {
        if (activateParticles)
        {
            // Activeer het ParticleSystem
            particleSys.Play();

            // Vernietig het script direct
            Destroy(this);
        }
    }
}
