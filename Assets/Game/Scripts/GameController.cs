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
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        CheckpointPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeathZone"))
        {
            audioManager.PlaySFX(audioManager.death);
            Die();
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
        CheckpointPos = pos;
    }

    void Die()
    {
        StartCoroutine(Respawn(0.5f));
    }

    IEnumerator Respawn(float duration)
    {
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(duration);

        transform.position = CheckpointPos;
        spriteRenderer.enabled = true;
    }
}