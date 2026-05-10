using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FinishPoint : MonoBehaviour
{
    [SerializeField] bool goNextLevel;
    [SerializeField] string levelName;

    [SerializeField] GameObject levelCompleteUI;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        levelCompleteUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioManager.PlaySFX(audioManager.sucess);

            levelCompleteUI.SetActive(true);
            StartCoroutine(LoadAfterDelay());
        }
    }

    IEnumerator LoadAfterDelay()
    {
        yield return new WaitForSeconds(3f);

        if (SceneController.instance == null)
        {
            Debug.LogError("SceneController instance is NULL!");
            yield break;
        }

        if (goNextLevel)
        {
            SceneController.instance.NextLevel();
        }
        else
        {
            SceneController.instance.LoadScene(levelName);
        }
    }
}