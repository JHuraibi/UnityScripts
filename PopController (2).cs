using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PopController : MonoBehaviour
{
    private float timer;
    private float lastPop;
    private int popperCounter;
    private int enemyCounter;
    private GameObject popper;
    private GameObject enemy;

    private bool popping;

    public PopController otherPopController;
    public GameObject prefabPopper1;
    public GameObject prefabPopper2;
    public GameObject prefabEnemy;
    public int maxPoppers;
    public int maxEnemies;
    public float minPopHeight;
    public float maxPopHeight;
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
    public float minLeft;
    public float maxLeft;
    public float minRight;
    public float maxRight;
    public float minTorque;
    public float maxTorque;
    public float turn;

    void Start()
    {
        timer = 0f;
        popperCounter = 0;
        enemyCounter = 0;
        popping = false;
    }

    void Update()
    {
        timer = timer + Time.deltaTime;
        Pop();
        PopEnemy();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PopperMan" || other.tag == "Player")
        {
            popping = true;
            lastPop = timer;

            if (otherPopController.popping == false)
            {
                otherPopController.popping = true;
            }
        }
    }

    private void Pop()
    {
        if (!popping || (timer - lastPop < 1))
        {
            return;
        }

        if (popperCounter > maxPoppers)
        {
            Debug.Log("Max Poppers Reached");
            return;
        }

        lastPop = timer;

        Vector3 spawnPosition = transform.position;
        float randZ = (float)Random.Range(minX, maxX);
        float randX = (float)Random.Range(minZ, maxZ);
        float randHeight = (float)Random.Range(minPopHeight, maxPopHeight);
        float randRight = (float)Random.Range(minRight, maxRight);
        float randLeft = (float)Random.Range(minLeft, maxLeft);
        float torque = Random.Range(minTorque, maxTorque);
        int whichPopper = (int)Random.Range(0, 2);

        spawnPosition.x += randX;
        spawnPosition.z += randZ;

        if (whichPopper > 0)
        {
            //Debug.Log("BOUNCY");
            popper = Instantiate(prefabPopper1, spawnPosition, prefabPopper1.transform.rotation);
        }
        else
        {
            //Debug.Log("NOT BOUNCY");
            popper = Instantiate(prefabPopper2, spawnPosition, prefabPopper2.transform.rotation);
        }
        popper.SetActive(true);

        popper.GetComponent<Rigidbody>().AddForce(new Vector3(randLeft, randHeight, randRight), ForceMode.Impulse);
        popper.GetComponent<Rigidbody>().AddTorque(transform.right * torque * turn, ForceMode.Impulse);
        popper.GetComponent<TimedColorChange>().SetTimer();
    }


    private void PopEnemy()
    {
        // Using the pop timer from the popped kernels
        if (!popping || (timer - lastPop < 1))
        {
            return;
        }

        if (enemyCounter > maxEnemies)
        {
            Debug.Log("Max enemies Reached");
            return;
        }

        int spawnEnemy = (int)Random.Range(0, 1);

        if (spawnEnemy > 0)
        {
            return;
        }

        Vector3 spawnPosition = transform.position;
        float randZ = (float)Random.Range(minX, maxX);
        float randX = (float)Random.Range(minZ, maxZ);
        float randHeight = (float)Random.Range(minPopHeight, maxPopHeight);
        float randRight = (float)Random.Range(minRight, maxRight);
        float randLeft = (float)Random.Range(minLeft, maxLeft);
        float torque = Random.Range(minTorque, maxTorque);

        spawnPosition.x += randX;
        spawnPosition.z += randZ;

        enemy = Instantiate(prefabEnemy, spawnPosition, prefabEnemy.transform.rotation);
        enemy.SetActive(true);

        enemy.GetComponent<Rigidbody>().AddForce(new Vector3(randLeft, randHeight, randRight), ForceMode.Impulse);
        enemy.GetComponent<Rigidbody>().AddTorque(transform.right * torque * turn, ForceMode.Impulse);
        enemy.GetComponent<TimedColorChange>().SetTimer();
    }

}
