using System.Collections;
using System.Linq;
using UnityEngine;

public class ActionMove : NPCAction
{
    public float arrivalThreshold = 0.1f;

    void Start()
    {
        skipAfterComplete = true;
    }
    public override IEnumerator Execute(NPCController npc, System.Action<NPCActionResult> onComplete)
    {
        isInterrupted = false;

        MoveActionElement moveElement = npc.moveList.FirstOrDefault(i => i.id == id);

        if (moveElement == null)
        {
            onComplete?.Invoke(NPCActionResult.Failed);
            yield break;
        }

        npc.MoveTo(moveElement.target);

        while (!npc.HasReachedTarget(arrivalThreshold))
        {
            if (isInterrupted)
            {
                npc.StopMoving();
                onComplete?.Invoke(NPCActionResult.Interrupted);
                yield break;
            }

            yield return null;
        }

        npc.StopMoving();
        onComplete?.Invoke(NPCActionResult.Completed);
    }

    public override void StopAction(NPCController npc)
    {
        isInterrupted = true;
        npc.StopMoving();
    }
}