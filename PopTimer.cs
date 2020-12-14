using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopTimer : MonoBehaviour
{
    private GameObject PoppedKernel;
    private GameObject Kernel;
    private float timer;
    private float timeBeforePop;

    private bool inOil;
    private bool popped;

    public float minPopTime;
    public float maxPopTime;
    public float minPopHeight;
    public float maxPopHeight;

    public GameObject prefabKernel;
    public GameObject prefabPoppedKernel;

    void Start()
    {
        // Random pop wait time between the max and min from inspector
        timeBeforePop = Random.Range(minPopTime, maxPopHeight);
        timer = 0.0f;
        popped = false;

        // TODO: Random rotation
        //Debug.Log("ROTATION: " + transform.rotation);

        Kernel = Instantiate(prefabKernel, transform.position, transform.rotation);
        Kernel.SetActive(true);

        if (!Kernel)
        {
            Debug.Log("<color=red>Error: </color>Kernel object not found.");
        }
    }

    void Update()
    {
        // Timer only runs while in the oil
        if (inOil)
        {
            timer = timer + Time.deltaTime;
        }

        // Pop when timer moer than the random pop wait time
        if (timer > timeBeforePop && !popped)
        {
            Pop();
            popped = true;
        }

        // Add another long timer to burn the kernel
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Oil" && !inOil)
        {
            //Debug.Log("In Oil");
            inOil = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (inOil)
        {
            inOil = false;
        }
    }

    private void Pop()
    {
        float popHeight = Random.Range(minPopHeight, maxPopHeight);

        Destroy(Kernel);
        PoppedKernel = Instantiate(prefabPoppedKernel, transform.position, transform.rotation);
        PoppedKernel.SetActive(true);
        PoppedKernel.GetComponent<Rigidbody>().AddForce(new Vector3(0, popHeight, 0), ForceMode.Impulse);

        popped = true;
    }
}
