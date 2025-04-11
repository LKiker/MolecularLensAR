/* ProteinLensAR - VisionFollower.cs
 * 
 * Functions for calculating and maintaining the user interface
 * within the camera fov.
 */

using Unity.VisualScripting;
using UnityEngine;

public class VisionFollower : MonoBehaviour
{ 
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float visionDistance = .37f;
    [SerializeField] private float fovTolerance = .5f;
    public bool isUI = false;
    private bool isCentered = false;
    private Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    private void Update()
    {
        Vector3 targetPosition = FindTargetPosition();

        if (!isCentered)
        {
            MoveToward(targetPosition);
            transform.LookAt(cameraTransform);  // keeps forward vector facing the user
            if (isUI)
            {
                transform.Rotate(new Vector3(0f, 180f, 0f)); // forward vector on UI is backward
            }

            if (ReachedPosition(targetPosition))
                isCentered = true;
        } else
        {
            if (Vector3.Distance(targetPosition, transform.position) > fovTolerance)
                isCentered = false;
        }

    }

    // Finds target position in the user's field of view
    private Vector3 FindTargetPosition()
    {
        Debug.Log("target position: " + cameraTransform.position + (cameraTransform.forward * visionDistance));
        return cameraTransform.position + (cameraTransform.forward * visionDistance);
    }

    // Moves the object toward the target position
    private void MoveToward(Vector3 targetPosition)
    {
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 0.5f);
    }

    // Determines if the object has reached the target position
    private bool ReachedPosition(Vector3 targetPosition)
    {
        return Vector3.Distance(targetPosition, transform.position) < 0.05f;
    }
}
