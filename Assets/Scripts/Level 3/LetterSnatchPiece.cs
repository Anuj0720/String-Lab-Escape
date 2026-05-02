using UnityEngine;

public class LetterSnatchPiece : MonoBehaviour
{
    public bool isWrong = true;

    // Optional — useful if you need future expansion
    public void RemoveFromScene()
    {
        Destroy(gameObject);
    }
}
