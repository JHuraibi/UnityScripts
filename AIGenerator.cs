using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class AIGenerator : MonoBehaviour
    {
        private float timer;
        private float lastGenTimer;
        private int counterAI;
        private bool maxAIReached = false;
        private Vector3 manualPosition = new Vector3(-42f, 125f, 95f);
        private GameObject ai;

        public GameObject[] cars;
        public GameObject prefabAI;
        public int maxAI;
        public float waitTimer;

        // Start is called before the first frame update
        void Start()
        {
            timer = 0.0f;
            counterAI = 0;
            //maxAIReached = false;
        }

        // Update is called once per frame
        void Update()
        {
            timer = timer + Time.deltaTime;

            if (!maxAIReached)
            {
                Generate();
            }
        }

        void Generate()
        {
            if (counterAI > maxAI)
            {
                //Debug.Log("Max AI's reached");
                maxAIReached = true;
                return;
            }

            if (timer - lastGenTimer < waitTimer)
            {
                return;
            }

            int whichCar;

            if (counterAI < cars.Length)
            {
                whichCar = counterAI;
            }
            else
            {
                whichCar = Random.Range(0, cars.Length);
            }

            //ai = Instantiate(prefabAI, prefabAI.transform.position, prefabAI.transform.rotation);
            ai = Instantiate(prefabAI, transform.position, transform.rotation);
            ai.transform.position = manualPosition;
            ai.SetActive(true);
            ai.GetComponent<Rigidbody>().AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            ai.GetComponent<AICharacterControl>().SetTarget(cars[whichCar].transform);

            counterAI++;
            lastGenTimer = timer;
        }


    }
}