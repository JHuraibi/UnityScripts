using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class AITargeter : MonoBehaviour
    {
        private AICharacterControl selfLargeAI;
        private bool active;

        public GameObject[] randomCubes;
        public TVController tvSpawner;
        public GameObject tv;
        //public MaterialChange materialChange;
        public GameObject ethanBody;
        public Renderer render;
        public Material regularMaterial;
        public Material activeMaterial;

        // Start is called before the first frame update
        void Start()
        {
            active = false;
            selfLargeAI = GetComponent<AICharacterControl>();
            TargetCube();
        }

        // Update is called once per frame
        void Update()
        {
            if (tvSpawner.spawning && !active)
            {
                TargetTV();
                ActiveMaterial();
                active = true;
            }
            else if (!tvSpawner.spawning && active)
            {
                TargetCube();
                OriginalMaterial();
                active = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "TV")
            {
                TargetCube();
                OriginalMaterial();
            }
        }

        void TargetTV()
        {
            selfLargeAI.SetTarget(tv.transform);
        }

        void TargetCube()
        {
            int whichCube = (int)Random.Range(0, randomCubes.Length);
            selfLargeAI.SetTarget(randomCubes[whichCube].transform);
        }

        public void OriginalMaterial()
        {
            render.material = regularMaterial;
        }

        public void ActiveMaterial()
        {
            render.material = activeMaterial;
        }
    }
}