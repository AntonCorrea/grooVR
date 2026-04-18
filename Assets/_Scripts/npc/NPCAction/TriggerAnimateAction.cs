using System.Collections;
using UnityEngine;

public class TriggerAnimateAction : NPCAction
{
    public override IEnumerator Execute(NPCController npc)
    {
        npc.TriggerAnimation(id);

        if (actionClipTime != 0f)
        {
            yield return new WaitForSeconds(actionClipTime);
        }
        else
        {
            yield return new WaitForSeconds(10f);
        }
    }
}
