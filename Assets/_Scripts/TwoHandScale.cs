using UnityEngine;
using Autohand;

public class TwoHandScale : MonoBehaviour
{
    public Grabbable grabbable;
    public Transform guizmo;
    public Transform model;
    public float scaleFactor;

    private Hand hand1;
    private Hand hand2;

    private bool wasScaling;
    private float initialDistance;
    private Vector3 initialModelScale;
    private Vector3 initialGuizmoScale;

    void Update()
    {
        if (grabbable.HeldCount() == 2)
        {
            if (hand1 == null || hand2 == null)
            {
                hand1 = grabbable.GetHeldBy()[0];
                hand2 = grabbable.GetHeldBy()[1];

                initialDistance = Vector3.Distance(hand1.follow.position, hand2.follow.position);
                initialModelScale = model.localScale;
                initialGuizmoScale = guizmo.localScale;
                wasScaling = true;
            }

            float currentDistance = Vector3.Distance(hand1.follow.position, hand2.follow.position);
            float scaleProportion = currentDistance / initialDistance;
            //model.localScale = Vector3.Lerp(transform.localScale, initialScale * scaleProportion * scaleFactor, Time.deltaTime * 10f);

            model.localScale = initialModelScale * scaleProportion * scaleFactor;
            guizmo.localScale = initialGuizmoScale * scaleProportion * scaleFactor;
        }
        else
        {
            if (wasScaling)
            {
                wasScaling = false;
                guizmo.localScale = initialGuizmoScale;
            }
            hand1 = null;
            hand2 = null;
        }
    }
}