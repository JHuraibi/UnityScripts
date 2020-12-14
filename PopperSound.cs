using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PopperSound : MonoBehaviour
{
    private AudioSource audioSource;

    public string collectObjectTag;
    public int randomSoundProb;

    public AudioClip collisionSound1;
    public AudioClip collisionSound2;
    public AudioClip collisionSound3;
    public AudioClip randomSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == collectObjectTag)
        {
            bool useRandomSound = false;
            int n = Random.Range(0, 100);

            if (n < randomSoundProb)
            {
                useRandomSound = true;
            }

            n = (n % 3) + 1;

            switch (n)
            {
                default:
                case 1:
                    if (!collisionSound1)
                        return;
                    audioSource.clip = collisionSound1;
                    break;

                case 2:

                    if (!collisionSound2)
                        return;
                    audioSource.clip = collisionSound2;
                    break;

                case 3:

                    if (!collisionSound3)
                        return;
                    audioSource.clip = collisionSound3;
                    break;
            }

            if (useRandomSound && randomSound)
            {
                audioSource.clip = randomSound;
            }

            audioSource.Play();
        }
    }
}
