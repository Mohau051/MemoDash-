using UnityEngine;
using UnityEngine.UI; // Needed for Image component

public class AudioManager2 : MonoBehaviour
{
    public void StopMusic() { musicSource.Stop(); }
    public void RestartMusic() { musicSource.Stop(); musicSource.Play(); }
    public static AudioManager2 Instance;

    [Header("----Audio Source-----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("----Audio Clip----")]
    public AudioClip background;
    public AudioClip Jump;

    [Header("----UI Elements----")]
    [SerializeField] private Image soundButtonImage;
    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;

    private bool isMuted = false;

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
        // Load saved mute state
        isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;
        UpdateAudioState();

        musicSource.clip = background;
        musicSource.Play();
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        UpdateAudioState();

        // Save state
        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
    }

    private void UpdateAudioState()
    {
        musicSource.mute = isMuted;
        SFXSource.mute = isMuted;

        if (soundButtonImage != null)
        {
            soundButtonImage.sprite = isMuted ? soundOffSprite : soundOnSprite;
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (!isMuted) // Only play if not muted
        {
            SFXSource.PlayOneShot(clip);
        }
    }
    // Add these methods to your existing AudioManager2 script
    
}