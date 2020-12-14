using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class ColorChange : MonoBehaviour
{
    // Use this for initialization    
    void Start()   {}
   
   // Update is called once per frame    
    void Update() { }
    
    void OnTriggerEnter(Collider other)
    {
    Renderer render = GetComponent<Renderer>();            
    
    render.material.color = Color.yellow;
    }
    }
    
    