using UnityEngine;

public class LavaRising : MonoBehaviour
{
    public float riseSpeed = 1f;
    public float acceleration = 0.1f;

    private Vector3 startPos;

    void Start()
    {
        //Makes the starting position the current position
        //This is so the lava can reset when the player dies
        startPos = transform.position;
    }

    void Update()
    {
        //This is what makes the lava rise. It will also accerlate the longer the player stays in the level
        transform.position += Vector3.up * riseSpeed * Time.deltaTime;
        riseSpeed += acceleration * Time.deltaTime;
    }

    //This is called when the player dies
    public void ResetLava()
    {
        //Resets the position and riseSpeed of the lava
        transform.position = startPos;
        riseSpeed = 1f;
    }
}