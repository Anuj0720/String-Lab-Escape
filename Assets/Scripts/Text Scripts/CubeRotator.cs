using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CubeRotator : MonoBehaviour
{
    public float rotationAmount = 90f;
    public float rotationSpeed = 300f;

    private bool isRotating = false;
    private Quaternion targetRotation;

    private void Start()
    {
        targetRotation = transform.rotation;
    }

    private void Update()
    {
        if (isRotating)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                transform.rotation = targetRotation;
                isRotating = false;
            }
        }
    }

    public void RotateCube()
    {
        if (isRotating) return;

        Vector3 nextEuler = transform.eulerAngles + new Vector3(0, rotationAmount, 0);
        targetRotation = Quaternion.Euler(nextEuler);
        isRotating = true;
    }
}
