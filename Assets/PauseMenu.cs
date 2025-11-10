using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    private bool isPaused = false;

    void Update()
    {
        // Toggle pause with Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; // Freeze game time
        AudioListener.pause = true; // Pause all audio
    }

    public void Home()
    {
        Time.timeScale = 1f; // Reset time scale before leaving
        AudioListener.pause = false;
        SceneManager.LoadScene("Main Menu");
    }

    public void Resume()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; // Restore normal time
        AudioListener.pause = false;
    }

    public void Restart()
    {
        Time.timeScale = 1f; // Ensure timescale resets
        AudioListener.pause = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}