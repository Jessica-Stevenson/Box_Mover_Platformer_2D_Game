using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    GameController gameController;
    SpriteRenderer spriteRenderer;

    public Sprite passive, active;
    Collider2D coll;
    AudioManager audioManager;
    
    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<GameController>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioManager.PlaySFX(audioManager.checkpoint);
            gameController.UpdateCheckpoint(transform.position);
            spriteRenderer.sprite = active;
            coll.enabled = false;
        }
    }
}