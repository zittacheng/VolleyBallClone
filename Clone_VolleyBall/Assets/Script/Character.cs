using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VBC
{
    public class Character : MonoBehaviour {
        public CharacterAnimControl CAC;
        public Rigidbody2D Rig;
        [HideInInspector]
        public bool Reseting;
        [Space]
        public float Speed;
        public float TempSpeedDecay;
        public float TempSpeedX;
        [Space]
        public float JumpSpeed;
        public float MaxFallSpeed;
        public int MaxDoubleJumpCount;
        public int DoubleJumpCount;
        [Space]
        public bool OnGround;
        public List<Collider2D> C2Ds;
        public List<MovingPlatform> MPs;
        [Space]
        public bool LaunchCoolHolding;
        public float LaunchCoolRate;
        public float LaunchCoolDown;
        public Vector2 LaunchReactiveForce;
        public Vector2 MaxReactiveForce;
        public GameObject FirePoint;
        public GameObject BallObject;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            float a = 0;
            if (!Reseting)
                a = Input.GetAxis("Horizontal");
            float b = Rig.velocity.y;
            if (b < MaxFallSpeed)
                b = MaxFallSpeed;
            float t = 0;
            foreach (MovingPlatform MP in MPs)
                if (MP.Rig)
                    t += MP.Rig.velocity.x;
            Rig.velocity = new Vector2(a * Speed + TempSpeedX + t, b);

            if (Mathf.Abs(TempSpeedX) <= 0.01f)
                TempSpeedX = 0;
            else if (TempSpeedX > 0)
                TempSpeedX -= TempSpeedDecay * Time.deltaTime;
            else if (TempSpeedX < 0)
                TempSpeedX += TempSpeedDecay * Time.deltaTime;

            if (a > 0)
                CAC.SetDirection(1);
            else if (a < 0)
                CAC.SetDirection(-1);

            if (C2Ds.Count > 0 && (Mathf.Abs(Rig.velocity.y) <= 0.01f || MPs.Count > 0))
            {
                if (!OnGround)
                    TouchDown();
                OnGround = true;
            }
            else
                OnGround = false;

            if (!LaunchCoolHolding)
            {
                if (LaunchCoolDown > 0)
                    LaunchCoolDown -= Time.deltaTime;
                else
                    LaunchCoolDown = 0;
            }

            if (Input.GetButtonDown("Jump"))
            {
                if (OnGround)
                    Jump();
                else if (DoubleJumpCount > 0)
                    DoubleJump();
            }

            if (Input.GetButtonDown("Fire"))
            {
                if (LaunchCoolDown <= 0)
                    Launch();
            }
        }

        public void Jump()
        {
            AddSpeed(0, JumpSpeed);
            OnGround = false;
            CAC.JumpAnim();
        }

        public void DoubleJump()
        {
            AddSpeed(0, JumpSpeed);
            DoubleJumpCount--;
            CAC.DoubleJumpAnim();
        }

        public void TouchDown()
        {
            OnGround = true;
            DoubleJumpCount = MaxDoubleJumpCount;
            LaunchCoolDown = 0;
            LaunchCoolHolding = false;
            CAC.TouchDownAnim();
        }

        public void Launch()
        {
            GameObject G = Instantiate(BallObject);
            G.transform.position = FirePoint.transform.position;
            Vector2 D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            D -= (Vector2)FirePoint.transform.position;
            D.Normalize();
            G.GetComponent<Ball>().Launch(D);
            float X = D.x * LaunchReactiveForce.x;
            if (X > MaxReactiveForce.x)
                X = MaxReactiveForce.x;
            else if (X < -MaxReactiveForce.x)
                X = -MaxReactiveForce.x;
            float Y = D.y * LaunchReactiveForce.y;
            if (Y > MaxReactiveForce.y)
                Y = MaxReactiveForce.y;
            if (Y < -MaxReactiveForce.y)
                Y = -MaxReactiveForce.y;
            if (OnGround)
                AddSpeed(X, 0);
            else
                AddSpeed(X, Y);
            CAC.LaunchAnim(D);

            LaunchCoolDown = LaunchCoolRate;
            if (!OnGround)
                LaunchCoolHolding = true;
        }

        public void LaunchReplenish()
        {
            LaunchCoolDown = 0;
            LaunchCoolHolding = false;
            CAC.ReplenishAnim();
        }

        public void AddSpeed(float X, float Y)
        {
            TempSpeedX += X;
            if (Y != 0)
                Rig.velocity = new Vector2(Rig.velocity.x, Y);
        }

        public void CharacterReset()
        {
            if (Reseting)
                return;

            StartCoroutine("CharacterResetIE");
        }

        public IEnumerator CharacterResetIE()
        {
            Reseting = true;
            ConstellationControl.Main().CharacterReset();
            yield return new WaitForSeconds(1.5f);
            transform.position = ConstellationControl.Main().CurrentCP.CharacterPoint.transform.position;
            Rig.velocity = new Vector2(0, 0);
            LaunchCoolHolding = false;
            LaunchCoolDown = 0;
            Reseting = false;
        }

        public void OnCollisionEnter2D(Collision2D C)
        {
            C2Ds.Add(C.collider);
            if (C.collider.GetComponent<MovingPlatform>())
                MPs.Add(C.collider.GetComponent<MovingPlatform>());
        }

        public void OnCollisionExit2D(Collision2D C)
        {
            if (C2Ds.Contains(C.collider))
                C2Ds.Remove(C.collider);
            if (C.collider.GetComponent<MovingPlatform>() && MPs.Contains(C.collider.GetComponent<MovingPlatform>()))
                MPs.Remove(C.collider.GetComponent<MovingPlatform>());
        }

        public static Character Main()
        {
            return ConstellationControl.Main().MC;
        }
    }
}