using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Memo : MonoBehaviour
{
    

    [Header("Movement Settings")]
    public float gravity = -9.8f;
    public float jumpStrength = 5f;

    [Header("Game Over Settings")]
    public GameObject gameOverPanel; // Assign in Inspector
    public Button restartButton;     // Assign in Inspector
    public Button menuButton;        // Assign in Inspector

    [Header("Audio")]
    public AudioClip jumpSound; // Assign in Inspector

    private Vector3 direction;
    private Rigidbody2D rb;
    private bool isGameOver = false;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        direction = Vector3.zero;

        // Initialize game over panel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
            restartButton.onClick.AddListener(RestartGame);
            menuButton.onClick.AddListener(ReturnToMenu);
        }
    }

    private void Update()
    {
        if (isGameOver) return;

        // Jump input
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) ||
            Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = Vector3.up * jumpStrength;
            // Play jump sound if audio manager exists
            if (audioManager != null && jumpSound != null)
            {
                audioManager.PlaySFX(jumpSound);
            }
        }

        // Apply custom gravity
        direction.y += gravity * Time.deltaTime;
        rb.linearVelocity = new Vector2(0, direction.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isGameOver)
        {
            ShowGameOver();
        }
    }

    private void ShowGameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f; // Freeze game

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        else
        {
            // Fallback if UI not set up
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ReturnToMenu()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene("Main Menu"); // Change to your menu scene name
    }
}