using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    Vector3 spawnPosition;
    private float timer;
    private float lastChangeTimer;
    private float changeTimer;
    private float minZ = 75.0f;
    private float maxZ = 10.0f;
    private float minX = -80.0f;
    private float maxX = -8.0f;
    private const float y = -5.0f;

    public float minTimer = 5.0f;
    public float maxTimer = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = transform.position;
        timer = 0.0f;

        RandomMove();
        changeTimer = (float)Random.Range(minTimer, maxTimer);
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;

        if (timer - lastChangeTimer > changeTimer)
        {
            RandomMove();
        }
    }

    void RandomMove()
    {
        lastChangeTimer = timer;

        Vector3 newPosition = spawnPosition;

        float newX = (float)Random.Range(minX, maxX);
        float newZ = (float)Random.Range(minZ, maxZ);

        newPosition.x = newX;
        newPosition.z = newZ;
        newPosition.y = y;

        transform.position = newPosition;
    }
}
