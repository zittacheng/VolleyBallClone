using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VBC
{
    public class ActivatorEffect_Replenish : ActivatorEffect {
        public Activator A;
        public Ball LastBall;

        public override void Effect(Ball B)
        {
            if (LastBall == B)
                return;
            LastBall = B;
            base.Effect(B);
            StartCoroutine(EffectIE(B));
        }

        public IEnumerator EffectIE(Ball B)
        {
            B.DestroyTime = 0;
            Character.Main().LaunchReplenish();
            yield return 0;
            A.Active = false;
        }
    }
}