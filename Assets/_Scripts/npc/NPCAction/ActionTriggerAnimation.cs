using System.Collections;
using UnityEngine;

public class ActionTriggerAnimation : NPCAction
{
    public string stopTrigger;

    public override IEnumerator Execute(NPCController npc, System.Action<NPCActionResult> onComplete)
    {
        isInterrupted = false;

        npc.TriggerAnimation(id);

        float duration = (actionClipTime != 0f) ? actionClipTime : 10f;
        float timer = 0f;

        while (timer < duration)
        {
            if (isInterrupted)
            {
                npc.TriggerAnimation(stopTrigger);
                onComplete?.Invoke(NPCActionResult.Interrupted);
                yield break;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        onComplete?.Invoke(NPCActionResult.Completed);
    }

    public override void StopAction(NPCController npc)
    {
        isInterrupted = true;
        npc.TriggerAnimation(stopTrigger);
    }
}