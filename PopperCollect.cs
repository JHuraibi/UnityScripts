using UnityEngine;
using UnityEngine.UI;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class PopperCollect : MonoBehaviour
    {
        private bool showJump;

        public Text jumpUI;
        public ThirdPersonCharacter thirdPersonRef;
        public string collectObjectTag;
        public float jumpIncrements;
        public float maxJumpHeight;

        // Start is called before the first frame update
        void Start()
        {
            showJump = false;
            jumpUI.text = "";
        }

        // Update is called once per frame
        void Update()
        {
            if (thirdPersonRef)
            {
                jumpUI.text = "Jump: " + (int)thirdPersonRef.m_JumpPower;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            //Debug.Log("PopperCollect: Collision.");
            if (other.tag == collectObjectTag)
            {
                other.GetComponent<Collider>().gameObject.SetActive(false);
                if (!thirdPersonRef)
                {
                    Debug.Log("Collision But No Third Person Reference Given.");
                    return;
                }

                if (thirdPersonRef.m_JumpPower > maxJumpHeight)
                {
                    Debug.Log("Max Jump Height Reached");
                    return;
                }

                thirdPersonRef.m_JumpPower += jumpIncrements;
                showJump = true;
            }
        }

    }// class

}