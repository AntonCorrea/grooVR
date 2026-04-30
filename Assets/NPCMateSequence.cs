using UnityEngine;
using UnityEngine.Events;

public class NPCMateSequence : NPCSequence
{
    bool isFirstTimeHandActive = true;

    public override void CreateSequence()
    {
        ActionSpeak speakHandDown = gameObject.AddComponent<ActionSpeak>();
        speakHandDown.id = "handDown";
        ActionContextMethod contextUseDownLeft = gameObject.AddComponent<ActionContextMethod>();
        contextUseDownLeft.id = "contextUseLeftMenu";
        contextUseDownLeft.actionEvent = GameManager.Instance.handGesturesController.leftHandDeactive;
        contextUseDownLeft.contextEvent.AddListener(runner.Skip);
        ActionComposite compositeHandDownLeft = gameObject.AddComponent<ActionComposite>();
        compositeHandDownLeft.id = "compositeHandDownLeft";
        compositeHandDownLeft.actions.Add(speakHandDown);
        compositeHandDownLeft.actions.Add(contextUseDownLeft);
        sequence.Add(compositeHandDownLeft);

        ActionSpeak speakCongrats = gameObject.AddComponent<ActionSpeak>();
        speakCongrats.id = "congratsMenuFinished";
        ActionPlayAnimation congratsAnimation = gameObject.AddComponent<ActionPlayAnimation>();
        congratsAnimation.id = "Clapping";
        congratsAnimation.actionClipTime = 4f;
        ActionComposite speakAndAnimCongrats = gameObject.AddComponent<ActionComposite>();
        speakAndAnimCongrats.id = "speakAndAnimCongrats";
        speakAndAnimCongrats.skipAfterComplete = true;
        speakAndAnimCongrats.actions.Add(speakCongrats);
        speakAndAnimCongrats.actions.Add(congratsAnimation);
        sequence.Add(speakAndAnimCongrats);

        ActionPlayAnimation idleAnimation = gameObject.AddComponent<ActionPlayAnimation>();
        idleAnimation.id = "Idle";
        idleAnimation.actionClipTime = 0.1f;
        idleAnimation.skipAfterComplete = true;
        sequence.Add(idleAnimation);

        ActionMove moveToTeleportShow_1 = gameObject.AddComponent<ActionMove>();
        moveToTeleportShow_1.id = "moveToShowTeleport_1";
        sequence.Add(moveToTeleportShow_1);

        ActionMove moveToTeleportShow_2 = gameObject.AddComponent<ActionMove>();
        moveToTeleportShow_2.id = "moveToShowTeleport_2";
        sequence.Add(moveToTeleportShow_2);

        ActionTriggerAnimation rightHandUpAnimation = gameObject.AddComponent<ActionTriggerAnimation>();
        rightHandUpAnimation.id = "HandUp_Right";
        rightHandUpAnimation.actionClipTime = .1f;
        rightHandUpAnimation.skipAfterComplete = true;
        sequence.Add(rightHandUpAnimation);

        ActionSpeak handTeleport_1 = gameObject.AddComponent<ActionSpeak>();
        handTeleport_1.id = "handTeleport_1";
        ActionSpeak handTeleport_2 = gameObject.AddComponent<ActionSpeak>();
        handTeleport_2.id = "handTeleport_2";
        ActionSpeak handTeleport_3 = gameObject.AddComponent<ActionSpeak>();
        handTeleport_3.id = "handTeleport_3";
        ActionComposite compositeSpeak = gameObject.AddComponent<ActionComposite>();
        compositeSpeak.id = "compositeSpeak";
        compositeSpeak.runSequentially = true;
        compositeSpeak.actions.Add(handTeleport_1);
        compositeSpeak.actions.Add(handTeleport_2);
        compositeSpeak.actions.Add(handTeleport_3);

        ActionContextMethod contextUseUpRight = gameObject.AddComponent<ActionContextMethod>();
        contextUseUpRight.id = "contextUseUpRight";
        contextUseUpRight.actionEvent = GameManager.Instance.handGesturesController.rightHandActive;
        contextUseUpRight.contextEvent.AddListener(RightHandUp);
        ActionContextMethod contextTeleport = gameObject.AddComponent<ActionContextMethod>();
        contextTeleport.id = "contextTeleport";
        contextTeleport.actionEvent = GameManager.Instance.teleporter.OnTeleport;
        contextTeleport.contextEvent.AddListener(runner.Skip);
        ActionComposite compositeTeleport = gameObject.AddComponent<ActionComposite>();
        compositeTeleport.id = "compositeTeleport";
        compositeTeleport.actions.Add(contextUseUpRight);
        compositeTeleport.actions.Add(contextTeleport);

        ActionComposite compositeSpeakAndTeleport = gameObject.AddComponent<ActionComposite>();
        compositeSpeakAndTeleport.id = "compositeSpeakAndTeleport";
        compositeSpeakAndTeleport.actions.Add(compositeSpeak);
        compositeSpeakAndTeleport.actions.Add(compositeTeleport);
        sequence.Add(compositeSpeakAndTeleport);

        ActionTriggerAnimation rightHandDownAnimation = gameObject.AddComponent<ActionTriggerAnimation>();
        rightHandDownAnimation.id = "HandDown_Right";
        rightHandDownAnimation.actionClipTime = .1f;
        rightHandDownAnimation.skipAfterComplete = true;
        sequence.Add(rightHandDownAnimation);

        ActionMove moveToTable_1 = gameObject.AddComponent<ActionMove>();
        moveToTable_1.id = "moveToTable_1";
        sequence.Add(moveToTable_1);

        ActionMove moveToTable_2 = gameObject.AddComponent<ActionMove>();
        moveToTable_2.id = "moveToTable_2";
        sequence.Add(moveToTable_2);

        ActionMove moveToTable_3 = gameObject.AddComponent<ActionMove>();
        moveToTable_3.id = "moveToTable_3";
        sequence.Add(moveToTable_3);

        ActionSpeak congratsTutorialFinished = gameObject.AddComponent<ActionSpeak>();
        congratsTutorialFinished.id = "congratsTutorialFinished";
        //ActionPlayAnimation congratsTutorialFinished = gameObject.AddComponent<ActionPlayAnimation>();
        //congratsAnimation.id = "Clapping";
        //congratsAnimation.actionClipTime = 4f;
        ActionComposite speakAndAnimCongratsTutorial = gameObject.AddComponent<ActionComposite>();
        speakAndAnimCongratsTutorial.id = "speakAndAnimCongrats";
        speakAndAnimCongratsTutorial.skipAfterComplete = true;
        speakAndAnimCongratsTutorial.actions.Add(congratsTutorialFinished);
        speakAndAnimCongratsTutorial.actions.Add(congratsAnimation);
        sequence.Add(speakAndAnimCongrats);

        //ActionPlayAnimation idleAnimation = gameObject.AddComponent<ActionPlayAnimation>();
        //idleAnimation.id = "Idle";
        //idleAnimation.actionClipTime = 0.1f;
        //idleAnimation.skipAfterComplete = true;
        sequence.Add(idleAnimation);

        ActionCallMethod callEndTutorial = gameObject.AddComponent<ActionCallMethod>();
        callEndTutorial.id = "endTutorial";
        callEndTutorial.onCall.AddListener(GameManager.Instance.SkipTutorial);
        sequence.Add(callEndTutorial);
    }

    void RightHandUp()
    {
        if(isFirstTimeHandActive)
        {
            isFirstTimeHandActive = false;
            GameManager.Instance.teleporter.StartTeleport();
            GameManager.Instance.handGesturesController.rightHandActive.AddListener(() => GameManager.Instance.teleporter.StartTeleport());
            GameManager.Instance.handGesturesController.rightHandDeactive.AddListener(() => GameManager.Instance.teleporter.CancelTeleport());
        }
    }


}
