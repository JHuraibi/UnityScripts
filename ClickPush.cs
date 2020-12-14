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
public class ClickPush : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 direction;
    private bool applyForce;

    public int whichDirection;
    public float strength;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        applyForce = false;
        SetDirection();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (applyForce)
        {
            rb.AddForce(direction * strength);
            applyForce = false;
        }
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
                Debug.Log("<color=#820200>Which Direction Not Valid. Pick 1-6.");
                direction = Vector3.up;
                break;

        }
    }

    void OnMouseDown()
    {
        applyForce = true;
    }
}
