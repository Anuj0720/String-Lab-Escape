using UnityEngine;

public class CorrectLetterZone : MonoBehaviour
{
    public GameObject blockQuizParent; // Assign Block_Quiz in Inspector
    private bool triggeredOnce = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered: " + other.name + " with tag: " + other.tag);

        if (triggeredOnce) return;

        if (other.CompareTag("CorrectLetter"))
        {
            Debug.Log("✅ CorrectLetter entered trigger zone!");

            if (blockQuizParent != null)
            {
                FloatLetters floatScript = blockQuizParent.GetComponent<FloatLetters>();
                if (floatScript != null)
                {
                    floatScript.ResumeFloating();
                    triggeredOnce = true;
                }
                else
                {
                    Debug.LogWarning("❌ FloatLetters script not found on Block_Quiz.");
                }
            }
            else
            {
                Debug.LogWarning("❌ blockQuizParent not assigned.");
            }
        }
    }
}
