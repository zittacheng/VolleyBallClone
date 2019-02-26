using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VBC
{
    public class ZoneEffect_Kill : ZoneEffect {

        public override void Effect()
        {
            Character.Main().CharacterReset();
            base.Effect();
        }
    }
}