using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelOnParticleTrigger : MonoBehaviour
{
    public GameObject particleObject;     // Assign Particle GameObject with ParticleSystem + trigger collider
    public AudioClip portalSound;         // Assign your portal opening sound in Inspector

    private bool hasActivated = false;
    private bool levelLoadingEnabled = false;

    private ParticleSystem ps;
    private AudioSource audioSource;

    void Start()
    {
        if (particleObject != null)
        {
            ps = particleObject.GetComponent<ParticleSystem>();
            particleObject.SetActive(false); // Hide particle at start
        }

        // Setup an AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!hasActivated && other.CompareTag("Player"))
        {
            hasActivated = true;

            if (particleObject != null)
            {
                particleObject.SetActive(true);

                if (ps != null)
                    ps.Play();

                Debug.Log("✨ Particle activated!");

                // Play portal sound
                if (portalSound != null)
                    audioSource.PlayOneShot(portalSound);
            }

            levelLoadingEnabled = true;
        }
    }

    void Update()
    {
        if (levelLoadingEnabled && particleObject != null)
        {
            Collider[] hits = Physics.OverlapSphere(particleObject.transform.position, 0.5f);

            foreach (var hit in hits)
            {
                if (hit.CompareTag("Player"))
                {
                    Debug.Log("🚪 Player touched particle! Loading next level...");

                    int currentScene = SceneManager.GetActiveScene().buildIndex;
                    int nextScene = currentScene + 1;

                    if (nextScene < SceneManager.sceneCountInBuildSettings)
                    {
                        SceneManager.LoadScene(nextScene);
                    }
                    else
                    {
                        Debug.LogWarning("⚠️ No next scene found!");
                    }

                    levelLoadingEnabled = false;
                    break;
                }
            }
        }
    }
}
