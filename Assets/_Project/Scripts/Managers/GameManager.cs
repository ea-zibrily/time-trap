using System;
using System.Collections;
using UnityEngine;
using MoreMountains.Feedbacks;
using TimeTrap.Gameplay.EventHandler;
using TimeTrap.DesignPattern.Singleton;

namespace TimeTrap.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        #region Variable

        [Header("Feedbacks")] 
        public MMFeedbacks GameOverFeedbacks;
        public MMFeedbacks TimeLimitFeedbacks;
        
        [Header("Game Over")] 
        [SerializeField] private GameObject gameOverPanel;

        #endregion

        #region MonoBehaviour Callbacks

        private void OnEnable()
        {
            GameEventHandler.OnGameStart += GameStart;
            GameEventHandler.OnGameOver += GameOver;
        }

        private void OnDisable()
        {
            GameEventHandler.OnGameStart -= GameStart;
            GameEventHandler.OnGameOver -= GameOver;
        }

        #endregion

        #region Tsukuyomi Callbacks
        
        // Subscribe Event
        private void GameStart()
        {
            
        }

        private IEnumerator GameOver()
        {
            yield return null;
        }

        #endregion
    }
}