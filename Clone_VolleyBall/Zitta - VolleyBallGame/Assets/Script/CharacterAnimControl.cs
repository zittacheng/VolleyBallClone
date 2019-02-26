using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VBC
{
    public class CharacterAnimControl : MonoBehaviour {
        public Character C;
        public Animator Anim;
        public GameObject Base;
        public Rigidbody2D Rig;
        [Space]
        public int CurrentDirection;
        public int TargetDirection;
        public bool KeepDirection;
        public bool Overriding;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!KeepDirection)
            {
                CurrentDirection = TargetDirection;
                if (CurrentDirection > 0)
                    Base.transform.localScale = new Vector3(1, Base.transform.localScale.y, Base.transform.localScale.z);
                else if (CurrentDirection < 0)
                    Base.transform.localScale = new Vector3(-1, Base.transform.localScale.y, Base.transform.localScale.z);
            }

            if (CurrentDirection != TargetDirection)
                Anim.SetBool("ChangeDirection", true);
            else
                Anim.SetBool("ChangeDirection", false);

            if (Mathf.Abs(Rig.velocity.x) <= 0.01f)
            {
                Anim.SetBool("Front", false);
                Anim.SetBool("Back", false);
            }
            else if (Rig.velocity.x > 0)
            {
                Anim.SetBool("Front", true);
                Anim.SetBool("Back", false);
            }
            else if (Rig.velocity.x < 0)
            {
                Anim.SetBool("Front", false);
                Anim.SetBool("Back", true);
            }

            if (C.OnGround || (Mathf.Abs(Rig.velocity.y) <= 5f && !C.OnGround))
            {
                Anim.SetBool("Up", false);
                Anim.SetBool("Down", false);
            }
            else if (Rig.velocity.y > 0)
            {
                Anim.SetBool("Up", true);
                Anim.SetBool("Down", false);
            }
            else if (Rig.velocity.y < 0)
            {
                Anim.SetBool("Up", false);
                Anim.SetBool("Down", true);
            }
        }

        public void SetDirection(int Value)
        {
            SetDirection(Value, false);
        }

        public void SetDirection(int Value, bool Overrid)
        {
            if (!Overrid && Overriding)
                return;
            TargetDirection = Value;
        }

        public void JumpAnim()
        {
            Anim.SetTrigger("Jump");
        }

        public void DoubleJumpAnim()
        {
            Anim.SetTrigger("Jump");
        }

        public void TouchDownAnim()
        {
            Anim.SetTrigger("TouchDown");
        }

        public void LaunchAnim(Vector2 Direction)
        {
            StartCoroutine(LaunchAnimIE(Direction));
        }

        public IEnumerator LaunchAnimIE(Vector2 Direction)
        {
            Overriding = true;
            if (Direction.x > 0)
                SetDirection(1, true);
            else if (Direction.x < 0)
                SetDirection(-1, true);
            if (Mathf.Abs(Direction.y) <= 0.18f)
                Anim.SetInteger("Aim", 1);
            else if (Direction.y > 0)
                Anim.SetInteger("Aim", 2);
            else if (Direction.y < 0)
                Anim.SetInteger("Aim", 0);
            Anim.SetTrigger("Launch");
            yield return new WaitForSeconds(0.6f);
            Overriding = false;
        }

        public void ReplenishAnim()
        {
            Anim.SetTrigger("Replenish");
        }

        public IEnumerator StartAnim(string Key)
        {
            Anim.SetBool(Key, true);
            yield return 0;
            Anim.SetBool(Key, false);
        }
    }
}