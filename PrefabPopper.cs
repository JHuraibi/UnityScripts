using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class PrefabPopper : MonoBehaviour
    {
        public float timer;

        public float selfDestroyTimer;

        // Start is called before the first frame update
        void Start()
        {
            timer = 0f;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                Debug.Log("PLAYER DESTROY");
            }
        }

    }// class

}