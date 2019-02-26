using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VBC
{
    public class MovingPlatform : MonoBehaviour {
        public Rigidbody2D Rig;

        public void Awake()
        {
            Rig = GetComponentInParent<Rigidbody2D>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}