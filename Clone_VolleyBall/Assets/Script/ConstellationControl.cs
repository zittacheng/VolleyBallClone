using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Zittacheng @ZWS

namespace VBC
{
    public class ConstellationControl : MonoBehaviour {
        public Character MC;
        public Animator ResetEffect;
        public Animator EndAnim;
        public string CurrentSceneName;
        public CheckPoint CurrentCP;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
        }

        public void CharacterReset()
        {
            ResetEffect.SetTrigger("Play");
        }

        public void SetCheckPoint(CheckPoint CP)
        {
            CurrentCP = CP;
        }

        public void End()
        {
            StartCoroutine("EndIE");
        }
        public IEnumerator EndIE()
        {
            Character.Main().StopCoroutine("CharacterResetIE");
            Character.Main().Reseting = true;
            yield return new WaitForSeconds(2.5f);
            EndAnim.SetTrigger("Play");
            yield return new WaitForSeconds(5f);
            while (true)
            {
                if (Input.anyKeyDown)
                    UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(CurrentSceneName);
                yield return 0;
            }
        }

        public static ConstellationControl Main()
        {
            return Camera.main.GetComponent<ConstellationControl>();
        }
    }
}