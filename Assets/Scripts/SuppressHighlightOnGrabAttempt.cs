using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.Input;

public class SuppressHighlightOnGrabAttempt : MonoBehaviour
{
    public bool suppressHighlight = false;

    private void OnEnable()
    {
        Grabbable grabbable = GetComponent<Grabbable>();
        if (grabbable != null)
        {
            grabbable.WhenPointerEventRaised += HandlePointerEvent;
        }
    }

    private void OnDisable()
    {
        Grabbable grabbable = GetComponent<Grabbable>();
        if (grabbable != null)
        {
            grabbable.WhenPointerEventRaised -= HandlePointerEvent;
        }
    }

    private void HandlePointerEvent(PointerEvent evt)
    {
        if (evt.Type == PointerEventType.Select)
        {
            suppressHighlight = true;
        }
        else if (evt.Type == PointerEventType.Unselect)
        {
            suppressHighlight = false;
        }
    }
}
