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
        StopCurrentAction();
        skipRequested = true;
    }

    public void StopCurrentAction()
    {
        print("stoped current action: " + currentAction.id);
        currentAction.stopNow = true;
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
                yield return StartCoroutine(currentAction.Execute(npc));

                if (playExtraAction)
                {
                    currentAction.stopNow = false; 
                    playExtraAction = false;
                    currentAction = extraAction;
                    print("extra action: " + currentAction.id);
                    yield return StartCoroutine(currentAction.Execute(npc));
                    currentAction.stopNow = false;
                    //currentAction.skipAfterAction = false;
                    //currentAction = action;
                }

                // Handle skip
                if (skipRequested || currentAction.skipAfterAction ||currentAction.stopNow)
                {
                    skipRequested = false;
                    print("skip action");
                    break; // move to next action
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
