using UnityEngine;
using UnityEngine.Events;

public class HandGesturesController : MonoBehaviour
{
    public UnityEvent leftHandActive, leftHandDeactive, rightHandActive, rightHandDeactive;
    public void LeftHandON()
    {
        leftHandActive.Invoke();
    }

    public void LeftHandOFF()
    {
        leftHandDeactive.Invoke();
    }

    public void RightHandON()
    {
        rightHandActive.Invoke();
    }

    public void RightHandOFF()
    {
        rightHandDeactive.Invoke();
    }
}
