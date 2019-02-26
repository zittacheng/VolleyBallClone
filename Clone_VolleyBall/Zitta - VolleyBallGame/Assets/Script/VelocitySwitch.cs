using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VBC
{
    public class VelocitySwitch : MonoBehaviour {
        public Rigidbody2D Rig;
        public float Speed;
        public float SX;
        public float TempSX;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Rig.velocity = new Vector2(SX * Speed + TempSX, Rig.velocity.y);
        }

        public void SetSX(float Value)
        {
            SX = Value;
        }

        public void SetTempSX(float Value)
        {
            TempSX = Value;
        }
    }
}