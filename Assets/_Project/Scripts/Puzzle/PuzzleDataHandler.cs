using System;
using UnityEngine;
using TimeTrap.Managers;
using TimeTrap.DesignPattern.Singleton;

namespace TimeTrap.Puzzle
{
    public class PuzzleDataHandler : MonoDDOL<PuzzleDataHandler>
    {
        #region Variable

        [Header("Fragment Data")]
        [Tooltip("Data untuk Fragment/Main Puzzle")]
        public bool[] IsFragmentInstall;
        
        // [Header("Engine and Clock Data")]
        // [Tooltip("Data untuk Fragment/Main Puzzle")]
        // public bool[] IsFragmentInstall;
        
        #endregion

        #region MonoBehaviour Callbacks

        private void OnEnable()
        {
            InitializeFragmentData();
        }

        #endregion
        
        #region Puzzle Data
        
        public void SetFragmentData(int index, bool value) => IsFragmentInstall[index - 1] = value;
        public bool GetFragmentData(int index) => IsFragmentInstall[index - 1];

        public bool FragmentPuzzleCheck()
        {
            var completePuzzleCount = ValueCounter();
            return completePuzzleCount >= IsFragmentInstall.Length;
        }
        
        private void InitializeFragmentData()
        {
            var fragmentCount = FindObjectsOfType<FragmentController>().Length;
            IsFragmentInstall ??= new bool[fragmentCount];
            
            for (var i = 0; i < IsFragmentInstall.Length; i++)
            {
                IsFragmentInstall[i] = true;
            }
        }

        private int ValueCounter()
        {
            var count = 0;
            foreach (var value in IsFragmentInstall)
            {
                if (value) count++;
            }
            
            return count;
        }
        
        #endregion
    }
}