using UnityEngine;
using TMPro;

public class NeonText : MonoBehaviour
{
    [Header("Target Texts (Only apply to these)")]
    public TextMeshProUGUI[] selectedTexts;

    [Header("Outline Settings")]
    public Color outlineColor = Color.cyan;
    public float minThickness = 0.0f;
    public float maxThickness = 0.5f;
    public float pulseSpeed = 2f;

    void Update()
    {
        float thickness = Mathf.Lerp(minThickness, maxThickness, (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f);

        foreach (var text in selectedTexts)
        {
            if (text == null) continue;

            Material mat = text.fontMaterial;

            if (mat.HasProperty("_OutlineColor") && mat.HasProperty("_OutlineWidth"))
            {
                // Ensure outline is enabled in TMP material inspector
                mat.SetColor("_OutlineColor", outlineColor);
                mat.SetFloat("_OutlineWidth", thickness);
            }
        }
    }
}
