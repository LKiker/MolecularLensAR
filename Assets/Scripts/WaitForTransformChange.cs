using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForTransformChange : MonoBehaviour
{
    public GameObject objectToMove;
    public float movementThreshold = 0.5f; // Minimum distance to consider movement
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 initialScale;
    UIController uiController;

    // Start is called before the first frame update
    void OnEnable()
    {
        uiController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
        initialPosition = objectToMove.transform.position;
        initialRotation = objectToMove.transform.rotation;
        initialScale = objectToMove.transform.localScale;
        StartCoroutine(WaitForMovementCoroutine());
    }

    IEnumerator WaitForMovementCoroutine()
    {
        yield return new WaitUntil(() =>
            Vector3.Distance(initialPosition, objectToMove.transform.position) > movementThreshold);
        Debug.Log("Enable tutorial 2 success called");
        // Rest transform of cube in case tutorial is repeated
        objectToMove.transform.position = initialPosition;
        objectToMove.transform.rotation = initialRotation;
        objectToMove.transform.localScale = initialScale;
        uiController.enableTutorial2Success();
    }
}
