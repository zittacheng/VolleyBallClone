using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VBC
{
    public class Ball : MonoBehaviour {
        public Animator Anim;
        public Rigidbody2D Rig;
        [Space]
        public bool Active = true;
        public bool Launched;
        public int Life;
        public float DestroyTime;
        public float RealSpeed;
        public float SpeedX;
        public float SpeedY;
        public float SpeedScale;
        public float SpeedScaleDecay;
        public List<OutlineCollider> OCs;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Rig.velocity = new Vector2(SpeedX * SpeedScale, SpeedY * SpeedScale);
            DestroyTime -= Time.deltaTime;
            if (DestroyTime <= 0)
                DestroyBall();
        }

        public void Launch(Vector2 Direction)
        {
            Direction.Normalize();
            Direction *= RealSpeed;
            SpeedX = Direction.x;
            SpeedY = Direction.y;
        }

        public void OnTriggerEnter2D(Collider2D C)
        {
            if (!Active)
                return;

            if (C.GetComponent<OutlineCollider>())
            {
                if (OCs.Count > 0)
                    return;
                OCs.Add(C.GetComponent<OutlineCollider>());
                if (C.GetComponent<OutlineCollider>().OType == OutlineType.X)
                    SpeedY = -SpeedY;
                else if (C.GetComponent<OutlineCollider>().OType == OutlineType.Y)
                    SpeedX = -SpeedX;

                SolidContact();
            }
            else if (C.GetComponent<CharacterCollider>())
            {
                if (OCs.Count > 0)
                    return;
                if (!C.GetComponent<CharacterCollider>().C)
                    return;
                if (!Launched)
                    return;

                Vector2 TV = transform.position - C.transform.position;
                Launch(TV);

                SolidContact();
            }
            else if (C.GetComponent<Activator>())
            {
                C.GetComponent<Activator>().Activate(this);
            }
        }

        public void OnTriggerExit2D(Collider2D C)
        {
            if (C.GetComponent<OutlineCollider>())
            {
                if (OCs.Contains(C.GetComponent<OutlineCollider>()))
                    OCs.Remove(C.GetComponent<OutlineCollider>());
            }
            else if (C.GetComponent<CharacterCollider>())
            {
                Launched = true;
            }
        }

        public void SolidContact()
        {
            if (SpeedScale > 1)
                SpeedScale -= SpeedScaleDecay;
            Life--;
            if (Life <= 0)
                DestroyBall();
        }

        public void DestroyBall()
        {
            if (!Active)
                return;
            Active = false;
            Anim.SetBool("Death", true);
            Destroy(gameObject, 5);
        }
    }
}