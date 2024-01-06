using System;
using UnityEngine;

namespace TimeTrap.Puzzle
{
    [AddComponentMenu("TimeTrap/Puzzle/ClockController")]
    [RequireComponent(typeof(CircleCollider2D))]
    public class ClockController : MonoBehaviour, IHoldable
    {
        #region Variable

        [Header("Internal Data")] 
        [SerializeField] private int uniqueId;

        [Header("Clock")] 
        [SerializeField] private Sprite keySprite;
        [SerializeField] private bool isCorrect;
        
        [Header("Reference")] 
        private Animator _clockAnimator;
        
        #endregion

        #region MonoBehaviour Callbacks

        private void Start()
        {
            
        }

        #endregion
        
        #region Tsukuyomi Callbacks

        public void OnHolded()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}