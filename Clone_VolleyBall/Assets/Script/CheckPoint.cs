using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VBC
{
    public class CheckPoint : MonoBehaviour {
        public string Key;
        public bool StartActive;
        public GameObject CharacterPoint;

        public void Awake()
        {
            if (StartActive)
                Activate();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Activate()
        {
            ConstellationControl.Main().SetCheckPoint(this);
        }
    }
}