using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VBC
{
    public class ActivatorEffect_Elevator : ActivatorEffect {
        public Elevator E;

        public override void Effect(Ball B)
        {
            E.Active();
            base.Effect(B);
        }
    }
}