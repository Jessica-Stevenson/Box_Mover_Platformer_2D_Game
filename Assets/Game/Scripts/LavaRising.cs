using UnityEngine;

public class LavaRising : MonoBehaviour
{
    public float riseSpeed = 1f;
    public float acceleration = 0.01f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.position += Vector3.up * riseSpeed * Time.deltaTime;
        riseSpeed += acceleration * Time.deltaTime;
    }

    public void ResetLava()
    {
        transform.position = startPos;
        riseSpeed = 1f;
    }
}