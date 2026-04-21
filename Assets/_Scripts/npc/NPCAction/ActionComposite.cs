using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionComposite : NPCAction
{
    public List<NPCAction> actions;
    public bool runSequentially = false;

    private List<Coroutine> runningCoroutines = new List<Coroutine>();

    public override IEnumerator Execute(NPCController npc)
    {
        runningCoroutines.Clear();

        if (runSequentially)
        {
            foreach (var action in actions)
            {
                if (stopNow)
                    break;

                bool finished = false;

                Coroutine running = npc.StartCoroutine(
                    RunAction(action, npc, () => finished = true)
                );

                runningCoroutines.Add(running);

                // Wait until either completes OR stop is requested
                yield return new WaitUntil(() => finished || stopNow);

                if (stopNow)
                {
                    action.stopNow = true;
                    npc.StopCoroutine(running);
                    break;
                }
            }
        }
        else
        {
            int completed = 0;
            int total = actions.Count;

            foreach (var action in actions)
            {
                var coroutine = npc.StartCoroutine(
                    RunAction(action, npc, () => completed++)
                );

                runningCoroutines.Add(coroutine);
            }

            yield return new WaitUntil(() => completed >= total || stopNow);
        }

        if (stopNow)
        {
            // Propagate stop to all actions
            foreach (var action in actions)
            {
                action.stopNow = true;
                action.StopAction(npc);
            }
                

            StopAllRunning(npc);
            yield break;
        }
    }

    public override void StopAction(NPCController npc)
    {
        foreach (var action in actions)
        {
            action.stopNow = true;
            action.StopAction(npc);
        }
    }

    private IEnumerator RunAction(NPCAction action, NPCController npc, System.Action onComplete)
    {
        yield return npc.StartCoroutine(action.Execute(npc));
        onComplete?.Invoke();
    }

    private void StopAllRunning(NPCController npc)
    {
        foreach (var coroutine in runningCoroutines)
        {
            if (coroutine != null)
                npc.StopCoroutine(coroutine);
        }

        runningCoroutines.Clear();
    }
}