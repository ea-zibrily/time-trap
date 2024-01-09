using System;
using System.Collections;
using UnityEngine;
using MoreMountains.Feedbacks;
using TimeTrap.Gameplay.EventHandler;
using TimeTrap.DesignPattern.Singleton;
using TimeTrap.Gameplay.Controller;

namespace TimeTrap.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        #region Variable

        [Header("Feedbacks")] 
        public MMFeedbacks GameWinFeedbacks;
        public MMFeedbacks GameLoseFeedbacks;

        [Header("Component")] 
        [SerializeField] private float delayTime;
        [SerializeField] private GameObject gameWinPanel;
        [SerializeField] private GameObject gameOverPanel;

        private bool _isWin;
        
        [Header("Reference")] 
        [SerializeField] private TimeController timeController;

        #endregion

        #region MonoBehaviour Callbacks

        private void OnEnable()
        {
            GameEventHandler.OnGameStart += GameStart;
            GameEventHandler.OnGameWin += GameWin;
            GameEventHandler.OnGameLose += GameLose;
        }

        private void OnDisable()
        {
            GameEventHandler.OnGameStart -= GameStart;
            GameEventHandler.OnGameWin -= GameWin;
            GameEventHandler.OnGameLose -= GameLose;
        }

        private void Start()
        {
            InitalizePlayerPrefs();
            var isWinning = PlayerPrefs.GetString("Over");
            if (isWinning == "Win")
            {
                StartCoroutine(GameWinRoutine());
            }
            else
            {
                StartCoroutine(GameLoseRoutine());
            }
        }

        #endregion

        #region Tsukuyomi Callbacks
        
        // Subscribe Event
        private void GameStart()
        {
            timeController.IsTimerStart = true;
        }
        
        private void GameWin() => StartCoroutine(GameWinEnumerator());
        private void GameLose() => StartCoroutine(GameLoseEnumerator());

        private IEnumerator GameWinEnumerator()
        {
            // GameLoseFeedbacks?.PlayFeedbacks();
            timeController.IsTimerStart = false;
            PlayerPrefs.SetString("Over", "Win");
            yield return null;
        }
        
        private IEnumerator GameLoseEnumerator()
        {
            // GameWinFeedbacks?.PlayFeedbacks();
            timeController.IsTimerStart = false;
            PlayerPrefs.SetString("Over", "Lose");
            yield return null;
        }

        private IEnumerator GameWinRoutine()
        {
            gameWinPanel.SetActive(true);
            yield return new WaitForSeconds(3f);
            gameWinPanel.SetActive(false);
        }
        
        private IEnumerator GameLoseRoutine()
        {
            gameOverPanel.SetActive(true);
            yield return new WaitForSeconds(3f);
            gameOverPanel.SetActive(false);
        }

        private void InitalizePlayerPrefs()
        {
            if (!PlayerPrefs.HasKey("Over"))
            {
                PlayerPrefs.SetString("Over", "Lose");
            }
        }

        #endregion
    }
}