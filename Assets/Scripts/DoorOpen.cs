using UnityEngine;
using UnityEngine.InputSystem;

public class DoorOpen : MonoBehaviour
{
    [Header("Door Settings")]
    public float openAngleY = -90f;         // Angle to open
    public float rotationSpeed = 2f;        // Rotation speed

    [Header("Input Settings")]
    public InputActionProperty triggerAction; // Reference to controller trigger

    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + openAngleY, transform.eulerAngles.z);
    }

    void Update()
    {
        // Input from trigger (works with XR Simulator left mouse)
        if (triggerAction.action.WasPressedThisFrame())
        {
            isOpen = !isOpen;
        }

        // Smooth rotation
        if (isOpen)
            transform.rotation = Quaternion.Slerp(transform.rotation, openRotation, Time.deltaTime * rotationSpeed);
        else
            transform.rotation = Quaternion.Slerp(transform.rotation, closedRotation, Time.deltaTime * rotationSpeed);
    }
}
