using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLauncher : MonoBehaviour
{
    private float timer;
    private float bounceTimer;
    private float carTouchTimer;
    private float lastLaunch;
    private float lastGroundTouch;
    private bool outsideTV;
    private bool grounded;

    public Rigidbody carRigidBody;
    public float maxLaunchHeight;
    public float lockOutTime;
    public float helpTime;
    public float minTorqueX;
    public float maxTorqueX;
    public float minTorqueY;
    public float maxTorqueY;
    public float minTorqueZ;
    public float maxTorqueZ;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        bounceTimer = 0.0f;
        carTouchTimer = 0.0f;
        lastLaunch = 0.0f;
        lastGroundTouch = 0.0f;
        outsideTV = true;
        grounded = false;

        carRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;

        if (timer - lastGroundTouch > helpTime)
        {
            HelpCarOut();
        }

        if (timer - carTouchTimer > helpTime)
        {
            HelpCarOut();
        }

        //Debug.Log("DIFF TIMER: " + (timer - lastGroundTouch));
    }

    private void OnTriggerEnter(Collider other)
    {
        outsideTV = false;

        //if (other.tag == "TV" && LaunchAvailable())
        if (other.tag == "TV")
        {
            Launch();
            AddRotation();
            lastLaunch = timer;
        }
        else if (other.tag == "Car")
        {
            PushBack();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        lastGroundTouch = timer;

        if(other.tag == "Car")
        {
            carTouchTimer = carTouchTimer + Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "TV")
        {
            outsideTV = true;
        }

        if(other.tag == "Ground")
        {
            lastGroundTouch = timer;
        }

        if (other.tag == "Car")
        {
            carTouchTimer = timer;
        }
    }

    void Launch()
    {
        float launchHeight = Random.Range(0, maxLaunchHeight);

        carRigidBody.velocity = new Vector3(carRigidBody.velocity.x, launchHeight, -carRigidBody.velocity.z);
    }

    void PushBack()
    {
        float push = Random.Range(-5, -1);

        carRigidBody.velocity = new Vector3(carRigidBody.velocity.x, carRigidBody.velocity.y, -carRigidBody.velocity.z);
    }

    void AddRotation()
    {
        float xTorque = Random.Range(minTorqueX, maxTorqueX);
        float yTorque = Random.Range(minTorqueY, maxTorqueY);
        float zTorque = Random.Range(minTorqueZ, maxTorqueZ);

        //carRigidBody.AddTorque(new Vector3(xTorque, 1, zTorque), ForceMode.Impulse);
        carRigidBody.AddRelativeTorque(new Vector3(xTorque, yTorque, zTorque), ForceMode.Impulse);
    }

    private bool LaunchAvailable()
    {
        if ((timer - lastLaunch) > lockOutTime && outsideTV)
        {
            return true;
        }

        return false;
    }

    private void HelpCarOut()
    {
        lastGroundTouch = timer;    // Override last ground touch
        carTouchTimer = timer;    // Override last ground touch

        carRigidBody.AddRelativeForce(new Vector3(26000f, 0, 0), ForceMode.Impulse);
        Debug.Log("HELPING!");
    }
}
