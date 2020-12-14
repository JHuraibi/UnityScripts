using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Inspector Values for "Which Direction":
 *  [1] Up
 *  [2] Down
 *  [3] Left
 *  [4] Right
 *  [5] Forward
 *  [6] Back
*/

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class PushCharacter : MonoBehaviour
    {
        private bool messageShown = false;

        private float timer;
        private Vector3 direction;
        private float lastPush;
        private bool jumpAvailable;

        public ThirdPersonCharacter character;
        public int whichDirection;
        public float strength;
        public float pushLockout;

        // Start is called before the first frame update
        void Start()
        {
            timer = 0.0f;
            lastPush = 0.0f;
            jumpAvailable = true;
            SetDirection();

            if (pushLockout < 0)
            {
                Debug.Log("<color=#ff9999>Warning: </color>Lockout less than 0.");
                pushLockout = 60.0f;
            }
        }

        void Update()
        {
            timer = timer + Time.deltaTime;
            CheckJumpEnabled();
        }

        void OnMouseDown()
        {
            if (jumpAvailable)
            {
                Debug.Log("PUSHED");
                messageShown = false;

                character.JumpOverride(strength, direction);
                jumpAvailable = false;
                lastPush = timer;
            }
        }

        void SetDirection()
        {
            switch (whichDirection)
            {
                case 1:
                    direction = Vector3.up;
                    break;
                case 2:
                    direction = Vector3.down;
                    break;
                case 3:
                    direction = Vector3.left;
                    break;
                case 4:
                    direction = Vector3.right;
                    break;
                case 5:
                    direction = Vector3.forward;
                    break;
                case 6:
                    direction = Vector3.back;
                    break;
                default:
                    Debug.Log("<color=820200>Which Direction Not Valid. Pick 1-6.");
                    direction = Vector3.up;
                    break;
            }

            direction = direction * strength;
        }

        void CheckJumpEnabled()
        {
            if ((timer - lastPush) > pushLockout)
            {
                jumpAvailable = true;
                Debug.Log("READY");
            }
        }
    }
}