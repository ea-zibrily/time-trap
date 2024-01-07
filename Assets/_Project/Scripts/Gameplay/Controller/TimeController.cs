using System;
using UnityEngine;
using TMPro;
using KevinCastejon.MoreAttributes;
using TimeTrap.Gameplay.EventHandler;
using TimeTrap.Puzzle;

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
        
        public bool IsTimerStart { get; set; }
        public float CurrentTime { get => currentTime; }
        
        [Header("User Interface")]
        [SerializeField] private TextMeshProUGUI timerTextUI;

        #endregion

        #region MonoBehaviour Callbacks

        private void Start()
        {
            currentTime = amountOfTime;
            timerTextUI.text = "02:00";
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
            if (!IsTimerStart)
            {
                return;
            }

            currentTime -= Time.deltaTime;
            DisplayTime(currentTime);
            if (currentTime <= 0f)
            {
                currentTime = 0;
                IsTimerStart = false;
                
                if (PuzzleDataHandler.Instance.FragmentPuzzleCheck())
                    GameEventHandler.GameWinEvent();
                else
                    GameEventHandler.GameLoseEvent();
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