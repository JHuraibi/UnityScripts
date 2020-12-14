// Kernel burns when however many seconds timeBeforePop is, ON TOP OF
//      how many seconds timeBeforeBurn is.
// e.g. if (timeBeforePop == 5) and (timeBeforeBurn == 5) then kernel
//          burns when player in oil for a TOTAL of 10 seconds

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UnityStandardAssets.Cameras
{
    [RequireComponent(typeof(Text))]
    public class MasterController : MonoBehaviour
    {
        private float timer;
        private float cookTimer;
        private float lastMessage;

        private bool popped;
        private bool burned; 

        public float timeBeforePop;
        public float timeBeforeBurn;
        public float popHeight;

        public GameObject kernelAssemblyReference;
        public GameObject kernelReference;
        public GameObject poppedAssemblyReference;
        public GameObject poppedReference;
        public OilCollision kernelOilRef;
        public OilCollision poppedOilRef;

        //public Text timerUI;
        public Text titleUI;
        public Text subtitleUI;

        public ProtectCameraFromWallClip camClipRef;
        public FreeLookCam freeLookRef;

        void Start()
        {
            timer = 0.0f;
            cookTimer = 0.0f;
            lastMessage = 0.0f;

            popped = false;
            burned = false;

            //timerUI.text = "";
            titleUI.text = "";
            subtitleUI.text = "";
        }

        void Update()
        {
            timer = timer + Time.deltaTime;

            //UpdateCookTimerUI();
            UpdateTitleUI();
            UpdateSubtitleUI();

            // Cook timer only runs if unpopped kernel is in the oil
            if (kernelOilRef.inOil && !popped)
            {
                cookTimer = cookTimer + Time.deltaTime;
            }
        }

        //private void UpdateCookTimerUI()
        //{
            //string currentTime = ClockFormatter(cookTimer);
            //timerUI.text = currentTime;
        //}

        private void UpdateTitleUI()
        {
            if (cookTimer > timeBeforePop && !popped)
            {
                Pop();
                popped = true;

                titleUI.text = "Popped!";
                lastMessage = timer;
            }
            else if ((timeBeforePop - cookTimer < 4) && !popped)
            {
                PopCountDown();
            }

            if (timer - lastMessage > 2)
            {
                titleUI.text = "";
            }
        }

        private void UpdateSubtitleUI()
        {
            if (kernelOilRef.inOil && !popped)
            {
                subtitleUI.text = "Cooking...";
            }
            else
            {
                subtitleUI.text = "";
            }
        }

        private void PopCountDown()
        {
            string timeToPop = ClockFormatter(timeBeforePop - cookTimer);

            titleUI.text = "Popping in " + timeToPop[4];
            lastMessage = timer;
        }

        private void Pop()
        {
            //Vector3 target = kernelReference.transform.position;
            poppedAssemblyReference.SetActive(true);
            poppedReference.transform.position = kernelReference.transform.position;
            Destroy(kernelAssemblyReference);

            camClipRef.closestDistance = 5f;
            freeLookRef.m_MoveSpeed = 3f;

            poppedReference.GetComponent<Rigidbody>().AddForce(new Vector3(0, popHeight, 0), ForceMode.Impulse);
        }

        private string ClockFormatter(float rawTime)
        {
            if (rawTime < 0)
            {
                Debug.Log("Time provided was negative: " + rawTime);
                return "";
            }

            int minutes = (int)(rawTime / 60);
            int seconds = (int)(rawTime % 60);
            string formattedTime = "";

            if (minutes < 10)
            {
                formattedTime += "0";
            }

            formattedTime += minutes.ToString();

            formattedTime += ":";

            if (seconds < 10)
            {
                formattedTime += "0";
            }

            formattedTime += seconds.ToString();

            // Uncomment line below to print the current time to the console in Unity
            //Debug.Log(minutes + ":" + seconds);

            return formattedTime;
        }
    }
}