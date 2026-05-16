using UnityEngine;

public class FlexibleFollower : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target;                  // The object to follow
    public bool followPosition = true;        // Should follow position?
    public bool followRotation = false;       // Should follow rotation?
    public bool lockXRotation = false;
    public bool lockYRotation = false;
    public bool lockZRotation = false;
    [Header("Offset & Pivot")]
    public Vector3 positionOffset = Vector3.zero;  // Local offset from the target
    //public Vector3 pivotOffset = Vector3.zero;     // Offset around which to pivot
    public Vector3 rotationOffset = Vector3.zero;

    [Header("Smoothing")]
    public float positionSmoothSpeed = 10f;   // Higher = snappier
    public float rotationSmoothSpeed = 10f;   // Higher = snappier

    void LateUpdate()
    {
        if (target == null)
            return;

        // Compute desired position with pivot and offset
        Vector3 desiredPosition = target.TransformPoint(positionOffset);

        if (followPosition)
        {
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * positionSmoothSpeed);
        }

        if (followRotation)
        {
            Quaternion desiredRotation = target.rotation * Quaternion.Euler(rotationOffset);
            Quaternion smoothedRotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSmoothSpeed);

            Vector3 euler = smoothedRotation.eulerAngles;
            if (lockXRotation)
                euler.x = 0f;
            if (lockYRotation)
                euler.y = 0f;
            if (lockZRotation)
                euler.z = 0f;

            transform.rotation = Quaternion.Euler(euler);
        }
    }

    //// Optional: visualize pivot in Scene view
    //void OnDrawGizmosSelected()
    //{
    //    if (target != null)
    //    {
    //        Gizmos.color = Color.cyan;
    //        Gizmos.DrawWireSphere(target.TransformPoint(pivotOffset), 0.1f);
    //    }
    //}

    public void SetInstantPosition()
    {
        transform.position = target.transform.position;
        transform.rotation = target.rotation * Quaternion.Euler(rotationOffset);
    }
}
