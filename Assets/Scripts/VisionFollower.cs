using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class VisionFollower : MonoBehaviour
{ 
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float distance = .37f;

    private bool isCentered = false;
    private Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    private void Update()
    {
        Vector3 targetPosition = FindTargetPosition();

        if (!isCentered)
        {
            MoveToward(targetPosition);
            transform.LookAt(cameraTransform);
            transform.Rotate(new Vector3(0f, 180f, 0f));

            if (ReachedPosition(targetPosition))
                isCentered = true;
        } else
        {
            if (Vector3.Distance(targetPosition, transform.position) > 0.3f)
                isCentered = false;
        }

    }

    private Vector3 FindTargetPosition()
    {
        Debug.Log("target position: " + cameraTransform.position + (cameraTransform.forward * distance));
        return cameraTransform.position + (cameraTransform.forward * distance);
    }

    private void MoveToward(Vector3 targetPosition)
    {
        //transform.position += (targetPosition - transform.position) * 0.05f;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 0.3f);
    }

    private bool ReachedPosition(Vector3 targetPosition)
    {
        return Vector3.Distance(targetPosition, transform.position) < 0.05f;
    }
}
