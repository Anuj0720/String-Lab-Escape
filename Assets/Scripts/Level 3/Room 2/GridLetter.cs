using UnityEngine;
using TMPro;

public class GridLetter : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public char correctLetter = ' ';
    public bool isReplaceable = false;

    public FinalEffectController effectController; // Already using this
    public LevelTimer levelTimer;                  // ✅ Add this reference

    public void Replace(char letter)
    {
        if (isReplaceable)
        {
            if (letter == correctLetter)
            {
                textMesh.text = correctLetter.ToString();
                isReplaceable = false;

                Debug.Log("✅ Correct letter placed!");

                // ✅ Stop the timer
                if (levelTimer != null)
                {
                    levelTimer.StopTimer();
                    Debug.Log("🛑 Timer stopped!");
                }

                // ✅ Play final audio + particle
                if (effectController != null)
                {
                    effectController.PlayEffect();
                }
            }
            else
            {
                Debug.Log("❌ Wrong letter!");
            }
        }
    }
}
