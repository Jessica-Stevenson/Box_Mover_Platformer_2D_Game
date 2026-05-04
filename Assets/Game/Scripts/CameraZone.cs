using UnityEngine;

public class CameraZone : MonoBehaviour
{
    public Transform cameraPoint;
    public bool lockCamera = true;

    private CameraController cam;

    private void Awake()
    {
        cam = Camera.main.GetComponent<CameraController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (lockCamera)
        {
            cam.SetFixedCamera(cameraPoint.position);
        }
        else
        {
            cam.FollowPlayer();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        cam.FollowPlayer();
    }

    private void OnDrawGizmos()
    {
        if (cameraPoint == null) return;

        Camera camMain = Camera.main;
        if (camMain == null) return;

        Gizmos.color = Color.yellow;

        float height = camMain.orthographicSize * 2f;
        float width = height * camMain.aspect;

        Vector3 center = new Vector3(cameraPoint.position.x, cameraPoint.position.y, 0);

        Gizmos.DrawWireCube(center, new Vector3(width, height, 0f));

        BoxCollider2D box = GetComponent<BoxCollider2D>();
        if (box != null)
        {
            Gizmos.color = new Color(0, 1, 1, 0.25f);
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawCube(box.offset, box.size);
        }
    }
}