using System.Collections;
using UnityEngine;

public class ActionPlayAnimation : NPCAction
{
    public override IEnumerator Execute(NPCController npc)
    {
        npc.PlayAnimation(id);

        float duration = (actionClipTime != 0f) ? actionClipTime : 10f;
        float timer = 0f;
        while (timer < duration)
        {
            if (stopNow)
            {
                npc.StopAnimation();
                yield break; // stops the coroutine immediately
            }

            timer += Time.deltaTime;
            yield return null; // wait one frame (interruptible)
        }
        //skipAfter = true;
    }

    public override void StopAction(NPCController npc)
    {
        npc.StopAnimation();
    }
}
