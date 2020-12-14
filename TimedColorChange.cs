using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class TimedColorChange : MonoBehaviour
{
    // "color" will be in RGBA mode
    private Color color;
    private float timer;
    private bool timerValid;
    private bool colorChanged;

    public float timeToChange = 0.0f;

    // Use intuitive 0-255 RGB values (See MapColors() below)
    public float red = 255.0f;
    public float green = 255.0f;
    public float blue = 255.0f;

    // Alpha is 0.0-1.0
    public float alpha = 1.0f;


    // Use this for initialization    
    void Start()
    {
        timer = 0.0f;
        timerValid = true;

        float alphaCheckValue = alpha * 255.0f;

        if (ColorValid(red) && ColorValid(green) &&
            ColorValid(blue) && ColorValid(alphaCheckValue))
        {
            MapColors();
            color = new Color(red, blue, green, alpha);
        }
        else
        {
            Debug.Log("<color=red>Error: </color>Invalid color or alpha entered. " +
                "(Red/Green/Blue: 0-255) (Alpha: 0.0-1.0). Defaulting to White.");
            color = Color.white;
        }

        if (timeToChange < 0.0f)
        {
            Debug.Log("<color=red>Error: </color>Timer negative.");
            timerValid = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;
        
        if (timer > timeToChange && !colorChanged && timerValid)
        {
            ChangeColor();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        SetTimer();
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

    void ChangeColor()
    {
        Renderer render = GetComponent<Renderer>();

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
        colorChanged = true;
    }

    // Used both interally and also for other scripts to reset/set timer
    public void SetTimer()
    {
        timer = 0.0f;
    }
}