using System.Collections;
using UnityEngine;

public class PlayAnimateAction : NPCAction
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
                yield break; // stops the coroutine immediately
            }

            timer += Time.deltaTime;
            yield return null; // wait one frame (interruptible)
        }
        //skipAfter = true;
    }
}
