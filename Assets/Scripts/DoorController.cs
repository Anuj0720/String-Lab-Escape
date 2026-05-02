using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform door;           // Assign the door object in the Inspector
    public float targetY = 90f;      // Target rotation in Y
    public float speed = 50f;        // Rotation speed

    private bool open = false;

    void Update()
    {
        if (open)
        {
            Vector3 currentRotation = door.localEulerAngles;
            float newY = Mathf.MoveTowardsAngle(currentRotation.y, targetY, speed * Time.deltaTime);
            door.localEulerAngles = new Vector3(currentRotation.x, newY, currentRotation.z);
        }
    }

    // Call this method from a UI button or XR interaction
    public void OpenDoor()
    {
        open = true;
    }
}
