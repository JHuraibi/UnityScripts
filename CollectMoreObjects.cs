using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class CollectMoreObjects : MonoBehaviour
{
    private AudioSource audioSource;
    private int score;

    private bool scoreGivenValid = true;
    private bool audioValid = true;
    private bool customTagGiven = true;
    private bool validNextLevelName = true;

    // Anything "public" will show in inspector
    public string tagOfObjectToCollect;
    public bool destoryObjectOnCollision = true;
    public int scoreToReach;
    public string nextLevelName;
    public bool useOptionalSoundClip = false;
    public AudioClip optionalSoundClip;

    private void Awake()
    {
        CheckCollisionTag();
        CheckNextLevelName();
        CheckAudio();
        CheckScoreEntered();
    }

    private void CheckCollisionTag()
    {
        if (tagOfObjectToCollect == "")
        {
            Debug.Log("<color=#802400ff>Warning: </color>No Tag Was Given. Using \"pick_me\".");
            customTagGiven = false;
        }
    }

    private void CheckAudio()
    {
        if (!GetComponent<AudioSource>())
        {
            Debug.Log("<color=red>Error</color>No Audio Source was found on this object." +
                "Please check that there is one.");
            audioValid = false;
            return;
        }

        audioSource = GetComponent<AudioSource>();
        
        if (useOptionalSoundClip && optionalSoundClip)
        {
            audioSource.clip = optionalSoundClip;
            audioValid = true;
        }
        else if (useOptionalSoundClip && !optionalSoundClip)
        {
            if (audioSource.clip)
            {
                Debug.Log("<color=#802400ff>Warning: </color>\"Use Optional Sound Clip\" " +
                    "was checked but no optional clip was given. Using audio clip on Audio Source.");
            }
            else
            {
                Debug.Log("<color=red>\"Use Optional Sound Clip\" was checked, no clip was given," +
                    "and the Audio Source does not have a clip.</color>");
                audioValid = false;
            }
        }
    }

    private void CheckNextLevelName()
    {
        if (nextLevelName == "")
        {
            Debug.Log("No Level Name Was Given.  Using \"level2\".");
            validNextLevelName = false;
        }
    }

    private void CheckScoreEntered()
    {
        if (scoreToReach < 0)
        {
            Debug.Log("<color=#802400ff>Warning:</color> Score To Reach Is Negative. Correct?");
            scoreGivenValid = false;
        }
        else if (scoreToReach == 0)
        {
            Debug.Log("<color=#802400ff>Warning:</color> Score To Reach Is 0.");
        }
        else if (scoreToReach == '\0')
        {
            Debug.Log("<color=red>Error: Score to Reach Field Error.</color>");
        }
    }

    void Start()
    {
        score = 0;
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "pick_me" || other.tag == tagOfObjectToCollect)
        {
            Debug.Log("Collision");
            audioSource.Play();

            if (destoryObjectOnCollision)
            {
                Destroy(other.GetComponent<Collider>().gameObject);
            }

            ScoreHandler();
        }
        else
        {
            Debug.Log("<color=#025594ff>Info: </color>" +
                "Collision with unknown object tag \"" + other.tag + "\"");
        }
    }

    private void ScoreHandler()
    {
        score = score + 1;

        Debug.Log("Current Score: " + score);

        if (score >= scoreToReach && scoreGivenValid)
        {
            NextLevelHandler();
        }
        else if (!scoreGivenValid && score >= 4)
        {
            NextLevelHandler();
        }
    }

    private void NextLevelHandler()
    {
        if (validNextLevelName)
        {
            SceneManager.LoadScene(nextLevelName);
        }
        else
        {
            SceneManager.LoadScene("level2");
        }
    }
}
