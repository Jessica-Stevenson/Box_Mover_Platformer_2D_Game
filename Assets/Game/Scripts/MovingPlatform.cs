using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    //posA and posB are empty objects in the game
    public Transform posA, posB;

    public float speed;
    Vector3 targetPos;
    
    public void Start()
    {
        //Sets the target position to posB
        targetPos = posB.position;
    }

    private void Update()
    {
        //If the moving platform reached posA then set the target Postion to posB
        if (Vector2.Distance (transform.position, posA.position) < 0.05f)
        {
            targetPos = posB.position;
        }
        //If the moving platform reached posB then set the target position to posA
        if (Vector2.Distance (transform.position, posB.position) < 0.05f)
        {
            targetPos = posA.position;
        }   

        //Makes the platform move
        transform.position = Vector3.MoveTowards (transform.position, targetPos, speed * Time.deltaTime);

    }

    //Runs when something enters the colldier check
    private void OnTriggerEnter2D (Collider2D collision)
    {
        //If it's a player then make the player a child of the moving platform
        //This is to make the player move with the platform
        if (collision.CompareTag("Player"))
        {
            collision.transform.parent= this.transform;
        }
    }

    //Runs when something exits the colldier check
    private void OnTriggerExit2D(Collider2D collision)
    {
        //if it's the player then undo all that was done in the entry check
        if (collision.CompareTag("Player"))
        {
            collision.transform.parent = null;   
        }
    }
}
