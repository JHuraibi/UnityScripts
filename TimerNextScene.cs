using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerNextScene : MonoBehaviour
{
    private float timer;

    public float nextSceneTime;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;
        CheckTime();
    }

    void CheckTime()
    {
        if (timer > nextSceneTime)
        {
            SceneManager.LoadScene("InsideTV");
        }
    }
}
