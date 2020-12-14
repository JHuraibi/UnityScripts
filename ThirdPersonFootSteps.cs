using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonFootSteps : MonoBehaviour
{
    private AudioSource stepAudioSource;
    [SerializeField] private AudioClip[] m_FootstepSounds;

    // Start is called before the first frame update
    void Start()
    {
        stepAudioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
