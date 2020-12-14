using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class TVController : MonoBehaviour
{
    private float timer;
    //private bool maxSpawnReached;
    private bool carLockOut = false;
    private bool aiLockOut = false;
    private float lastSpawn;
    private int spawnCounter;

    private VideoPlayer videoPlayer;
    private GameObject spawnItem;

    public VideoClip staticVideo;
    public VideoClip video;
    public GameObject prefabSpawn;
    public bool spawning;
    public int maxItems;
    public float spawnWaitTime;
    public Vector3 vector;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        lastSpawn = 0.0f;
        spawning = false;
        //maxSpawnReached = false;
        spawnCounter = 0;

        videoPlayer = GetComponentInChildren<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;

        //maxSpawnReached = spawnCounter > maxItems ? true : false;

        //if (spawning && !maxSpawnReached)
        if (spawning)
        {
            SpawnItem();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car" && !carLockOut)
        {
            StartSpawning();
            carLockOut = true;
        }
        else if (other.tag == "LargeAI" && !aiLockOut)
        {
            StopSpawning();
            aiLockOut = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Car")
        {
            carLockOut = false;
        }
        else if (other.tag == "LargeAI")
        {
            aiLockOut = false;
        }
    }

    void StartSpawning()
    {
        videoPlayer.clip = video;
        videoPlayer.Play();
        spawning = true;
    }

    void StopSpawning()
    {
        videoPlayer.clip = staticVideo;
        videoPlayer.Play();
        spawning = false;
    }

    private void SpawnItem()
    {
        if (!spawning || (timer - lastSpawn < spawnWaitTime))
        {
            return;
        }

        if (spawnCounter > maxItems)
        {
            //Debug.Log("Max items Reached");
            //maxSpawnReached = false;
            return;
        }

        spawnCounter++;
        lastSpawn = timer;

        spawnItem = Instantiate(prefabSpawn, prefabSpawn.transform.position, prefabSpawn.transform.rotation);

        spawnItem.SetActive(true);
        spawnItem.GetComponent<Rigidbody>().AddForce(vector, ForceMode.Impulse);
    }
}