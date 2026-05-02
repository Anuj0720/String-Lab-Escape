using UnityEngine;

public class PlaySound : MonoBehaviour
{
    private AudioSource audioSource;
    private bool hasPlayed = false;

    // 🔄 Static reference to the currently playing audio
    private static AudioSource currentlyPlayingAudio = null;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("No AudioSource found on this GameObject.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!hasPlayed && other.CompareTag("Player"))
        {
            hasPlayed = true;

            if (audioSource != null && audioSource.clip != null)
            {
                // ❌ Stop the currently playing audio if it's different
                if (currentlyPlayingAudio != null && currentlyPlayingAudio != audioSource)
                {
                    currentlyPlayingAudio.Stop();
                }

                // ✅ Set this as the currently playing audio and play
                currentlyPlayingAudio = audioSource;
                audioSource.Play();

                StartCoroutine(DestroyAfterAudio());
            }
            else
            {
                Destroy(gameObject); // fallback
            }
        }
    }

    System.Collections.IEnumerator DestroyAfterAudio()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        if (currentlyPlayingAudio == audioSource)
        {
            currentlyPlayingAudio = null;
        }
        Destroy(gameObject);
    }
}
