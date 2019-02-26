using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VBC
{
    public class ActivatorEffect_End : ActivatorEffect {

        public override void Effect(Ball B)
        {
            ConstellationControl.Main().End();
        }
    }
}