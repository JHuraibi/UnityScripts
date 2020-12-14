using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PoppedFloater : MonoBehaviour
{
    private bool inOil = false;
    private Rigidbody rb;
    private Vector3 buoyancyCentreOffset;

    public float waterLevel;
    public float floatHeight;
    public float bounceDamp;
    public float minRandomOffset;
    public float maxRandomOffset;
    public float xMinRandomOffset;
    public float xMaxRandomOffset;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // X axis can handle larger absolute values
        float x = Random.Range(xMinRandomOffset, xMaxRandomOffset);

        // Y and Z are more sensitive
        float y = Random.Range(minRandomOffset, maxRandomOffset);
        float z = Random.Range(minRandomOffset, maxRandomOffset);

        // In case values Y or Z are 0.0
        if (y == 0) { y = 0.01f; }
        if (z == 0) { z = 0.01f; }

        buoyancyCentreOffset = new Vector3(x, y, z);

        Debug.Log("Random X: " + z);
        //Debug.Log("Random Y: " + y);
        //Debug.Log("Random Z: " + z);
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
            Debug.Log("Floater: In Oil");
            inOil = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        inOil = false;
        Debug.Log("Floater: Out of Oil");
    }
}
