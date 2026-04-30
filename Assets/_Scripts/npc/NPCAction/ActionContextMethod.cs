using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ActionContextMethod : NPCAction
{
    public UnityEvent actionEvent;
    public UnityEvent contextEvent = new UnityEvent();

    private bool triggered = false;

    public override IEnumerator Execute(NPCController npc, System.Action<NPCActionResult> onComplete)
    {
        isInterrupted = false;
        triggered = false;

        if (actionEvent != null)
            actionEvent.AddListener(OnActionTriggered);

        // DO NOT WAIT
        onComplete?.Invoke(NPCActionResult.Completed);

        yield break;

        if (isInterrupted)
        {
            onComplete?.Invoke(NPCActionResult.Interrupted);
        }
        else
        {
            onComplete?.Invoke(NPCActionResult.Completed);
        }
    }

    public override void StopAction(NPCController npc)
    {
        isInterrupted = true;

        if (actionEvent != null)
            actionEvent.RemoveListener(OnActionTriggered);
    }

    void OnActionTriggered()
    {
        if (triggered) return;

        triggered = true;
        contextEvent?.Invoke();
    }
}