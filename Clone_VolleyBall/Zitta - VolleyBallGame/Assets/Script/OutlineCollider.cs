using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VBC
{
    public class OutlineCollider : MonoBehaviour {
        public OutlineType OType;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

    public enum OutlineType
    {
        X,
        Y
    }
}