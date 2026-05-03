using UnityEngine;

public class DraggableBlock : MonoBehaviour
{
    public ItemData itemData;
    
    private bool isDragging;
    private Vector3 offset;
    private Rigidbody2D rb;

    private bool placed = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;
    }

    void OnMouseDown()
    {
        isDragging = true;

        rb.bodyType = RigidbodyType2D.Kinematic;

        offset = transform.position - GetMouseWorldPos();
    }

    void OnMouseUp()
    {
        isDragging = false;

        rb.bodyType = RigidbodyType2D.Kinematic;

        placed = true;
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

}