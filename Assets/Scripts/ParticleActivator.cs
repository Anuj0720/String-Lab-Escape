using UnityEngine;

public class ParticleActivator : MonoBehaviour
{
    public GameObject particleObject;      // Assign the GameObject with ParticleSystem
    public string triggerTag = "Player";   // Tag of object that triggers the particle

    private bool hasActivated = false;
    private ParticleSystem particleSystem;

    void Start()
    {
        if (particleObject != null)
        {
            particleSystem = particleObject.GetComponent<ParticleSystem>();
            particleObject.SetActive(false); // Hide particle at start
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasActivated && other.CompareTag(triggerTag))
        {
            hasActivated = true;

            if (particleObject != null)
            {
                particleObject.SetActive(true);

                if (particleSystem != null)
                    particleSystem.Play();

                Debug.Log("✨ Particle played after collision!");
            }
        }
    }
}
