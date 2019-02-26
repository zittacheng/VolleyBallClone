using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VBC
{
    public class Zone : MonoBehaviour {
        public List<ZoneEffect> ZEs;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnTriggerEnter2D(Collider2D C)
        {
            if (C.GetComponent<Character>() && C.GetComponent<Character>() == Character.Main())
            {
                foreach (ZoneEffect ZE in ZEs)
                    ZE.Effect();
            }
        }
    }
}