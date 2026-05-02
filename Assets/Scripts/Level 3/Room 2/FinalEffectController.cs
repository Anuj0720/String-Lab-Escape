using UnityEngine;

public class FinalEffectController : MonoBehaviour
{
    public AudioClip finalAudio;               // 🔊 Assign your audio clip
    public GameObject particleEffectObject;    // ✨ Assign GameObject with ParticleSystem

    private AudioSource audioSource;
    private ParticleSystem particleSystem;
    private bool hasPlayed = false;

    void Start()
    {
        // Set up AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;

        // Set up Particle System
        if (particleEffectObject != null)
        {
            particleSystem = particleEffectObject.GetComponent<ParticleSystem>();
            particleEffectObject.SetActive(false); // Hide at start
        }
    }

    public void PlayEffect()
    {
        if (hasPlayed) return; // Prevent replay
        hasPlayed = true;

        // 🔊 Play final audio
        if (finalAudio != null)
            audioSource.PlayOneShot(finalAudio);

        // ✨ Activate and play particle
        if (particleEffectObject != null && particleSystem != null)
        {
            particleEffectObject.SetActive(true);
            particleSystem.Stop();
            particleSystem.Play();
        }

        Debug.Log("🎉 Final effect played!");
    }
}
