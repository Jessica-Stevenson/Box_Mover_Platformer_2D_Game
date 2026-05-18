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
        //Get the components Rigidbody2D and Collider2D
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        //Set the Rigid body type to Kinematic
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    //When holding the block
    void OnMouseDown()
    {
        //If the player is standing on the block it does nothing
        if (playerOnTop) return;

        //If the player isn't then set is dragging to true and disables collisions
        isDragging = true;

        col.enabled = false;

        //Helps with positioning the block making it easier for thr player
        offset = transform.position - GetMouseWorldPos();
    }

    //When no longer holding the block
    void OnMouseUp()
    {
        //Sets isDragging to false and enables collisions again
        isDragging = false;

        col.enabled = true;
    }

    //Every Update
    void Update()
    {
        //If is dragging is set to true
        if (isDragging)
        {
            //The block will be dragged with the mouse.
            transform.position = GetMouseWorldPos() + offset;
        }
    }

    //Positioning
    Vector3 GetMouseWorldPos()
    {
        //makes is so when the player is dragging the block it does not snap to 
        //the middle of the the cursor but rather stays in it's position
        Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePoint.z = 0f;
        return mousePoint;
    }

    //Checks if the player is standing on the block
    private void OnTriggerEnter2D(Collider2D other)
    {
        //If so then sets playerOnTop to true
        if (other.CompareTag("Player"))
        {
            playerOnTop = true;
        }
    }

    //Checks if player left the block
    private void OnTriggerExit2D(Collider2D other)
    {
        //if so set playerOnTop to false
        if (other.CompareTag("Player"))
        {
            playerOnTop = false;
        }
    }
}