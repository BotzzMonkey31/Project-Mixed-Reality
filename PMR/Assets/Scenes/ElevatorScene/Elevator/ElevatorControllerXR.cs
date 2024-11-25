using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ElevatorControllerXR : MonoBehaviour
{
    public Transform[] levels;
    public float moveSpeed = 2f;
    public AudioClip[] levelAudioClips;
    public AudioSource audioSource;
    private int currentLevel = 0;
    private bool isMoving = false;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (isMoving)
        {
            Transform target = levels[currentLevel];
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target.position) < 0.01f)
            {
                isMoving = false;
                PlayLevelAudio(); // Play audio when elevator stops at a level
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

    private void PlayLevelAudio()
    {
        if (levelAudioClips.Length > currentLevel && levelAudioClips[currentLevel] != null)
        {
            audioSource.clip = levelAudioClips[currentLevel];
            audioSource.Play();
        }
    }
}
