using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject gameOverCanvas;  

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);  
        }
    }

    public void GameOver()
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }
        Time.timeScale = 0f;
    }


    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}