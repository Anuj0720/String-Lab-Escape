using UnityEngine;

public class PlaneMover : MonoBehaviour
{
    public GameObject plane;
    public float moveSpeed = 2f;
    public float rotateSpeed = 100f;

    public float startY = 0f;
    public float endY = 5f;

    public float rotationAngleY = 390f; // Total Y rotation

    private bool isMoving = false;
    private bool isRotating = false;
    private bool hasActivatedOnce = false; // ✅ Prevent re-triggering

    private float currentRotatedY = 0f;

    void Start()
    {
        if (plane != null)
        {
            plane.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!hasActivatedOnce && other.CompareTag("Player") && plane != null)
        {
            hasActivatedOnce = true; // ✅ Set flag so it won't trigger again
            plane.SetActive(true);

            // Reset position
            Vector3 pos = plane.transform.position;
            pos.y = startY;
            plane.transform.position = pos;

            // Reset rotation
            plane.transform.rotation = Quaternion.Euler(0, 0, 0);
            currentRotatedY = 0f;

            isMoving = true;
        }
    }

    void Update()
    {
        if (isMoving && plane != null)
        {
            Vector3 pos = plane.transform.position;
            pos.y = Mathf.MoveTowards(pos.y, endY, moveSpeed * Time.deltaTime);
            plane.transform.position = pos;

            if (Mathf.Abs(pos.y - endY) < 0.01f)
            {
                isMoving = false;
                isRotating = true;
            }
        }

        if (isRotating && plane != null)
        {
            float rotateStep = rotateSpeed * Time.deltaTime;
            if (currentRotatedY + rotateStep > rotationAngleY)
            {
                rotateStep = rotationAngleY - currentRotatedY;
            }

            plane.transform.Rotate(0, rotateStep, 0);
            currentRotatedY += rotateStep;

            if (currentRotatedY >= rotationAngleY)
            {
                isRotating = false;
            }
        }
    }
}
