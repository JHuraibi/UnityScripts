using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingLight : MonoBehaviour
{
    Light flashingLight;
    private bool rampingUp;

    public float activationSensitivity;
    public float startingBrightness;
    public float offThreshold;
    public float rampUpSpeed;
    public float decayRate;


    // Start is called before the first frame update
    void Start()
    {
        flashingLight = GetComponent<Light>();
        flashingLight.enabled = false;
        rampingUp = false;
        //randomActivation();
    }

    // Update is called once per frame
    void Update()
    {
        //randomActivation();
        if (flashingLight.enabled && rampingUp) {
            rampUp();
        }
        else if (flashingLight.enabled)
        {
            rampDown();
        }
        else
        {
            randomActivation();
        }
    }

    void randomActivation()
    {
        float randFloat = Random.Range(0.0f, 1.0f);
        //Debug.Log(randFloat);
        //if (randFloat > activationSensitivity)
        if (activationSensitivity > randFloat)
        {
            flashingLight.enabled = true;
            flashingLight.intensity = 0.0f;
            rampingUp = true;
        }
    }

    void rampUp()
    {
        flashingLight.intensity += rampUpSpeed;

        if (flashingLight.intensity > startingBrightness)
        {
            rampingUp = false;
        }
    }

    void rampDown()
    {
        flashingLight.intensity -= decayRate;
        //Debug.Log(flashingLight.intensity);

        if (offThreshold > flashingLight.intensity)
        {
            flashingLight.enabled = false;
            //Debug.Log("OFF");
        }
    }

    //IEnumerator Flashing()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds();

    //    }
    //}
}
