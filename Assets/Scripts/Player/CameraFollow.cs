using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera Smooth Settings")]
    [SerializeField] private float smoothTime;
    private Vector3 velocity = Vector3.zero;

    [Header("Target Reference")]
    [SerializeField] private Transform target;

    [Header("Camera Offset Settings")]
    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;
    private float yOffsetOriginal;
    private Vector3 offset;

    [Header("Player References")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private InitialPlayerData playerData;

    private void Awake()
    {
        yOffsetOriginal = yOffset;
    }

    private void Update()
    {
        offset = new Vector3(xOffset, yOffset, -10f);
        Vector3 targetPosition = target.position + offset;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        if (Input.GetKey(playerData.jumpKey))
        {
            yOffset = 2;
        }
        else if (playerController.inAir)
        {
            yOffset = -1;
        }
        else if (Input.GetKey(playerData.downKey))
        {
            yOffset = -2;
        }
        else
        {
            yOffset = yOffsetOriginal;
        }
    }
}
