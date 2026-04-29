using UnityEngine;
using UnityEngine.Events;

public class HandGesturesController : MonoBehaviour
{
    public UnityEvent leftHandActive, leftHandDeactive, rightHandActive, rightHandDeactive;

    [ContextMenu("LeftHand ON")]
    public void LeftHandON()
    {
        leftHandActive.Invoke();
    }

    [ContextMenu("LeftHand OFF")]
    public void LeftHandOFF()
    {
        leftHandDeactive.Invoke();
    }

    [ContextMenu("RightHand ON")]
    public void RightHandON()
    {
        rightHandActive.Invoke();
    }

    [ContextMenu("RightHand OFF")]
    public void RightHandOFF()
    {
        rightHandDeactive.Invoke();
    }
}
