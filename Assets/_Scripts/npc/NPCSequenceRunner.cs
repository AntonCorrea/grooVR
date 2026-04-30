using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSequenceRunner : MonoBehaviour
{
    public NPCController npc;

    private bool skipRequested = false;

    public NPCAction currentAction;

    bool playExtraAction = false;
    NPCAction extraAction;

    public void PlaySequence(List<NPCAction> sequence)
    {
        StartCoroutine(Run(sequence));
    }

    [ContextMenu("Skip")]
    public void Skip()
    {
        skipRequested = true;
        StopCurrentAction();
    }

    public void StopCurrentAction()
    {
        if (currentAction == null) return;

        print("stopped current action: " + currentAction.id);
        currentAction.StopAction(npc);
    }

    private IEnumerator Run(List<NPCAction> sequence)
    {
        foreach (var action in sequence)
        {
            currentAction = action;

            bool repeat = true;

            do
            {
                print("action: " + currentAction.id);

                bool done = false;
                NPCActionResult result = NPCActionResult.Completed;

                yield return StartCoroutine(
                    currentAction.Execute(npc, r =>
                    {
                        result = r;
                        done = true;
                    })
                );

                yield return new WaitUntil(() => done);

                // ----- EXTRA ACTION -----
                if (playExtraAction)
                {
                    playExtraAction = false;

                    var previous = currentAction;
                    currentAction = extraAction;

                    print("extra action: " + currentAction.id);

                    bool extraDone = false;
                    NPCActionResult extraResult = NPCActionResult.Completed;

                    yield return StartCoroutine(
                        currentAction.Execute(npc, r =>
                        {
                            extraResult = r;
                            extraDone = true;
                        })
                    );

                    yield return new WaitUntil(() => extraDone);

                    currentAction = previous;

                    // If extra was interrupted → propagate skip
                    if (extraResult == NPCActionResult.Interrupted)
                    {
                        skipRequested = false;
                        break;
                    }
                }

                // ----- SKIP LOGIC -----

                if (skipRequested)
                {
                    skipRequested = false;
                    print("skip action (external)");
                    break;
                }

                if (currentAction.skipAfterComplete)
                {
                    print("skip action (completed)");
                    break;
                }

                if (result == NPCActionResult.Interrupted)
                {
                    print("skip action (interrupted)");
                    break;
                }

                if (result == NPCActionResult.Failed)
                {
                    print("action failed, skipping");
                    break;
                }




            } while (repeat);
        }

        print("sequence ended");
    }

    public void PlaySingleAction(NPCAction action)
    {
        StopCurrentAction();

        playExtraAction = true;
        extraAction = action;
    }
}