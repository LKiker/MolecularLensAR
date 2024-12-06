/* ProteinLensAR - DisableRayWhileGrabbing.cs
 * 
 * Ensures the ray interactors while grabbing an object will be
 * disabled so the Protein is not highlighted while doing transforms.
 * The functions utilize the listeners from the Grabbable class by Meta.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class DisableRayWhileGrabbing : MonoBehaviour
{
    public GameObject[] rayInteractors;
    private Grabbable grabbable;

    // Gets the grabbable component on the Hand Interaction
    private void Awake()
    {
        grabbable = GetComponent<Grabbable>();
    }

    // When enabling the object (Unity function)
    private void OnEnable()
    {
        if (grabbable != null)
        {
            grabbable.WhenPointerEventRaised += handlePointerEvent;
        }
    }

    // When disabling the object (Unity function)
    private void OnDisable()
    {
        if (grabbable != null)
        {
            grabbable.WhenPointerEventRaised -= handlePointerEvent;
        }
    }

    // Handles the event onEnable and onDisable
    private void handlePointerEvent(PointerEvent pointerEvent)
    {
        switch (pointerEvent.Type)
        {
            case PointerEventType.Select: // Grab starts
                setRayInteractorsActive(false);
                break;

            case PointerEventType.Unselect: // Grab ends
                setRayInteractorsActive(true);
                break;
        }
    }

    // Disables the ray interactors when grabbed
    private void setRayInteractorsActive(bool active)
    {
        foreach (var rayInteractor in rayInteractors)
        {
            if (rayInteractor != null)
            {
                rayInteractor.SetActive(active);
            }
        }
    }
}