using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VBC
{
    public class Elevator : MonoBehaviour {
        public Rigidbody2D Rig;
        public Animator Anim;
        public List<ElevatorDirection> EDs;
        public List<float> MovementLimits;
        public float Speed;
        public float WaitTime;
        public bool StartActive;

        // Start is called before the first frame update
        void Start()
        {
            if (StartActive)
                Active();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Active()
        {
            StartCoroutine("Process");
        }

        public IEnumerator Process()
        {
            if (Anim)
                Anim.SetBool("Active", true);
            int i = 0;
            while (true)
            {
                yield return new WaitForSeconds(WaitTime);
                if (EDs[i] == ElevatorDirection.Up)
                {
                    while (transform.position.y < MovementLimits[i])
                    {
                        Rig.velocity = new Vector2(0, Speed);
                        yield return 0;
                    }
                    Rig.velocity = new Vector2(0, 0);
                    transform.position = new Vector3(transform.position.x, MovementLimits[i], transform.position.z);
                }
                else if (EDs[i] == ElevatorDirection.Down)
                {
                    while (transform.position.y > MovementLimits[i])
                    {
                        Rig.velocity = new Vector2(0, -Speed);
                        yield return 0;
                    }
                    Rig.velocity = new Vector2(0, 0);
                    transform.position = new Vector3(transform.position.x, MovementLimits[i], transform.position.z);
                }
                else if (EDs[i] == ElevatorDirection.Left)
                {
                    while (transform.position.x > MovementLimits[i])
                    {
                        Rig.velocity = new Vector2(-Speed, 0);
                        yield return 0;
                    }
                    Rig.velocity = new Vector2(0, 0);
                    transform.position = new Vector3(MovementLimits[i], transform.position.y, transform.position.z);
                }
                else if (EDs[i] == ElevatorDirection.Right)
                {
                    while (transform.position.x < MovementLimits[i])
                    {
                        Rig.velocity = new Vector2(Speed, 0);
                        yield return 0;
                    }
                    Rig.velocity = new Vector2(0, 0);
                    transform.position = new Vector3(MovementLimits[i], transform.position.y, transform.position.z);
                }

                i++;
                if (i >= EDs.Count)
                    i = 0;
                yield return 0;
            }
        }
    }

    public enum ElevatorDirection
    {
        Up,
        Down,
        Left,
        Right
    }
}