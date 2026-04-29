using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionComposite : NPCAction
{
    public List<NPCAction> actions = new List<NPCAction>();
    public bool runSequentially = false;

    private List<Coroutine> runningCoroutines = new List<Coroutine>();

    public override IEnumerator Execute(NPCController npc)
    {
        runningCoroutines.Clear();

        foreach (var action in actions)
        {
            action.stopNow = false;
        }

        if (runSequentially)
        {
            foreach (var action in actions)
            {
                if (stopNow) yield break;

                bool finished = false;

                Coroutine running = npc.StartCoroutine(
                    RunAction(action, npc, () => finished = true)
                );

                runningCoroutines.Add(running);

                // Wait only for this action to finish or interruption
                yield return new WaitUntil(() => finished || stopNow);

                if (stopNow)
                {
                    action.stopNow = true;
                    npc.StopCoroutine(running);
                    yield break;
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

            // Wait only until all children finish or interruption
            yield return new WaitUntil(() => completed >= total || stopNow);
        }

        // If interrupted, stop everything
        if (stopNow)
        {
            foreach (var action in actions)
            {
                action.stopNow = true;
                action.StopAction(npc);
            }

            StopAllRunning(npc);
        }
    }

    public override void StopAction(NPCController npc)
    {
        stopNow = true;

        foreach (var action in actions)
        {
            action.stopNow = true;
            action.StopAction(npc);
        }

        StopAllRunning(npc);
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