using UnityEngine;

public class FixBinDetector : MonoBehaviour
{
    public GameObject wordRoot; // Assign 'Block_Quiz' in Inspector
    public DoorController doorController; // Assign DoorController in Inspector
    public AudioClip doorOpenSound; // 🎵 Assign your door open sound in Inspector

    private bool doorOpened = false;
    private AudioSource audioSource;

    private void Start()
    {
        // Add AudioSource component to this GameObject (if not already)
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WrongLetter"))
        {
            Debug.Log("🗑 Wrong letter entered bin: " + other.name);

            // Remove the letter
            Destroy(other.gameObject);

            // Refresh float anchors
            if (wordRoot != null)
            {
                FloatLetters floatScript = wordRoot.GetComponent<FloatLetters>();
                if (floatScript != null)
                {
                    floatScript.CacheChildrenPositions();
                }
            }

            // Open the door (once) and play sound
            if (!doorOpened && doorController != null)
            {
                doorController.OpenDoor();

                if (doorOpenSound != null)
                    audioSource.PlayOneShot(doorOpenSound); // 🔊 Play sound here

                doorOpened = true;
            }
        }
    }
}
