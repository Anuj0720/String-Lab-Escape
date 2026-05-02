using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float totalTime = 60f;
    private float currentTime;
    private bool timerRunning = false;

    [Header("UI")]
    public TextMeshProUGUI timerText;

    [Header("Sound")]
    public AudioSource tickAudioSource;
    public float tickInterval = 1f;
    private float nextTickTime;

    private bool warningColorApplied = false;

    void Start()
    {
        currentTime = totalTime;
        UpdateTimerUI();
    }

    void Update()
    {
        if (timerRunning)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerUI();

            // Tick every 1 second
            if (Time.time >= nextTickTime)
            {
                PlayTickSound();
                nextTickTime = Time.time + tickInterval;
            }

            // Color change for last 10 seconds
            if (currentTime <= 10f && !warningColorApplied)
            {
                ApplyWarningColor();
            }

            if (currentTime <= 0f)
            {
                TimerFailed();
            }
        }
    }

    public void StartTimer()
    {
        if (timerRunning) return;

        Debug.Log("⏱️ Timer Started!");
        currentTime = totalTime;
        timerRunning = true;
        nextTickTime = Time.time + tickInterval;

        if (timerText != null)
            timerText.color = Color.white; // Reset color at start

        warningColorApplied = false;
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    void PlayTickSound()
    {
        if (tickAudioSource != null && tickAudioSource.clip != null)
        {
            tickAudioSource.PlayOneShot(tickAudioSource.clip);
        }
    }

    void ApplyWarningColor()
    {
        if (timerText != null)
        {
            timerText.color = Color.red;
            warningColorApplied = true;
        }
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60f);
            int seconds = Mathf.FloorToInt(currentTime % 60f);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }

    void TimerFailed()
    {
        timerRunning = false;
        Debug.Log("🛑 Time's up! Reloading level...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
