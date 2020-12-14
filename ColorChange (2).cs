using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class ColorChange : MonoBehaviour
{
    // "color" will be in RGBA mode
    private Color color;
    private Color startColor;
    private Renderer render;

    // Use intuitive 0-255 RGB values (See MapColors() at bottom)
    public float red = 255.0f;
    public float green = 255.0f;
    public float blue = 255.0f;

    // Alpha is 0.0-1.0
    public float alpha = 1.0f;

    // Use this for initialization    
    void Start()
    {
        float alphaCheckValue = alpha * 255.0f;
        render = GetComponent<Renderer>();
        startColor = render.material.color;

        if ( ColorValid(red) && ColorValid(green) &&
            ColorValid(blue) && ColorValid(alphaCheckValue) )
        {            
            MapColors();
            color = new Color(red, blue, green, alpha);
        }
        else
        {
            Debug.Log("<color=red>Error: </color>Invalid color or alpha entered. " +
                "(Red/Blue/Green: 0-255) (Alpha: 0.0-1.0). Defaulting to White.");
            color = Color.white;
        }
    }

    // Update is called once per frame
    void Update() { }

    void OnTriggerEnter(Collider other)
    {
        ChangeColor();
    }

    public void ChangeColor()
    {
        // Setting object's Shader to allow transparency 
        // (See Unity Documentation on StandardShaderGUI.cs)
        render.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        render.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        render.material.SetInt("_ZWrite", 0);
        render.material.DisableKeyword("_ALPHATEST_ON");
        render.material.DisableKeyword("_ALPHABLEND_ON");
        render.material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        render.material.renderQueue = 3000;

        // Setting the actual RGBA color of the object
        render.material.color = color;
        Debug.Log("COLOR CHANGE");
    }

    public void ResetColor()
    {
        render.material.color = startColor;
    }

    bool ColorValid(float value)
    {
        return value >= 0.0f && value <= 255.0f;
    }

    void MapColors()
    {
        // RGB is commonly known to be 0-255.
        // However Unity's Color takes values between 0.0-1.0
        red = red / 255.0f;
        green = green / 255.0f;
        blue = blue / 255.0f;
    }
}