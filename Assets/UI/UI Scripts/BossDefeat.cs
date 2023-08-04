using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDefeat : MonoBehaviour
{
    public static bool GameOver = false;

    public GameObject gameOverPanel;
    public BossHealth health;

    private void Update()
    {
        if (health.GetCurrentHealth() <= 0)
        {
            Time.timeScale = 0f;
            gameOverPanel.SetActive(true);
            GameOver = true;
        }
    }

    // Method to retry the game (called from the "Retry" button in the UI)
    public void Retry()
    {
        GameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Method to return to the main menu (called from the "Main Menu" button in the UI)
    public void MainMenu()
    {
        GameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); // Replace "MainMenu" with the name of your main menu scene
    }

    public void QuitGame()
    {
        Debug.Log("QUITTING GAME...");
        Application.Quit();
    }
}
