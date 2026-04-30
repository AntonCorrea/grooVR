using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionComposite : NPCAction
{
    public List<NPCAction> actions = new List<NPCAction>();
    public bool runSequentially = false;

    public override IEnumerator Execute(NPCController npc, System.Action<NPCActionResult> onComplete)
    {
        isInterrupted = false;

        if (actions == null || actions.Count == 0)
        {
            onComplete?.Invoke(NPCActionResult.Completed);
            yield break;
        }

        if (runSequentially)
        {
            yield return RunSequential(npc, onComplete);
        }
        else
        {
            yield return RunParallel(npc, onComplete);
        }
    }

    public override void StopAction(NPCController npc)
    {
        isInterrupted = true;

        foreach (var action in actions)
        {
            action.StopAction(npc);
        }
    }

    // ------------------------
    // Sequential execution
    // ------------------------
    private IEnumerator RunSequential(NPCController npc, System.Action<NPCActionResult> onComplete)
    {
        foreach (var action in actions)
        {
            print("composite sequential action: " + action.id);

            if (isInterrupted)
            {
                onComplete?.Invoke(NPCActionResult.Interrupted);
                yield break;
            }

            bool done = false;
            NPCActionResult result = NPCActionResult.Completed;

            yield return npc.StartCoroutine(
                action.Execute(npc, r =>
                {
                    result = r;
                    done = true;
                })
            );

            yield return new WaitUntil(() => done);

            if (isInterrupted)
            {
                action.StopAction(npc);
                onComplete?.Invoke(NPCActionResult.Interrupted);
                yield break;
            }

            if (result == NPCActionResult.Failed)
            {
                onComplete?.Invoke(NPCActionResult.Failed);
                yield break;
            }

            if (result == NPCActionResult.Interrupted)
            {
                onComplete?.Invoke(NPCActionResult.Interrupted);
                yield break;
            }
        }

        onComplete?.Invoke(NPCActionResult.Completed);
    }

    // ------------------------
    // Parallel execution
    // ------------------------
    private IEnumerator RunParallel(NPCController npc, System.Action<NPCActionResult> onComplete)
    {
        int total = actions.Count;
        int completed = 0;

        bool failed = false;
        bool interrupted = false;

        foreach (var action in actions)
        {
            print("composite parallel action: " + action.id);
            npc.StartCoroutine(action.Execute(npc, result =>
            {
                if (result == NPCActionResult.Failed)
                    failed = true;

                if (result == NPCActionResult.Interrupted)
                    interrupted = true;

                completed++;
            }));
        }

        yield return new WaitUntil(() =>
            completed >= total ||
            failed ||
            interrupted ||
            isInterrupted
        );

        // Propagate interruption to all children
        if (isInterrupted || interrupted)
        {
            foreach (var action in actions)
            {
                action.StopAction(npc);
            }

            onComplete?.Invoke(NPCActionResult.Interrupted);
            yield break;
        }

        if (failed)
        {
            onComplete?.Invoke(NPCActionResult.Failed);
            yield break;
        }

        onComplete?.Invoke(NPCActionResult.Completed);
    }
}