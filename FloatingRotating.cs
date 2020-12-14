using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using Random = UnityEngine.Random;


// Disable the "[SerializedField] is never assigned" warning in the console :)
#pragma warning disable 649

[RequireComponent(typeof(AudioSource))]
public class FloatingRotating : MonoBehaviour
{
    private float timer;
    private float soundTimer;
    private bool playerNotFound;
    private bool soundOneMissing;
    private bool soundTwoMissing;
    private bool soundThreeMissing;
    private bool soundFourMissing;
    [SerializeField] private bool disableSound;

    // Below 2 methods of using multiple sounds not working properly
    //[SerializeField] private AudioClip[] squishSounds;
    //private AudioClip[] squishSounds;

    private AudioSource audioSource;
    private GameObject playerObjRef;
    private FirstPersonController player;

    public AudioClip collisionSound1;
    public AudioClip collisionSound2;
    public AudioClip collisionSound3;
    public AudioClip collisionSound4;
    public float waterLevel;
    public float floatHeight;
    public Vector3 buoyancyCentreOffset;
    public float bounceDamp;
    public float speedStartTimer;
    public float speedChangeTimer;
    public float preTimerRotationSpeed;
    public float postTimerRotationSpeed;
    public float soundReplayDelay;
    public int activationProbability;
    public float FOVIncreaseAmount;


    private void Start()
    {
        if (!disableSound)
        {
            CheckSound();
        }

        // Set false for now
        playerNotFound = false;
        soundOneMissing = false;
        soundTwoMissing = false;
        soundThreeMissing = false;
        soundFourMissing = false;
    }

    private void CheckSound()
    {
        // Audio source (should be attached to same object as this script)
        if (GetComponent<AudioSource>())
        {
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Debug.Log("No Audio Source Found. Does object have an Audio Source?");
            disableSound = true;
            return;
        }


        if (!collisionSound1)
        {
            Debug.Log("Collision Sound 1 - Missing");
            soundOneMissing = true;
        }
        if (!collisionSound2)
        {
            Debug.Log("Collision Sound 2 - Missing");
            soundTwoMissing = true;
        }
        if (!collisionSound3)
        {
            Debug.Log("Collision Sound 3 - Missing");
            soundThreeMissing = true;
        }
        if (!collisionSound4)
        {
            Debug.Log("Collision Sound 4 - Missing");
            soundFourMissing = true;
        }
    }

    private void Awake()
    {
        // Reference to Player Controller
        if (GameObject.Find("FPSController"))
        {
            playerObjRef = GameObject.Find("FPSController");
            Debug.Log("FPSController Found");
        }
        else if (GameObject.Find("ThirdPersonController"))
        {
            playerObjRef = GameObject.Find("ThirdPersonController");
            Debug.Log("ThirdPersonController Found");
        }

        timer = 0.0f;
        soundTimer = 0.0f;

        if (playerObjRef != null)
            player = playerObjRef.GetComponent<FirstPersonController>();
        else
        {
            Debug.Log("Player Controller - NOT FOUND. Is there a FPSController or ThirdPersonController?");
            playerNotFound = true;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (disableSound)
        {
            return;
        }

        float soundDelta = timer - soundTimer;

        if ((other.tag == "Player" || other.tag == "MainCamera") && (soundDelta > soundReplayDelay))
        {
            // Randomly pick a sound clip out of the 4
            int n = Random.Range(0, 4);
            n = n + 1;

            switch (n)
            {
                case 1:

                    if (soundOneMissing)
                        return;
                    audioSource.clip = collisionSound1;
                    break;

                case 2:

                    if (soundTwoMissing)
                        return;
                    audioSource.clip = collisionSound2;
                    break;

                case 3:

                    if (soundThreeMissing)
                        return;
                    audioSource.clip = collisionSound3;
                    break;

                case 4:

                    if (soundFourMissing)
                        return;
                    audioSource.clip = collisionSound4;
                    break;

                default:
                    audioSource.clip = collisionSound1;
                    break;
            }

            audioSource.Play();
            soundTimer = timer;
        }
    }

    void FixedUpdate()
    {
        if (playerNotFound)
        {
            return;
        }

        Vector3 actionPoint = transform.position + transform.TransformDirection(buoyancyCentreOffset);
        float forceFactor = 1f - ((actionPoint.y - waterLevel) / floatHeight);

        if (forceFactor > 0f)
        {
            Vector3 uplift = -Physics.gravity * (forceFactor - GetComponent<Rigidbody>().velocity.y * bounceDamp);
            GetComponent<Rigidbody>().AddForceAtPosition(uplift, actionPoint);
        }

        this.RotateTowardPlayer();
        timer += Time.fixedDeltaTime;
    }

    void RotateTowardPlayer()
    {
        if (playerNotFound)
        {
            return;
        }

        float rotateStr;

        if (timer < speedStartTimer)
        {
            rotateStr = 0;
        }
        else if (timer < speedChangeTimer)
        {
            rotateStr = preTimerRotationSpeed / 100;
        }
        else
        {
            rotateStr = postTimerRotationSpeed / 100;
        }


        Vector3 targetTransform = player.transform.position - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(targetTransform, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateStr);
    }
}