using UnityEngine;

public class UIFollowPlayer : MonoBehaviour
{
    public Transform playerHand;
    public Transform playerHead;
    public float distanceFromPlayer = 2f;
    public Vector3 offset = Vector3.zero;
    public bool lockHeight = false;

    void LateUpdate()
    {
        if (playerHand)
        {
            Vector3 targetPos = playerHand.position + playerHand.forward * distanceFromPlayer + offset;

            if (lockHeight)
                targetPos.y = transform.position.y; // Keep current Y height

            transform.position = targetPos;
        }

        if (playerHead)
        {
            Vector3 lookDirection = transform.position - playerHead.position;

            if (lockHeight)
                lookDirection.y = 0f; // Prevent tilting up/down

            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }
}
