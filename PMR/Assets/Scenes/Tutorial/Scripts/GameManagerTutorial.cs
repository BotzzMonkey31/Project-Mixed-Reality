using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerTutorial : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void QuitTutorial()
    {
        SceneManager.LoadScene("CombinedScene");
    }
}
