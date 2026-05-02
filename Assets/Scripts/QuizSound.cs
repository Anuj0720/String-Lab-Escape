using UnityEngine;
using UnityEngine.UI;

public class QuizSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip correctSound;
    public AudioClip wrongSound;

    public Button optionA;
    public Button optionB;
    public Button optionC;
    public Button optionD;

    public Button correctButton; // Assign the correct option here

    void Start()
    {
        optionA.onClick.AddListener(() => HandleAnswer(optionA));
        optionB.onClick.AddListener(() => HandleAnswer(optionB));
        optionC.onClick.AddListener(() => HandleAnswer(optionC));
        optionD.onClick.AddListener(() => HandleAnswer(optionD));
    }

    void HandleAnswer(Button clickedButton)
    {
        if (clickedButton == correctButton)
        {
            audioSource.PlayOneShot(correctSound);
            Debug.Log("Correct!");
        }
        else
        {
            audioSource.PlayOneShot(wrongSound);
            Debug.Log("Wrong!");
        }
    }
}
