using UnityEngine;

public class CarSeatController : MonoBehaviour
{
    [Header("References")]
    public Transform playerCam;        // The actual VR headset camera (from XR Rig)
    public Transform seatPoint;       // The point inside the car where the head should be


    private Vector3 initialHeadOffset;

    void Start()
    {
        if (playerCam == null)
            //playerCam = Camera.main.transform; // fallback
            playerCam = GameManager.Instance.vehicleController.mainPlayerCam.transform;

        // Store headset’s initial offset in local space (for correct seated positioning)
        initialHeadOffset = playerCam.localPosition;

    }

    void LateUpdate()
    {
        // Get the headset’s local movement relative to its XR rig
        Vector3 headsetLocalOffset = playerCam.localPosition - initialHeadOffset;
        Quaternion headsetLocalRotation = playerCam.localRotation;

        // Apply it to the car camera, relative to the seat
        transform.position = seatPoint.TransformPoint(headsetLocalOffset);
        transform.rotation = seatPoint.rotation * headsetLocalRotation;
    }
}
