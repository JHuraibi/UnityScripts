using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Inspector Values for "Which Direction":
 *  [1] Up
 *  [2] Down
 *  [3] Left
 *  [4] Right
 *  [5] Forward
 *  [6] Back
*/

[RequireComponent(typeof(Rigidbody))]
public class ClickPushCharacter : MonoBehaviour
{
    private Rigidbody rb;
    private float timer;
    private Vector3 direction;
    private bool applyForce;
    private float lastPush;

    public int whichDirection;
    public float strength;
    public float pushDurationInFrames;
    public float pushLockoutInFrames;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        lastPush = -100.0f;     // Negative allows for a push at first frame

        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        applyForce = false;
        SetDirection();

        if (pushDurationInFrames < 0)
        {
            Debug.Log("<color=#ff9999>Warning: </color>Push duration less than 0");
        }

        if (pushLockoutInFrames < 0)
        {
            Debug.Log("<color=#ff9999>Warning: </color>Lockout less than 0. Setting to 60 frames");
        }
    }

    void FixedUpdate()
    {
        timer = timer + Time.deltaTime;

        if (applyForce)
        {
            Push();
        }
    }

    void Update()
    {
    }

    void SetDirection()
    {
        switch (whichDirection)
        {
            case 1:
                direction = Vector3.up;
                break;
            case 2:
                direction = Vector3.down;
                break;
            case 3:
                direction = Vector3.left;
                break;
            case 4:
                direction = Vector3.right;
                break;
            case 5:
                direction = Vector3.forward;
                break;
            case 6:
                direction = Vector3.back;
                break;
            default:
                Debug.Log("<color=820200>Which Direction Not Valid. Pick 1-6.");
                direction = Vector3.up;
                break;
        }
    }

    void OnMouseDown()
    {
        if (!PushLocked())
        {
            applyForce = true;
            lastPush = timer;
        }
    }

    void Push()
    {
        //GetComponent<Rigidbody>().AddForce(direction * strength);
        GetComponent<Rigidbody>().AddForce(new Vector3(0, 1, 0) * strength, ForceMode.Impulse);

        if (timer - lastPush > pushDurationInFrames)
        {
            applyForce = false;
            Debug.Log("DONE PUSH");
        }
    }

    bool PushLocked()
    {
        return (timer - lastPush) < pushLockoutInFrames;
    }
}
