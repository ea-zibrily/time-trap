using System;
using UnityEngine;
using TMPro;
using KevinCastejon.MoreAttributes;
using TimeTrap.Gameplay.EventHandler;

namespace TimeTrap.Gameplay.Controller
{
    [AddComponentMenu("TimeTrap/Controller/TimeController")]
    public class TimeController : MonoBehaviour
    {
        #region Variable

        [Header("Timer")]
        [Tooltip("Isi variable ini dengan total waktu dalam jumlah detik")]
        [SerializeField] private int amountOfTime;
        [SerializeField, ReadOnly] private float currentTime;
        [SerializeField] private bool isTimerStart;
        
        public float CurrentTime { get => currentTime; }
        
        [Header("User Interface")]
        [SerializeField] private TextMeshProUGUI timerTextUI;

        [Header("Reference")] 
        private GameEventHandler _gameEventHandler;

        #endregion

        #region MonoBehaviour Callbacks

        private void Awake()
        {
            _gameEventHandler = gameObject.AddComponent<GameEventHandler>();
        }

        private void Start()
        {
            currentTime = amountOfTime;
            timerTextUI.text = "00:00";
        }

        private void Update()
        {
            //--- NOTE: Set Countdown method here
            CountdownTimer();
        }

        #endregion

        #region Tsukuyomi Callbacks

        private void CountdownTimer()
        {
            if (!isTimerStart)
            {
                return;
            }

            currentTime -= Time.deltaTime;
            DisplayTime(currentTime);
            if (currentTime <= 0f)
            {
                currentTime = 0;
                isTimerStart = false;
                _gameEventHandler.GameOverEvent();
            }
        }
        
        private void DisplayTime(float timeToDisplay)
        {
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);
            
            timerTextUI.text = string.Format("{00:00}:{01:00}", minutes, seconds);
        }
        
        #endregion
    }
}