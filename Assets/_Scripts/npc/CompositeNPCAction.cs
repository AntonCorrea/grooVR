using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositeNPCAction : NPCAction
{
    public List<NPCAction> actions;

    public override IEnumerator Execute(NPCController npc)
    {
        int completed = 0;
        int total = actions.Count;

        foreach (var action in actions)
        {
            npc.StartCoroutine(RunAction(action, npc, () => completed++));
        }

        yield return new WaitUntil(() => completed >= total || stopNow);

        if (stopNow)
        {
            //foreach (var action in actions)
            //    action.stopNow = true;
            yield break;
        }
    }

    private IEnumerator RunAction(NPCAction action, NPCController npc, System.Action onComplete)
    {
        yield return npc.StartCoroutine(action.Execute(npc));
        onComplete?.Invoke();
    }
}
