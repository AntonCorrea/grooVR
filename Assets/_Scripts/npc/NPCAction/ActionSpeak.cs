using System.Collections;
using System.Linq;
using UnityEngine;

public class ActionSpeak : NPCAction
{

    public override IEnumerator Execute(NPCController npc)
    {
        SpeakActionElement speakElement = npc.speakList.FirstOrDefault(i => i.id == id);

        actionClipTime = speakElement.timeLenght;

        npc.PlayVoice(speakElement.clip);
        npc.SetSubsText(speakElement.subtitles);

        float duration = (actionClipTime != 0f) ? actionClipTime : 10f;
        float timer = 0f;
        while (timer < duration)
        {
            if (stopNow)
            {
                yield break; // stops the coroutine immediately
            }

            timer += Time.deltaTime;
            yield return null; // wait one frame (interruptible)
        }

    }

    public override void StopAction(NPCController npc)
    {

    }
}
