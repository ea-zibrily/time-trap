using System;
using UnityEngine;
using TimeTrap.Enum;
using TimeTrap.Puzzle;
using TimeTrap.DesignPattern.Singleton;

namespace TimeTrap.Managers
{
    [AddComponentMenu("TimeTrap/Managers/PuzzleManager")]
    public class PuzzleManager : MonoSingleton<PuzzleManager>
    {
        #region Variable
        
        [Header("Interface")] 
        [SerializeField] private GameObject[] timeIndicatorUI;
        
        #endregion

        #region MonoBehaviour Callbacks

        private void Start()
        {
            SetDefaultIndicator();
        }

        #endregion

        #region Tsukuyomi Callbacks

        private void SetDefaultIndicator()
        {
            foreach (var indicatorUI in timeIndicatorUI)
            {
                indicatorUI.SetActive(false);
            }
        }

        public void SetIndicatorTime(TimeIndicator indicator)
        {
            var indicatorIndex = indicator switch
            {
                TimeIndicator.Morning => 0,
                TimeIndicator.Night => 1,
                _ => throw new NotImplementedException()
            };
            
            for (int i = 0; i < timeIndicatorUI.Length; i++)
            {
                timeIndicatorUI[i].SetActive(i == indicatorIndex);
            }
        }

        #endregion
    }
}