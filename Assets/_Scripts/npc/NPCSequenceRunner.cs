using System.Collections;
using UnityEngine;

public class NPCSequenceRunner : MonoBehaviour
{
    public NPCController npc;
    private bool skipRequested = false;
    private NPCAction currentAction;

    bool playExtraAction = false;
    NPCAction extraAction;
    public void PlaySequence(NPCSequence sequence)
    {
        StartCoroutine(Run(sequence));
    }

    [ContextMenu("Skip")]
    public void Skip()
    {
        StopCurrentAction();
        skipRequested = true;
    }

    public void StopCurrentAction()
    {
        print("stoped action: " + currentAction.id);
        currentAction.stopNow = true;
    }

    private IEnumerator Run(NPCSequence sequence)
    {
        foreach (var action in sequence.actions)
        {
            currentAction = action;
            bool repeat = true;
            do
            {
                print("action: " + currentAction.id);
                yield return StartCoroutine(currentAction.Execute(npc));

                if (playExtraAction)
                {
                    currentAction.stopNow = false;
                    playExtraAction = false;
                    currentAction = extraAction;
                    print("extra action: " + currentAction.id);
                    yield return StartCoroutine(currentAction.Execute(npc));
                    currentAction.skipAfterAction = false;
                    currentAction = action;
                }

                // Handle skip
                if (skipRequested || currentAction.skipAfterAction ||currentAction.stopNow)
                {
                    skipRequested = false;
                    break; // move to next action
                }                    
            } while (repeat);
        }
    }

    public void PlaySingleAction(NPCAction action)
    {
        StopCurrentAction();
        playExtraAction = true;
        extraAction = action;   
    }
}
