using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class CarLevelController : MonoBehaviour
    {
        private GameObject playerCar;

        public FirstPersonController playerFPS;
        public GameObject normalCar;
        public GameObject prefabPlayerCar;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                Debug.Log("PLayer Collide");

                Destroy(playerFPS);
                Destroy(normalCar);
                playerCar = Instantiate(prefabPlayerCar, prefabPlayerCar.transform.position, 
                    prefabPlayerCar.transform.rotation);

                playerCar.SetActive(true);
            }
        }
    }
}