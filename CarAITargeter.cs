using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UnityStandardAssets.Vehicles.Car
{
    public class CarAITargeter : MonoBehaviour
    {
        private CarAIControl selfAI;

        public GameObject[] randomCubes;
        public TVController tvSpawner;
        public GameObject tv;

        // Start is called before the first frame update
        void Start()
        {
            selfAI = GetComponent<CarAIControl>();
            ManualTargetChange();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "TV")
            {
                ManualTargetChange();
            }
        }

        void ManualTargetChange()
        {
            int whichCube = Random.Range(0, randomCubes.Length);
            selfAI.SetTarget(randomCubes[whichCube].transform);
        }
    }
}