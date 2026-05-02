using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class ReplaceHammer : MonoBehaviour
{
    [Header("Letter Settings")]
    public char[] letters = { 'A', 'E', 'I', 'O', 'T' }; // Your hammer letters

    public TextMeshProUGUI faceTextFront;
    public TextMeshProUGUI faceTextBack;

    [Header("XR Controller")]
    public ActionBasedController controller;  // Assign the XR controller

    private char currentLetter;
    private bool previousPressState = false;

    void Start()
    {
        ShuffleLetter();
    }

    void Update()
    {
        if (controller != null && controller.activateAction != null)
        {
            bool isPressed = controller.activateAction.action.ReadValue<float>() > 0.5f;

            if (isPressed && !previousPressState)
            {
                ShuffleLetter();
            }

            previousPressState = isPressed;
        }
    }

    public void ShuffleLetter()
    {
        int index = Random.Range(0, letters.Length);
        currentLetter = letters[index];

        if (faceTextFront != null)
            faceTextFront.text = currentLetter.ToString();

        if (faceTextBack != null)
            faceTextBack.text = currentLetter.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("WrongLetter"))
        {
            GridLetter letter = collision.gameObject.GetComponent<GridLetter>();
            if (letter != null)
            {
                letter.Replace(currentLetter);
            }
        }
    }
}
