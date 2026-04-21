using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ActionContextMethod : NPCAction
{
    public UnityEvent actionEvent;
    public UnityEvent contextEvent = new UnityEvent();
    public override IEnumerator Execute(NPCController npc)
    {
        if (actionEvent != null && contextEvent != null)
        {
            actionEvent.AddListener(OnActionTriggered);
        }

        while (!stopNow)
        {
            yield return null;
        }
        yield break; // stops the coroutine immediately

    }

    public override void StopAction(NPCController npc)
    {
        if (actionEvent != null)
        {
            actionEvent.RemoveListener(OnActionTriggered);
        }
    }

    //bool triggered = false;
    void OnActionTriggered()
    {
        //if (triggered) return;
        //triggered = true;

        contextEvent?.Invoke();
        //stopNow = true;
    }
}
