using System.Collections;
using UnityEngine;

public enum NPCActionResult
{
    Completed,     // finished normally
    Interrupted,   // externally cancelled
    Failed         // could not complete (invalid data, blocked, etc.)
}
public abstract class NPCAction : MonoBehaviour
{
    public string id;

    public float actionClipTime = 0f;

    protected bool isInterrupted = false;

    public bool skipAfterComplete = false;

    public abstract IEnumerator Execute(NPCController npc, System.Action<NPCActionResult> onComplete);

    public virtual void StopAction(NPCController npc)
    {
        isInterrupted = true;
    }
}

//example:
//public override IEnumerator Execute(NPCController npc, System.Action<NPCActionResult> onComplete)
//{
//    isInterrupted = false;

//    // setup

//    while (true)
//    {
//        if (isInterrupted)
//        {
//            Cleanup(npc);
//            onComplete?.Invoke(NPCActionResult.Interrupted);
//            yield break;
//        }

//        if (/* success condition */)
//        {
//            Cleanup(npc);
//            onComplete?.Invoke(NPCActionResult.Completed);
//            yield break;
//        }

//        if (/* failure condition */)
//        {
//            Cleanup(npc);
//            onComplete?.Invoke(NPCActionResult.Failed);
//            yield break;
//        }

//        yield return null;
//    }
//}