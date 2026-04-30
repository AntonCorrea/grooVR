using System.Collections;
using UnityEngine;

public class ActionPlayAnimation : NPCAction
{
    private void Start()
    {
        skipAfterComplete = true;
    }
    public override IEnumerator Execute(NPCController npc, System.Action<NPCActionResult> onComplete)
    {
        isInterrupted = false;

        npc.PlayAnimation(id);

        float duration = (actionClipTime != 0f) ? actionClipTime : 10f;
        float timer = 0f;

        while (timer < duration)
        {
            if (isInterrupted)
            {
                npc.StopAnimation();
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
        npc.StopAnimation();
    }
}