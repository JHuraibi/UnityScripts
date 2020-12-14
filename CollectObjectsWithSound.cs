using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectObjectsWithSound : MonoBehaviour
{
    private int score;
    private bool validAudioSource = true;
    private bool validLevelName = true;
    private AudioClip originalSound;

    public AudioSource audioSource;
    public string nextLevelName;
    public string collectObjectTag;
    public int scoreToGoToNextLevel;
    public bool enableSoundsByTag;
    public string objectTagOne;
    public string objectTagTwo;
    public string objectTagThree;
    public AudioClip soundForTagOne;
    public AudioClip soundForTagTwo;
    public AudioClip soundForTagThree;


    private void Awake()
    {
        if (!GetComponent<AudioSource>())
        {
            Debug.Log("No Audio Source Found On This Object");
            validAudioSource = false;
        }

        if (nextLevelName == "")
        {
            Debug.Log("No Level Name was Given.");
            validLevelName = false;
        }

        if (scoreToGoToNextLevel == 0)
        {
            Debug.Log("Score Given was 0.");
        }
        else if (scoreToGoToNextLevel < 0)
        {
            Debug.Log("Score Given was negative.");
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        score = 0;

        if (audioSource && enableSoundsByTag)
        {
            // Save the AudioSource's clip (for when enableSoundsByTag is on)
            originalSound = audioSource.clip;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!validAudioSource)
        {
            Debug.Log("No Audio Source Found.");
            return;
        }

        if (enableSoundsByTag)
        {
            SoundsByTag(other);
        }
        else
        {
            StandardCollisionSound(other);
        }
    }

    void StandardCollisionSound(Collider other)
    {
        Debug.Log("Collision with tag: " + other.tag);

        if (other.tag == collectObjectTag)
        {
            if (validAudioSource)
            {
                audioSource.clip = originalSound;
                audioSource.Play();
            }

            Destroy(other.GetComponent<Collider>().gameObject);
            HandleScore();
        }
    }

    // CURRENT: Test that sounds are different
    void SoundsByTag(Collider other)
    {
        Debug.Log("Collision with tag: " + other.tag);

        bool validTagAndSoundClip = false;
        string tag = other.tag;

        if (tag == objectTagOne && soundForTagOne)
        {
            audioSource.clip = soundForTagOne;
            validTagAndSoundClip = true;
        }
        else if (tag == objectTagTwo && soundForTagTwo)
        {
            audioSource.clip = soundForTagTwo;
            validTagAndSoundClip = true;
        }
        else if (tag == objectTagThree && soundForTagThree)
        {
            audioSource.clip = soundForTagThree;
            validTagAndSoundClip = true;
        }
        else if (tag == collectObjectTag && originalSound)
        {
            audioSource.clip = originalSound;
            validTagAndSoundClip = true;
        }
        else
        {
            Debug.Log("No sound set for tag \"" + tag + "\"");
        }

        if (validTagAndSoundClip)
        {
            audioSource.Play();
            audioSource.clip = null;        // CHECK: Okay to set to null to clear?

            Destroy(other.GetComponent<Collider>().gameObject);
            HandleScore();
        }
    }

    void HandleScore()
    {
        if (scoreToGoToNextLevel == 0)
        {
            Debug.Log("<color=yellow>Score Given in the inspector is 0</color>");
            return;
        }
        else if (scoreToGoToNextLevel < 0)
        {
            Debug.Log("<color=yellow>Score Given in the inspector was negative!</color>");
            return;
        }

        score = score + 1;

        Debug.Log("Current Score: " + score);

        if (score >= scoreToGoToNextLevel)
        {
            if (!validLevelName)
            {
                Debug.Log("Score Reached, but Level Name Was Not Given.");
                return;
            }

            SceneManager.LoadScene(nextLevelName);
        }
    }

}