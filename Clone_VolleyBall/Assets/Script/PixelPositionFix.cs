using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VBC
{
    public class PixelPositionFix : MonoBehaviour {
        public float PPU = 100;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void FixedUpdate()
        {

        }

        public static Vector2 Fixed(Vector2 OriPosition, float PPU)
        {
            Vector2 a = new Vector2(Mathf.RoundToInt(OriPosition.x * PPU), Mathf.RoundToInt(OriPosition.y * PPU));
            return a / PPU;
        }
    }
}