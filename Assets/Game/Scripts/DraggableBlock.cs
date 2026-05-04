using UnityEngine;

public class DraggableBlock : MonoBehaviour
{
    public ItemData itemData;

    private bool isDragging;
    private Vector3 offset;
    private Rigidbody2D rb;
    private bool playerOnTop = false;
    private Collider2D col;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void OnMouseDown()
    {
        if (playerOnTop) return;

        isDragging = true;

        col.enabled = false;

        offset = transform.position - GetMouseWorldPos();
    }

    void OnMouseUp()
    {
        isDragging = false;

        col.enabled = true;
    }

    void Update()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPos() + offset;
        }
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePoint.z = 0f;
        return mousePoint;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnTop = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnTop = false;
        }
    }
}