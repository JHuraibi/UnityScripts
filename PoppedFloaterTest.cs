using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PoppedFloaterTest : MonoBehaviour
{
    private bool inOil = false;
    private Rigidbody rb;

    public float waterLevel;
    public float floatHeight;
    public float bounceDamp;
    public Vector3 buoyancyCentreOffset;
    //public float minRandomOffset;
    //public float maxRandomOffset;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Weird effects if applying while kernel isn't in the oil
        // Only float when the kernel or popped kernel is in oil
        if (inOil)
        {
            Vector3 actionPoint = transform.position + transform.TransformDirection(buoyancyCentreOffset);
            float forceFactor = 1f - ((actionPoint.y - waterLevel) / floatHeight);

            if (forceFactor > 0f)
            {
                Vector3 uplift = -Physics.gravity * (forceFactor - GetComponent<Rigidbody>().velocity.y * bounceDamp);
                rb.AddForceAtPosition(uplift, actionPoint);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Oil" && !inOil)
        {
            //Debug.Log("Floater: In Oil");
            inOil = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Floater: Out of Oil");
        //inOil = false;
    }
}
