using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomSounds : MonoBehaviour
{
    private float timer;
    private int size;
    private int waitTime;
    private int soundCounter;

    public bool autoAudioSource;
    public bool limitAtOnce;
    public int maxAtOnce = 1;
    public AudioSource[] audioSources;
    public AudioClip[] sounds;
    public int minWaitTime;
    public int maxWaitTime;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        size = sounds.Length;
        waitTime = Random.Range(minWaitTime, maxWaitTime);
        soundCounter = 0;

        if (autoAudioSource)
        {
            audioSources = new AudioSource[size];

            for (int i = 0; i < size; i++)
            {
                audioSources[i] = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
                audioSources[i].clip = sounds[i];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;

        if (timer > waitTime)
        {
            RandomSound();
            timer = 0.0f;
            waitTime = Random.Range(minWaitTime, maxWaitTime);
        }
    }

    bool CanPlaySound()
    {
        int count = 0;
        for (int i = 0; i < size; i++)
        {
            if (audioSources[i].isPlaying)
            {
                count++;
            }
        }

        return maxAtOnce > count;
    }

    void RandomSound()
    {
        if(limitAtOnce && !CanPlaySound())
        {
            Debug.Log("Number sounds at once reached.");
            return;
        }

        int whichSound = Random.Range(0, size);

        if (!audioSources[whichSound].isPlaying)
        {
            Debug.Log("PLAYING");
            audioSources[whichSound].Play();
        }
    }
}
