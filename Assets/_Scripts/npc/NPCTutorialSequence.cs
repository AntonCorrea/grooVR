using System.Collections.Generic;
using UnityEngine;

public class NPCTutorialSequence : NPCSequence
{
    public SliderAction slider; 
    private void Start()
    {
        slider.action.AddListener(runner.Skip);
    }

    public override void CreateSequence()
    {

        ActionSpeak speakPoint = gameObject.AddComponent<ActionSpeak>();
        speakPoint.id = "point";
        ActionPlayAnimation animPoint = gameObject.AddComponent<ActionPlayAnimation>();
        animPoint.id = "Pointing";
        ActionComposite speakAndPoint = gameObject.AddComponent<ActionComposite>();
        speakAndPoint.id = "composite speak and point";
        speakAndPoint.actions = new List<NPCAction>();
        speakAndPoint.actions.Add(speakPoint);
        speakAndPoint.actions.Add(animPoint);
        sequence.Add(speakAndPoint);

        ActionMove move = gameObject.AddComponent<ActionMove>();
        move.id = "moveToPlayer";
        sequence.Add(move);

        ActionSpeak speakGreet = gameObject.AddComponent<ActionSpeak>();
        speakGreet.id = "greet";
        ActionPlayAnimation animgreet = gameObject.AddComponent<ActionPlayAnimation>();
        animgreet.id = "Greet";
        ActionComposite speakAndGreet = gameObject.AddComponent<ActionComposite>();
        speakAndGreet.id = "composite speak and greet";
        speakAndGreet.skipAfterComplete = true;
        speakAndGreet.actions = new List<NPCAction>();
        speakAndGreet.actions.Add(speakGreet);
        speakAndGreet.actions.Add(animgreet);
        sequence.Add(speakAndGreet);

        ActionSpeak speakHand = gameObject.AddComponent<ActionSpeak>();
        speakHand.id = "useLeftHand";
        ActionTriggerAnimation triggerAnimation = gameObject.AddComponent<ActionTriggerAnimation>();
        triggerAnimation.id = "HandUp_Left";
        triggerAnimation.stopTrigger = "HandDown_Left";
        ActionSpeak speakUseOtherLeft = gameObject.AddComponent<ActionSpeak>();
        speakUseOtherLeft.id = "useTheOtherLeft";
        //ActionContextMethod contextUseOtherLeft = gameObject.AddComponent<ActionContextMethod>();
        //contextUseOtherLeft.id = "contextUseOtherLeft";
        //contextUseOtherLeft.actionEvent = GameManager.Instance.handGesturesController.rightHandActive;
        //contextUseOtherLeft.contextEvent.AddListener(() => UseTheOtherLeft(speakUseOtherLeft));
        ActionContextMethod contextUseLeftMenu = gameObject.AddComponent<ActionContextMethod>();
        contextUseLeftMenu.id = "contextUseLeftMenu";
        contextUseLeftMenu.actionEvent = GameManager.Instance.handGesturesController.leftHandActive;
        contextUseLeftMenu.contextEvent.AddListener(UseLeftHandMenu);
        ActionComposite handAndTriggerAnimation = gameObject.AddComponent<ActionComposite>();
        handAndTriggerAnimation.id = "handAndTrigger";
        handAndTriggerAnimation.actions.Add(speakHand);
        handAndTriggerAnimation.actions.Add(triggerAnimation);
        //handAndTriggerAnimation.actions.Add(contextUseOtherLeft);
        handAndTriggerAnimation.actions.Add(contextUseLeftMenu);
        sequence.Add(handAndTriggerAnimation);

        ActionSpeak cheersFirstMenu = gameObject.AddComponent<ActionSpeak>();
        cheersFirstMenu.id = "cheersFirstMenu";
        sequence.Add(cheersFirstMenu);

        ActionSpeak pickMate = gameObject.AddComponent<ActionSpeak>();
        pickMate.id = "pickMate";
        sequence.Add(pickMate);

    }

    [ContextMenu("UseTheOtherLeft")]
    public void UseTheOtherLeft(NPCAction action)
    {      
        runner.PlaySingleAction(action);
    }

    [ContextMenu("UseLeftHandMenu")]
    public void UseLeftHandMenu()
    {
        GameManager.Instance.handMenu.Show();
        GameManager.Instance.handGesturesController.leftHandActive.AddListener(() => GameManager.Instance.handMenu.Show());
        GameManager.Instance.handGesturesController.leftHandDeactive.AddListener(() => GameManager.Instance.handMenu.Hide());
        runner.Skip();
    }

    public void ButtonAction()
    {
        GameManager.Instance.SkipTutorial();
    }
}