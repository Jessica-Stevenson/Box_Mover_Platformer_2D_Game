using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform target;
    Vector3 velocity = Vector3.zero;

    [Range(0, 1)]
    public float smoothTime;

    public Vector3 positionOffset;

    [Header("Axis Limit")]
    public Vector2 xLimit;
    public Vector2 yLimit;

    private Vector3 currentTarget;
    private bool useFixedCamera = false;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 followTarget = target.position + positionOffset;
        followTarget = new Vector3(
            Mathf.Clamp(followTarget.x, xLimit.x, xLimit.y),
            Mathf.Clamp(followTarget.y, yLimit.x, yLimit.y),
            -10
        );

        Vector3 desiredTarget = useFixedCamera ? currentTarget : followTarget;

        Vector3 smoothedPosition = Vector3.SmoothDamp(
            transform.position,
            desiredTarget,
            ref velocity,
            smoothTime
        );

        smoothedPosition = new Vector3(
            Mathf.Round(smoothedPosition.x * 100f) / 100f,
            Mathf.Round(smoothedPosition.y * 100f) / 100f,
            -10
        );

        transform.position = smoothedPosition;
    }

    public void SetFixedCamera(Vector3 position)
    {
        useFixedCamera = true;
        currentTarget = new Vector3(position.x, position.y, -10);
    }

    public void FollowPlayer()
    {
        useFixedCamera = false;
    }
}