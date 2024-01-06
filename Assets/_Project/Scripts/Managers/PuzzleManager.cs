using System;
using UnityEngine;
using TimeTrap.Puzzle;
using TimeTrap.DesignPattern.Singleton;

namespace TimeTrap.Managers
{
    [AddComponentMenu("TimeTrap/Managers/PuzzleManager")]
    public class PuzzleManager : MonoDDOL<PuzzleManager>
    {
        #region Variable

        [Header("Puzzle Data")] 
        public bool[] IsFragmentInstall;
        
        #endregion

        #region MonoBehaviour Callbacks

        private void Start()
        {
            InitializeFragmentData();
        }
        
        #endregion
        
        #region Tsukuyomi Callbacks
        
        private void InitializeFragmentData()
        {
            if (IsFragmentInstall != null) return;
            IsFragmentInstall = new bool[4];
            
            for (var i = 0; i < IsFragmentInstall.Length; i++)
            {
                IsFragmentInstall[i] = true;
            }
        }
        
        public void SetFragmentData(int index, bool value) => IsFragmentInstall[index - 1] = value;
        
        #endregion
    }
}