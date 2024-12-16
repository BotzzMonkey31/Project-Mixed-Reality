using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ElevatorControllerXR : MonoBehaviour
{
    public Transform[] levels;
    public float moveSpeed = 2f;
    public AudioClip[] levelAudioClips;
    public AudioSource audioSource;
    public GameObject quizMachineObject;
    private QuizMachine quizMachine;
    private int currentLevel = 0;
    public int huidLevel = 0;
    private bool isMoving = false;
    private bool controlsLocked = false;
    public GameObject elevatorDoor;

    void Start()
    {
        if(quizMachineObject == null)
        {
            Debug.LogError("quiz machine object is not referenced");
        }
        else
        {
            quizMachine = quizMachineObject.GetComponent<QuizMachine>();
            if(quizMachine == null )
            {
                Debug.LogError("quiz machine script is not referenced");
            }
        }
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        if(elevatorDoor == null)
        {
            Debug.LogError("elevator door is not assigned");
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
                LockControls();
                PlayLevelAudio(); // Play audio when elevator stops at a level
                ChangeHuidLevel(); // Update level so huid stuff can be triggered
            }
        }
    }

    public void MoveDown()
    {
        if (!isMoving && currentLevel < levels.Length - 1 && !controlsLocked)
        {
            currentLevel++;
            isMoving = true;
        }
    }

    public void MoveUp()
    {
        if (!isMoving && currentLevel > 0 && !controlsLocked)
        {
            currentLevel--;
            isMoving = true;
        }
    }

    private void PlayLevelAudio()
    {
        // reopen elevator
        if(currentLevel == levels.Length - 1 && elevatorDoor != null)
        {
            elevatorDoor.SetActive(false);
        }
        if (levelAudioClips.Length > currentLevel && levelAudioClips[currentLevel] != null)
        {
            audioSource.clip = levelAudioClips[currentLevel];
            audioSource.Play();

            // Trigger quiz logic after level audio finishes
            Invoke(nameof(StartQuiz), audioSource.clip.length + 0.5f);
        }
    }

    private void ChangeHuidLevel() {
        huidLevel = currentLevel;
    }
    private void StartQuiz()
    {
        if (quizMachine != null)
        {
            quizMachine.LoadNextQuestion();
        }
    }
    public void UnlockControls()
    {
        controlsLocked = false;
    }
    private void LockControls()
    {
        controlsLocked = true;
    }
}
