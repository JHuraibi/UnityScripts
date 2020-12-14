using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CollectObjectsRotate : MonoBehaviour
{
    private GameObject objectToRotate;
    private bool rotateObjectValid = true;
    private Vector3 rotationVector;
    private Vector3 startRotationVector;
    private bool rotateDone = true;
    private float angleSum;
    private char axis;
    private bool reverse;

    //public AudioClip pickupSound;
    public string nextLevelName;
    public string objectTagToRotate;
    public float rotationSpeed;
    public float rotationAmount;
    public string rotationAxis;

    private void Awake()
    {
        angleSum = 0f;

        if (objectTagToRotate == "")
            Debug.Log("No Tag Given To Rotate");
        else
            objectToRotate = GameObject.FindGameObjectWithTag(objectTagToRotate);
    }

    void Start()
    {
        if (objectToRotate == null)
        {
            Debug.Log("Container Not Found");
            rotateObjectValid = false;
        }

        this.InitializeRotationDirection();
    }

    void InitializeRotationDirection()
    {
        rotationAxis = rotationAxis.ToLower();

        switch (rotationAxis[0])
        {
            default:
                Debug.Log("Invalid Rotation Axis Given. Defaulting to X");
                axis = 'x';
                break;

            case 'x':
                axis = 'x';
                break;

            case 'y':
                axis = 'y';
                break;

            case 'z':
                axis = 'z';
                break;
        }
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        // Use "rotateDone" also to keep from resetting value while currently rotating
        if (other.tag == "CollectObject")
        {
            if (rotateObjectValid && rotateDone)
            {
                Debug.Log("Rotating");
                reverse = false;
                rotateDone = false;
            }
        }
        else if (other.tag == "CollectObjectReverse")
        {
            // Use "rotateDone" also to keep from resetting value while currently rotating
            if (rotateObjectValid && rotateDone)
            {
                Debug.Log("Rotating");
                reverse = true;
                rotateDone = false;
            }
        }

    }

    void FixedUpdate()
    {
        if (!rotateDone)
        {
            RotateObject();
        }
    }

    void RotateObject()
    {
        Vector3 currentPos = objectToRotate.transform.eulerAngles;
        int modifier = 1;

        if (reverse)
        {
            modifier = -1;
        }

        switch (axis)
        {
            default:
                Debug.Log("Defaulting to X");
                currentPos.x += (rotationSpeed * Time.deltaTime) * modifier;
                break;

            case 'x':
                currentPos.x += (rotationSpeed * Time.deltaTime) * modifier;
                break;

            case 'y':
                currentPos.x += (rotationSpeed * Time.deltaTime) * modifier;
                break;

            case 'z':
                currentPos.x += (rotationSpeed * Time.deltaTime) * modifier;
                break;
        }

        objectToRotate.transform.eulerAngles = currentPos;

        angleSum += (rotationSpeed * Time.deltaTime) * modifier;

        if (Mathf.Abs(angleSum) > rotationAmount)
        {
            angleSum = 0f;
            rotateDone = true;

            Debug.Log("DONE ROTATION");
        }
    }

}