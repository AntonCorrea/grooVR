using System.Collections;
using UnityEngine;

public class TriggerAnimateAction : NPCAction
{
    public override IEnumerator Execute(NPCController npc)
    {
        npc.TriggerAnimation(id);

        if (loopTime != 0f)
        {
            yield return new WaitForSeconds(loopTime);
        }
        else
        {
            yield return new WaitForSeconds(10f);
        }
    }
}
