using System.Collections;
using UnityEngine;

public class ActionTriggerAnimation : NPCAction
{
    public string stopTrigger;
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

    public override void StopAction(NPCController npc)
    {
        npc.TriggerAnimation(stopTrigger);
    }
}
