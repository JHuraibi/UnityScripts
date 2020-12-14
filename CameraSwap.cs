using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CameraSwap : MonoBehaviour
{
    private bool kernelCamOn;

    public Text instructionsUI;
    public Camera cameraRef;

    private void Start()
    {
        kernelCamOn = true;
        instructionsUI.text = "Cashier Cam:";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (kernelCamOn)
            {
                kernelCamOn = false;

                cameraRef.depth = -1f;
                instructionsUI.text = "Kernel Cam:";
            }
            else
            {
                kernelCamOn = true;

                cameraRef.depth = 1f;
                instructionsUI.text = "Cashier Cam:";
            }
        }
    }

}