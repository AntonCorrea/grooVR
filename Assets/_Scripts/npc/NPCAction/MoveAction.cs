using System.Collections;
using System.Linq;
using UnityEngine;

public class MoveAction : NPCAction
{
    public float arrivalThreshold = 0.1f;

    public override IEnumerator Execute(NPCController npc)
    {
        MoveActionElement moveElement = npc.moveList.FirstOrDefault(i => i.id == id);

        npc.MoveTo(moveElement.target);

        while (!npc.HasReachedTarget(arrivalThreshold))
        {
            yield return null;
        }

        npc.Stop();
        skipAfterAction = true;
        stopNow = true;
    }
}
