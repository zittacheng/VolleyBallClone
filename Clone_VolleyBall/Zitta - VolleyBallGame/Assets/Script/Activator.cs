using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VBC
{
    public class Activator : MonoBehaviour {
        public Animator Anim;
        public bool Active;
        public List<ActivatorEffect> AEs;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Anim)
                Anim.SetBool("Active", Active);
        }

        public virtual void Activate(Ball B)
        {
            if (Active)
                return;

            Active = true;
            foreach (ActivatorEffect AE in AEs)
                AE.Effect(B);
        }
    }
}