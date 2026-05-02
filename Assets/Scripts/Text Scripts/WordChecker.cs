using System.Collections;
using UnityEngine;
using TMPro;

public class WordChecker : MonoBehaviour
{
    [System.Serializable]
    public class WordSlot
    {
        public string targetWord;
        public Transform[] slots;
    }

    public WordSlot[] wordSlots;

    [Header("Door Parts")]
    public Transform doorTop;
    public Transform doorBottom;

    [Header("Door Settings")]
    public float moveDistance = 2f;
    public float moveDuration = 2f;

    [Header("Door Sound")]
    public AudioSource doorAudioSource; // Assign in Inspector

    private bool doorOpened = false;

    void Update()
    {
        if (doorOpened) return;

        int matchedWords = 0;

        foreach (var word in wordSlots)
        {
            string formedWord = "";

            foreach (Transform slot in word.slots)
            {
                if (slot.childCount > 0)
                {
                    GameObject cube = slot.GetChild(0).gameObject;
                    TextMeshProUGUI text = cube.GetComponentInChildren<TextMeshProUGUI>();

                    if (text != null && !string.IsNullOrWhiteSpace(text.text))
                    {
                        string letter = text.text.Trim().ToUpper();
                        if (letter.Length > 0)
                            formedWord += letter[0];
                    }
                }
            }

            Debug.Log($"Formed: {formedWord} | Target: {word.targetWord.ToUpper()}");

            if (formedWord == word.targetWord.ToUpper())
            {
                matchedWords++;
            }
        }

        if (matchedWords == wordSlots.Length)
        {
            Debug.Log("✅ All words correct — opening split door!");
            StartCoroutine(OpenDoor());
            doorOpened = true;
        }
    }

    IEnumerator OpenDoor()
    {
        // 🔊 Play door sound
        if (doorAudioSource != null)
        {
            doorAudioSource.Play();
        }

        Vector3 topStart = doorTop.position;
        Vector3 topEnd = topStart + Vector3.up * moveDistance;

        Vector3 bottomStart = doorBottom.position;
        Vector3 bottomEnd = bottomStart + Vector3.down * moveDistance;

        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moveDuration);
            float smoothT = Mathf.SmoothStep(0f, 1f, t);

            doorTop.position = Vector3.Lerp(topStart, topEnd, smoothT);
            doorBottom.position = Vector3.Lerp(bottomStart, bottomEnd, smoothT);

            yield return null;
        }

        Debug.Log("🚪 Door opened (top up, bottom down)");
    }
}
