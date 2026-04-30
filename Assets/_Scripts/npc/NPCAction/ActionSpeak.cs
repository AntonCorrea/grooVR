using System.Collections;
using System.Linq;
using UnityEngine;

public class ActionSpeak : NPCAction
{
    private void Start()
    {
        skipAfterComplete = true;
    }
    public override IEnumerator Execute(NPCController npc, System.Action<NPCActionResult> onComplete)
    {
        isInterrupted = false;

        SpeakActionElement speakElement = npc.speakList.FirstOrDefault(i => i.id == id);

        if (speakElement == null)
        {
            onComplete?.Invoke(NPCActionResult.Failed);
            yield break;
        }

        actionClipTime = speakElement.timeLenght;

        npc.PlayVoice(speakElement.clip);
        npc.SetSubsText(speakElement.subtitles);

        float duration = (actionClipTime != 0f) ? actionClipTime : 10f;
        float timer = 0f;

        while (timer < duration)
        {
            if (isInterrupted)
            {
                onComplete?.Invoke(NPCActionResult.Interrupted);
                yield break;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        onComplete?.Invoke(NPCActionResult.Completed);
    }

    public override void StopAction(NPCController npc)
    {
        isInterrupted = true;
    }
}