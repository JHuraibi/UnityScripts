using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChange : MonoBehaviour
{
    public Renderer render;
    public Material regularMaterial;
    public Material activeMaterial;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OriginalMaterial()
    {
        render.material = regularMaterial;
        Debug.Log("ORIGINAL MATERIAL");
    }

    public void ActiveMaterial()
    {
        render.material = activeMaterial;
        Debug.Log("ACTIVE MATERIAL");
    }
}
