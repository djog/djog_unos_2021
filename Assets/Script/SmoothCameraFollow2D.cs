using UnityEngine;

public class SmoothCameraFollow2D : MonoBehaviour
{
    public Transform target;
    public enum CameraFollowUpdateMode { Update, FixedUpdate, LateUpdate }
    public CameraFollowUpdateMode updateMode = CameraFollowUpdateMode.FixedUpdate;
    public float smoothSpeed = 0.1f;
    public float zPosition = -100;
    public bool followX = true;
    public bool followY = true;
    public Vector2 offset;

    [Header("Size with Velocity (optional)")]
    public bool sizeWithVelocity = false;
    public float defaultSize = 5.0f;
    public Vector2 velocityMultiplier = Vector2.one;
    public new Camera camera;
    public new Rigidbody2D rigidbody;

    private void Start()
    {
        transform.position = target.position;
    }

    void FixedUpdate()
    {
        if (updateMode == CameraFollowUpdateMode.FixedUpdate)
            Follow();
    }

    private void Update()
    {
        if (updateMode == CameraFollowUpdateMode.Update)
            Follow();
    }

    private void LateUpdate()
    {
        if (updateMode == CameraFollowUpdateMode.LateUpdate)
            Follow();
    }

    void Follow()
    {
        if (target)
        {
            Vector2 desiredPosition = new Vector2(followX ? target.position.x : 0, followY ? target.position.y : 0) + offset;
            Vector2 smoothedPosition = Vector2.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, zPosition);

            if (sizeWithVelocity)
            {
                float velocity = (rigidbody.velocity * velocityMultiplier).magnitude;
                float targetSize = defaultSize + velocity;
                float smoothSize = Mathf.Lerp(camera.orthographicSize, targetSize, Time.deltaTime);
                camera.orthographicSize = smoothSize;
            }
        }
    }
}