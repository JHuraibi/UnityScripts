using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// PARENT OF KERNEL
public class KernelOilRelay : MonoBehaviour
{
    //protecttheValueIWant = parentReferenceScript.theValueIWant
    private GameObject kernelChild;
    public bool kernelInOil;

    private bool shown = false;

    void Start()
    {
        kernelChild = GameObject.Find("PlayerAlmondKernel");
        kernelInOil = kernelChild.GetComponent<OilCollision>().inOil;
    }

    private void Update()
    {
        if (kernelInOil && !shown)
        {
            Debug.Log("FROM RELAY: " + kernelInOil);

        }
    }
}
