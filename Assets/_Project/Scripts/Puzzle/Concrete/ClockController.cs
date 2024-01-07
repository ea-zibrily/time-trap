using System;
using System.Collections;
using TimeTrap.Enum;
using TimeTrap.Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace TimeTrap.Puzzle
{
    [AddComponentMenu("TimeTrap/Puzzle/ClockController")]
    [RequireComponent(typeof(CircleCollider2D))]
    public class ClockController : MonoBehaviour, IClickable
    {
        #region Variable

        [Header("Internal Data")] 
        [SerializeField] private int uniqueId;
        [SerializeField] private Sprite[] allClockSprite;
        [SerializeField] private Sprite keySpriteNight;
        [SerializeField] private Sprite keySpriteMorning;
        [SerializeField] private bool[] isAllTimeComplete;
        
        private bool _isPuzzleCorrect;
        private int _timeCompleteIndex;
        private int _clockSpriteIndex;
        private int _clockIteration;
        
        //--- Const
        private const string ACTIVATE_TRIGGER = "Activate";
        
        [Header("Reference")] 
        [SerializeField] private SpriteRenderer _clockSpriteRenderer;
        private Animator _clockAnimator;
        
        #endregion

        #region MonoBehaviour Callbacks

        private void Awake()
        {
            _clockAnimator = GetComponentInChildren<Animator>();
        }

        private void Start()
        {
            isAllTimeComplete ??= new bool[2];
            
            _clockSpriteIndex = 1;
            _timeCompleteIndex = 0;
            _clockIteration = 0;
            _isPuzzleCorrect = false;
        }

        #endregion
        
        #region Tsukuyomi Callbacks

        public void OnClicked()
        {
            FindObjectOfType<AudioManager>().PlayAudio(SoundEnum.SFX_Click);
            ChangeClock();
            CheckClickTimeIndicator();
        }

        public bool IsCorrect() => _isPuzzleCorrect;

        private void ChangeClock()
        {
            _clockSpriteRenderer.sprite = allClockSprite[_clockSpriteIndex];
            _clockSpriteIndex++;
            if (_clockSpriteIndex >= allClockSprite.Length)
            {
                _clockIteration++;
                _clockSpriteIndex = 0;
            }
        }

        private void CheckClickTimeIndicator()
        {
            var currentSprite = _clockSpriteRenderer.sprite;
            if (_clockIteration is 0 && currentSprite == keySpriteNight)
            {
                PuzzleManager.Instance.SetIndicatorTime(TimeIndicator.Night);
                isAllTimeComplete[_timeCompleteIndex] = true;
                _timeCompleteIndex++;
            }
            
            if (_clockIteration is 1 && currentSprite == keySpriteMorning)
            {
                PuzzleManager.Instance.SetIndicatorTime(TimeIndicator.Morning);
                isAllTimeComplete[_timeCompleteIndex] = true;
                _timeCompleteIndex++;
            }
            
            _isPuzzleCorrect = _timeCompleteIndex >= isAllTimeComplete.Length;
        }

        #endregion
    }
}