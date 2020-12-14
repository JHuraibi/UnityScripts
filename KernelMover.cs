using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class KernelMover : MonoBehaviour
{
    private Rigidbody rb = null;

    private void Awake()
    {
        if (!GetComponent<Rigidbody>())
        {
            Debug.Log("NO RIGIDBODY");
        }
        else
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    // CURRENT 2: Maybe can add a tag to floor to make kernel move on collision
    //void OnTriggerEnter(Collider other)
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("X: " + rb.velocity.x);
        //Debug.Log("Y: " + rb.velocity.y);
        //Debug.Log("Z: " + rb.velocity.z);
        
        // CURRENT:
        if (rb.velocity.x == 0)
        {
            rb.velocity = new Vector3(0, 0.01f, 0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Kernel: Exited");
    }
}
