using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeChange : MonoBehaviour
{
    Vector3 scaleChange;
    bool changeSize = false;

    // Start is called before the first frame update
    void Start()
    {
        scaleChange = new Vector3(2.0f, 2.0f, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        changeSize = true;
    }
}
