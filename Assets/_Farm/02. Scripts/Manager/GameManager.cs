using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;

    public void GameOver()
    {
        Time.timeScale = 0;
        
        gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}