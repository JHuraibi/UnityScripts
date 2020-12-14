using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVCollider : MonoBehaviour
{
    public bool collided = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
        {
            collided = true;
        }
    }
}
