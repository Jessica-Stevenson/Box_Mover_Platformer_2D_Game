using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector2 CheckpointPos;
    SpriteRenderer spriteRenderer;
    public LavaRising lava;
    AudioManager audioManager;

    private void Awake()
    {
        //Get the Sprite Renderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        //Get the Audio Manager
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        //Set the starting checkpoint. 
        //When the player starts their checkpoint will be set to their starting pos
        CheckpointPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the player touches something tagged with "Deathzone"
        if (collision.CompareTag("DeathZone"))
        {
            //They die and a sound is played
            audioManager.PlaySFX(audioManager.death);
            Die();
            //If there is rising lava in the level reset the lava to it's starting pos
            if (lava != null)
            {
                lava.ResetLava();
            }
            else
            {
                Debug.Log("No lava in this level (this is fine)");
            }
        }
    }

    public void UpdateCheckpoint(Vector2 pos)
    {
        //When a checkpoint is touches update the checkpoint
        CheckpointPos = pos;
    }

    void Die()
    {
        //Player won't respawn immeditlly.
        StartCoroutine(Respawn(0.5f));
    }

    IEnumerator Respawn(float duration)
    {
        //Disables the sprite while respawning
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(duration);

        //Moves the player to the last checkpoint 
        transform.position = CheckpointPos;
        //Displays the sprite again
        spriteRenderer.enabled = true;
    }
}