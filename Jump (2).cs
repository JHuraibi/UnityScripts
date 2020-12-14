using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Jump : MonoBehaviour
{
    private Rigidbody rb;
    private float timer;
    private float lastJumpTime;
    private bool jumpAvailable;

    public float jumpHeight;
    public float jumpLockOutTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        timer = 0.0f;
        jumpAvailable = true;

        if (jumpHeight < 0)
        {
            Debug.Log("<color=#802400ff>Warning: </color>Jump height was negative. Defaulting to 3.");
            jumpHeight = 3;
        }
        else if (jumpHeight == 0.0f)
        {
            Debug.Log("<color=#802400ff>Warning: </color>Jump height was 0. Defaulting to 3.");
            jumpHeight = 3;
        }

        if (jumpLockOutTime < 0.0f)
        {
            Debug.Log("<color=#802400ff>Warning: </color>Jump lockout time was negative. " +
                "Defaulting to 0.");
            jumpLockOutTime = 0.0f;
        }
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        if (timer - lastJumpTime > jumpLockOutTime)
        {
            jumpAvailable = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpAvailable)
        {
            rb.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);

            lastJumpTime = timer;
            jumpAvailable = false;
        }
    }
}