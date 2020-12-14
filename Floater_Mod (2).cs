using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using Random = UnityEngine.Random;


// Disable the "[SerializedField] is never assigned" warning in the console :)
#pragma warning disable 649

[RequireComponent(typeof(AudioSource))]
public class Floater_Mod : MonoBehaviour
{
    private float timer;
    private float soundTimer;

    // Below 2 methods of using multiple sounds not working properly
    //[SerializeField] private AudioClip[] squishSounds;
    //private AudioClip[] squishSounds;

    private AudioSource squishSource;
    private GameObject playerObjRef;
    private FirstPersonController player;

    public AudioClip squishSound1;
    public AudioClip squishSound2;
    public AudioClip squishSound3;
    public AudioClip squishSound4;
    public float waterLevel, floatHeight;
    public Vector3 buoyancyCentreOffset;
    public float bounceDamp;
    public float speedStartTimer;
    public float speedChangeTimer;
    public float preTimerRotationSpeed;
    public float postTimerRotationSpeed;
    public float soundReset;
    public int activationProbability;
    public float FOVIncreaseAmount;

    private Camera m_Camera;


    private void Start()
    {
        // Audio source
        squishSource = GetComponent<AudioSource>();
        m_Camera = Camera.main;
    }

    private void Awake()
    {
        // Reference to FPSController
        playerObjRef = GameObject.Find("FPSController");

        timer = 0.0f;
        soundTimer = 0.0f;

        if (playerObjRef != null)
            player = playerObjRef.GetComponent<FirstPersonController>();
        else
            Debug.Log("FPSController NOT FOUND");
    }


    void OnTriggerEnter(Collider other)
    {
        

        float soundDelta = timer - soundTimer;

        if (other.tag == "FloatingEyeball" && (soundDelta > soundReset))
        {
            // Randomly pick a sound clip out of 4
            int n = Random.Range(0, 4);

            switch (n)
            {
                case 0:
                    squishSource.clip = squishSound1;
                    break;
                case 1:
                    squishSource.clip = squishSound2;
                    break;
                case 2:
                    squishSource.clip = squishSound3;
                    break;
                case 3:
                    squishSource.clip = squishSound4;
                    break;
                default:
                    squishSource.clip = squishSound1;
                    break;
            }

            squishSource.Play();
            soundTimer = timer;
        }
    }

    void FixedUpdate()
    {
        Vector3 actionPoint = transform.position + transform.TransformDirection(buoyancyCentreOffset);
        float forceFactor = 1f - ((actionPoint.y - waterLevel) / floatHeight);

        if (forceFactor > 0f)
        {
            Vector3 uplift = -Physics.gravity * (forceFactor - GetComponent<Rigidbody>().velocity.y * bounceDamp);
            GetComponent<Rigidbody>().AddForceAtPosition(uplift, actionPoint);
        }

        this.RotateTowardPlayer();
        timer += Time.fixedDeltaTime;
        //this.PrintDeltas();
    }

    void RotateTowardPlayer()
    {
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

        // Rotate gameobject by -90
    }

    void PrintDeltas()
    {
        float playerX = player.transform.position.x;
        float eyeballX = this.transform.position.x;

        float playerZ = player.transform.position.z;
        float eyeballZ = this.transform.position.z;

        if (playerX > eyeballX)
            Debug.Log("PLAYER X GREATER");
        else
            Debug.Log("EYEBALL X GREATER");

        if (playerZ > eyeballZ)
            Debug.Log("PLAYER Z GREATER");
        else
            Debug.Log("EYEBALL Z GREATER");
    }
}