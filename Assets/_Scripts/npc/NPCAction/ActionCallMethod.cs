using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ActionCallMethod : NPCAction
{
    public UnityEvent onCall = new UnityEvent();

    public override IEnumerator Execute(NPCController npc, System.Action<NPCActionResult> onComplete)
    {
        isInterrupted = false;

        onCall?.Invoke();

        onComplete?.Invoke(NPCActionResult.Completed);
        yield break;
    }

    public override void StopAction(NPCController npc)
    {
        isInterrupted = true;
    }
}
