using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VBC
{
    public class ZoneEffect_CheckPoint : ZoneEffect {
        public CheckPoint CP;

        public override void Effect()
        {
            CP.Activate();
            base.Effect();
        }
    }
}