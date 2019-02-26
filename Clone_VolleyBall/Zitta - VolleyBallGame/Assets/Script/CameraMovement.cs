using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VBC
{
    public class CameraMovement : MonoBehaviour {
        public Character C;
        public Vector3 RelativePosition;
        public float MaxXDelay;
        public float XDelay;
        public float XDelayDecay;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LateUpdate()
        {
            if (C.Reseting)
                return;

            XDelay = transform.position.x - (C.transform.position.x + RelativePosition.x);
            if (Mathf.Abs(XDelay) > 0.1f)
            {
                if (XDelay > 0)
                    XDelay -= XDelayDecay * Time.deltaTime;
                else if (XDelay < 0)
                    XDelay += XDelayDecay * Time.deltaTime;
            }
            else
                XDelay = 0;
            if (Mathf.Abs(XDelay) > MaxXDelay)
            {
                if (XDelay > 0)
                    XDelay = MaxXDelay;
                else if (XDelay < 0)
                    XDelay = -MaxXDelay;
            }
            //float X = C.transform.position.x + RelativePosition.x + XDelay;
            float X = C.transform.position.x + RelativePosition.x;
            float Y = C.transform.position.y + RelativePosition.y;
            transform.position = new Vector3(X, Y, transform.position.z);
        }
    }
}