using System.Collections.Generic;
using UnityEngine;

public class NPCTutorialSequence : MonoBehaviour
{
    public NPCSequence sequence;
    public NPCSequenceRunner runner;


    void Start()
    {
        CreateSequence();
        runner.PlaySequence(sequence);
    }

    void CreateSequence()
    {
        sequence = gameObject.AddComponent<NPCSequence>();
        sequence.actions = new List<NPCAction>();

        //SpeakAction speak = gameObject.AddComponent<SpeakAction>();
        //speak.id = "pressToStart";
        //sequence.actions.Add(speak);

        //MoveAction move = gameObject.AddComponent<MoveAction>();
        //move.id = "moveToTable";
        //sequence.actions.Add(move);

        PlayAnimateAction animationGreet = gameObject.AddComponent<PlayAnimateAction>();
        animationGreet.id = "Greet";
        //animationGreet.skipAfter = true;
        //sequence.actions.Add(animation);

        SpeakAction speakGreet = gameObject.AddComponent<SpeakAction>();
        speakGreet.id = "sayGreet";
        //sequence.actions.Add(speak);

        CompositeNPCAction greet = gameObject.AddComponent<CompositeNPCAction>();
        greet.id = "greet animation and speak";
        greet.actions = new List<NPCAction>();
        greet.actions.Add(animationGreet);
        greet.actions.Add(speakGreet);
        greet.skipAfter = true;
        sequence.actions.Add(greet);

        TriggerAnimateAction triggerAnimation = gameObject.AddComponent<TriggerAnimateAction>();
        triggerAnimation.id = "HandUp_Left";
        sequence.actions.Add(triggerAnimation);
    }

    [ContextMenu("UseTheOtherLeft")]
    public void UseTheOtherLeft()
    {
        SpeakAction speak = gameObject.AddComponent<SpeakAction>();
        speak.id = "useTheOtherLeft";
        speak.skipAfter = true;
        runner.PlaySingleAction(speak);
    }
}