using UnityEngine;
using Oculus.Interaction;

public class TriggerTutorial2OnRelease : MonoBehaviour
{
    [SerializeField] private TutorialManager tutorialManager;
    private Grabbable grabbable;

    private void Awake()
    {
        grabbable = GetComponent<Grabbable>();
    }

    private void OnEnable()
    {
        if (grabbable != null)
        {
            grabbable.WhenPointerEventRaised += HandlePointerEvent;
        }
    }

    private void OnDisable()
    {
        if (grabbable != null)
        {
            grabbable.WhenPointerEventRaised -= HandlePointerEvent;
        }
    }

    private void HandlePointerEvent(PointerEvent pointerEvent)
    {
        if (pointerEvent.Type == PointerEventType.Unselect)
        {
            Debug.Log("Object released — triggering tutorial success");
            if (tutorialManager != null)
            {
                tutorialManager.EnableTutorial2Success();
            }
        }
    }
}
