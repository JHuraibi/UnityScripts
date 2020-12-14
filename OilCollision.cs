using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class OilCollision : MonoBehaviour
{
    //protecttheValueIWant = parentReferenceScript.theValueIWant
    public bool inOil;

    void Awake()
    {
        inOil = false;
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("COLLIDED WITH: " + other.tag);
        if (other.tag == "Oil" && !inOil)
        {
            inOil = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Oil")
        {
            inOil = false;
        }
    }
}
