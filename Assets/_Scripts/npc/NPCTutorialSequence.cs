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

        ActionSpeak speakPoint = gameObject.AddComponent<ActionSpeak>();
        speakPoint.id = "point";
        ActionPlayAnimation animPoint = gameObject.AddComponent<ActionPlayAnimation>();
        animPoint.id = "Pointing";
        ActionComposite speakAndPoint = gameObject.AddComponent<ActionComposite>();
        speakAndPoint.id = "composite speak and point";
        speakAndPoint.actions = new List<NPCAction>();
        speakAndPoint.actions.Add(speakPoint);
        speakAndPoint.actions.Add(animPoint);
        sequence.actions.Add(speakAndPoint);

        ActionMove move = gameObject.AddComponent<ActionMove>();
        move.id = "moveToPlayer";
        move.skipAfterAction = true;
        sequence.actions.Add(move);

        ActionSpeak speakGreet = gameObject.AddComponent<ActionSpeak>();
        speakGreet.id = "greet";
        ActionPlayAnimation animgreet = gameObject.AddComponent<ActionPlayAnimation>();
        animgreet.id = "Greet";
        ActionComposite speakAndGreet = gameObject.AddComponent<ActionComposite>();
        speakAndGreet.id = "composite speak and greet";
        speakAndGreet.actions = new List<NPCAction>();
        speakAndGreet.actions.Add(speakGreet);
        speakAndGreet.actions.Add(animgreet);
        speakAndGreet.skipAfterAction = true;
        sequence.actions.Add(speakAndGreet);

        ActionSpeak speakHand = gameObject.AddComponent<ActionSpeak>();
        speakHand.id = "useLeftHand";
        ActionTriggerAnimation triggerAnimation = gameObject.AddComponent<ActionTriggerAnimation>();
        triggerAnimation.id = "HandUp_Left";
        triggerAnimation.stopTrigger = "HandDown_Left";
        ActionContextMethod contextUseOtherLeft = gameObject.AddComponent<ActionContextMethod>();
        contextUseOtherLeft.id = "contextUseOtherLeft";
        contextUseOtherLeft.actionEvent = GameManager.Instance.handGesturesController.rightHandActive;
        contextUseOtherLeft.contextEvent.AddListener(UseTheOtherLeft);
        ActionContextMethod contextUseLeftMenu = gameObject.AddComponent<ActionContextMethod>();
        contextUseLeftMenu.id = "contextUseLeftMenu";
        contextUseLeftMenu.actionEvent = GameManager.Instance.handGesturesController.leftHandActive;
        contextUseLeftMenu.contextEvent.AddListener(UseLeftHandMenu);
        ActionComposite handAndTriggerAnimation = gameObject.AddComponent<ActionComposite>();
        handAndTriggerAnimation.id = "handAndTrigger";
        handAndTriggerAnimation.actions = new List<NPCAction>();
        handAndTriggerAnimation.actions.Add(speakHand);
        handAndTriggerAnimation.actions.Add(triggerAnimation);
        handAndTriggerAnimation.actions.Add(contextUseOtherLeft);
        handAndTriggerAnimation.actions.Add(contextUseLeftMenu);
        sequence.actions.Add(handAndTriggerAnimation);

        ActionSpeak cheersFirstMenu = gameObject.AddComponent<ActionSpeak>();
        cheersFirstMenu.id = "cheersFirstMenu";
        cheersFirstMenu.skipAfterAction = true;
        sequence.actions.Add(cheersFirstMenu);

        ActionSpeak pickMate = gameObject.AddComponent<ActionSpeak>();
        pickMate.id = "pickMate";
        sequence.actions.Add(pickMate);

    }

    [ContextMenu("UseTheOtherLeft")]
    public void UseTheOtherLeft()
    {
        ActionSpeak speak = gameObject.AddComponent<ActionSpeak>();
        speak.id = "useTheOtherLeft";
        speak.skipAfterAction = true;
        runner.PlaySingleAction(speak);
    }

    [ContextMenu("UseLeftHandMenu")]
    public void UseLeftHandMenu()
    {
        GameManager.Instance.handMenu.Show();
        GameManager.Instance.handGesturesController.leftHandActive.AddListener(() => GameManager.Instance.handMenu.Show());
        GameManager.Instance.handGesturesController.leftHandDeactive.AddListener(() => GameManager.Instance.handMenu.Hide());
        runner.Skip();
    }
}