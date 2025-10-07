using UnityEngine;

public class FlexibleFollower : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform followTarget;      // Object to follow (position)
    public Vector3 positionOffset = Vector3.zero; // Offset from target
    public bool smoothFollow = true;    // Whether to move smoothly
    public float followSpeed = 5f;      // Speed of smooth following

    [Header("Look Settings")]
    public bool lookAtTarget = false;   // Whether to look at a target
    public Transform lookTarget;        // Object to look at
    public Vector3 lookOffset = Vector3.zero; // Offset for look direction

    [Header("Update Settings")]
    public bool useFixedUpdate = false; // Option for physics-based follow

    void Update()
    {
        if (!useFixedUpdate)
            FollowBehaviour();
    }

    void FixedUpdate()
    {
        if (useFixedUpdate)
            FollowBehaviour();
    }

    private void FollowBehaviour()
    {
        if (followTarget)
        {
            Vector3 desiredPosition = followTarget.position + positionOffset;

            if (smoothFollow)
            {
                transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = desiredPosition;
            }
        }

        if (lookAtTarget)
        {
            Vector3 lookPosition = lookTarget.position + lookOffset;
            Vector3 direction = lookPosition - transform.position;

            if (direction.sqrMagnitude > 0.0001f)
                transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    // Optional: Draw gizmos to visualize follow/look targets
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        if (followTarget)
            Gizmos.DrawLine(transform.position, followTarget.position + positionOffset);

        Gizmos.color = Color.yellow;
        if (lookAtTarget)
            Gizmos.DrawLine(transform.position, lookTarget.position + lookOffset);
    }
}
