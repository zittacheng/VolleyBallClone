using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VBC
{
    public class VolleyBallControl : MonoBehaviour {
        public Ball MainBall;
        public List<int> Scores;
        public List<Text> TEXTs;
        public int MaxScore;
        public int CurrentBallSide;
        public int CurrentCatchCount;
        public int MaxCatchCount;
        public List<GameObject> SpawnPoints;
        public bool AlreadyWon;
        public Animator WinAnim;
        public Text WinText;
        public string CurrentSceneName;

        public void Awake()
        {

        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < TEXTs.Count; i++)
                TEXTs[i].text = Scores[i].ToString();
        }

        public void OnCatch()
        {
            CurrentCatchCount++;
            if (CurrentCatchCount >= MaxCatchCount)
            {
                if (CurrentBallSide == 0)
                    Score(1);
                else
                    Score(0);
            }
        }

        public void OnSwitchSide(int Side)
        {
            if (CurrentBallSide == Side)
                return;

            CurrentBallSide = Side;
            CurrentCatchCount = 0;
        }

        public void Score(int Side)
        {
            StartCoroutine(ScoreIE(Side));
        }

        public IEnumerator ScoreIE(int Side)
        {
            MainBall.Anim.SetBool("Play", true);
            yield return 0;
            MainBall.Anim.SetBool("Play", false);
            yield return new WaitForSeconds(0.25f);
            Scores[Side]++;
            CheckWinCondition();
            yield return new WaitForSeconds(0.35f);
            CurrentBallSide = Side;
            CurrentCatchCount = 0;
            MainBall.SpeedX = 0;
            MainBall.SpeedY = 0;
            MainBall.SpeedScale = 1;
            MainBall.transform.position = SpawnPoints[Side].transform.position;
        }

        public void CheckWinCondition()
        {
            if (AlreadyWon)
                return;
            if (Scores[0] >= MaxScore)
            {
                WinText.text = "The Left Player Wins!";
            }
            else if (Scores[1] >= MaxScore)
            {
                WinText.text = "The Right Player Wins!";
            }
            else
                return;
            AlreadyWon = true;
            StartCoroutine("Win");
        }

        public IEnumerator Win()
        {
            WinAnim.SetBool("Play", true);
            yield return new WaitForSeconds(1);
            while (true)
            {
                if (Input.anyKeyDown)
                    UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(CurrentSceneName);
                yield return 0;
            }
        }

        public static VolleyBallControl Main()
        {
            return Camera.main.GetComponent<VolleyBallControl>();
        }
    }
}