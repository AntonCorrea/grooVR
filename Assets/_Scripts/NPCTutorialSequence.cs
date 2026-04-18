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

        //PlayAnimateAction clap = gameObject.AddComponent<PlayAnimateAction>();
        //clap.id = "Clapping";
        //clap.actionClipTime = 2f;
        //sequence.actions.Add(clap);

        //SpeakAction speak = gameObject.AddComponent<SpeakAction>();
        //speak.id = "pressToStart";
        //sequence.actions.Add(speak);

        MoveAction move = gameObject.AddComponent<MoveAction>();
        move.id = "moveToTable";
        sequence.actions.Add(move);

        MoveAction move1 = gameObject.AddComponent<MoveAction>();
        move1.id = "1";
        sequence.actions.Add(move1);

        MoveAction move2 = gameObject.AddComponent<MoveAction>();
        move2.id = "2";
        sequence.actions.Add(move2);

        MoveAction move3 = gameObject.AddComponent<MoveAction>();
        move3.id = "3";
        sequence.actions.Add(move3);

        //PlayAnimateAction animationGreet = gameObject.AddComponent<PlayAnimateAction>();
        //animationGreet.id = "Greet";
        //animationGreet.skipAfter = true;
        //sequence.actions.Add(animation);

        //SpeakAction speakGreet = gameObject.AddComponent<SpeakAction>();
        //speakGreet.id = "sayGreet";
        //sequence.actions.Add(speak);

        //CompositeNPCAction greet = gameObject.AddComponent<CompositeNPCAction>();
        //greet.id = "greet animation and speak";
        //greet.actions = new List<NPCAction>();
        //greet.actions.Add(animationGreet);
        //greet.actions.Add(speakGreet);
        //greet.skipAfter = true;
        //sequence.actions.Add(greet);

        //PlayAnimateAction point = gameObject.AddComponent<PlayAnimateAction>();
        //point.id = "Pointing";
        //point.loopTime = 5f;
        //sequence.actions.Add(point);

        //TriggerAnimateAction triggerAnimation = gameObject.AddComponent<TriggerAnimateAction>();
        //triggerAnimation.id = "HandUp_Left";
        //sequence.actions.Add(triggerAnimation);
    }

    [ContextMenu("UseTheOtherLeft")]
    public void UseTheOtherLeft()
    {
        SpeakAction speak = gameObject.AddComponent<SpeakAction>();
        speak.id = "useTheOtherLeft";
        speak.skipAfterAction = true;
        runner.PlaySingleAction(speak);
    }
}