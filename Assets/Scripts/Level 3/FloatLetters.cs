using UnityEngine;
using System.Collections.Generic;

public class FloatLetters : MonoBehaviour
{
    public float floatStrength = 0.5f;
    public float floatSpeed = 2f;

    [Tooltip("Direction to float in (e.g., (0,1,0) = Y axis)")]
    public Vector3 floatDirection = Vector3.up; // Default = Y-axis

    private Dictionary<Transform, Vector3> originalPositions = new Dictionary<Transform, Vector3>();
    private bool globalFloating = true;

    void Start()
    {
        CacheChildrenPositions();
    }

    void Update()
    {
        if (!globalFloating) return;

        foreach (var kvp in originalPositions)
        {
            Transform child = kvp.Key;
            if (child != null)
            {
                Vector3 startPos = kvp.Value;

                float offset = Mathf.Sin(Time.time * floatSpeed + child.GetSiblingIndex()) * floatStrength;

                // Apply movement in the specified direction
                Vector3 newPos = startPos + (floatDirection.normalized * offset);
                child.localPosition = newPos;
            }
        }
    }

    public void CacheChildrenPositions()
    {
        originalPositions.Clear();

        foreach (Transform child in transform)
        {
            originalPositions[child] = child.localPosition;
        }
    }

    public void PauseFloating()
    {
        globalFloating = false;
    }

    public void ResumeFloating()
    {
        globalFloating = true;
    }
}
