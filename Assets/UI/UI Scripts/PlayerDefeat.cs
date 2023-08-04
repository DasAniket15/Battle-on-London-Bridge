using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDefeat : MonoBehaviour
{
    public static bool GameOver = false;

    public GameObject gameOverPanel;
    public PlayerHealth health;

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
        Time.timeScale = 1f;
        GameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Method to return to the main menu (called from the "Main Menu" button in the UI)
    public void MainMenu()
    {
        Time.timeScale = 1f;
        GameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2); // Replace "MainMenu" with the name of your main menu scene
    }

    public void QuitGame()
    {
        Debug.Log("QUITTING GAME...");
        Application.Quit();
    }
}
