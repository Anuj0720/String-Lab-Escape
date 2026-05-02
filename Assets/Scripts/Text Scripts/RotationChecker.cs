using System.Collections;
using UnityEngine;

public class RotationChecker : MonoBehaviour
{
    [Header("Cubes to Check")]
    public Transform[] cubes;

    [Header("Target Rotations (Euler Angles)")]
    public Vector3[] targetRotations;

    [Header("Rotation Tolerance (degrees)")]
    public float tolerance = 5f;

    [Header("Door Parts")]
    public Transform doorTop;
    public Transform doorBottom;
    public float moveDistance = 2f;
    public float moveDuration = 2f;

    [Header("Door Sound")]
    public AudioSource doorAudioSource;

    private bool doorIsOpen = false;
    private Coroutine doorCoroutine;

    // New: Store initial positions
    private Vector3 topInitialPos;
    private Vector3 bottomInitialPos;

    void Start()
    {
        topInitialPos = doorTop.position;
        bottomInitialPos = doorBottom.position;
    }

    void Update()
    {
        bool matched = AllCubesMatchRotation();

        if (matched && !doorIsOpen)
        {
            if (doorCoroutine != null) StopCoroutine(doorCoroutine);
            doorCoroutine = StartCoroutine(OpenDoor());
            doorIsOpen = true;
        }
        else if (!matched && doorIsOpen)
        {
            if (doorCoroutine != null) StopCoroutine(doorCoroutine);
            doorCoroutine = StartCoroutine(CloseDoor());
            doorIsOpen = false;
        }
    }

    bool AllCubesMatchRotation()
    {
        for (int i = 0; i < cubes.Length; i++)
        {
            if (i >= targetRotations.Length) return false;

            Vector3 currentEuler = cubes[i].rotation.eulerAngles;
            Vector3 targetEuler = targetRotations[i];

            if (!AnglesClose(currentEuler.x, targetEuler.x) ||
                !AnglesClose(currentEuler.y, targetEuler.y) ||
                !AnglesClose(currentEuler.z, targetEuler.z))
            {
                return false;
            }
        }
        return true;
    }

    bool AnglesClose(float a, float b)
    {
        float diff = Mathf.Abs(Mathf.DeltaAngle(a, b));
        return diff <= tolerance;
    }

    IEnumerator OpenDoor()
    {
        if (doorAudioSource) doorAudioSource.Play();

        Vector3 topTarget = topInitialPos + Vector3.up * moveDistance;
        Vector3 bottomTarget = bottomInitialPos + Vector3.down * moveDistance;

        float elapsed = 0f;
        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / moveDuration);

            doorTop.position = Vector3.Lerp(topInitialPos, topTarget, t);
            doorBottom.position = Vector3.Lerp(bottomInitialPos, bottomTarget, t);
            yield return null;
        }
    }

    IEnumerator CloseDoor()
    {
        if (doorAudioSource) doorAudioSource.Play();

        Vector3 topOpenPos = topInitialPos + Vector3.up * moveDistance;
        Vector3 bottomOpenPos = bottomInitialPos + Vector3.down * moveDistance;

        float elapsed = 0f;
        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / moveDuration);

            doorTop.position = Vector3.Lerp(topOpenPos, topInitialPos, t);
            doorBottom.position = Vector3.Lerp(bottomOpenPos, bottomInitialPos, t);
            yield return null;
        }
    }
}
