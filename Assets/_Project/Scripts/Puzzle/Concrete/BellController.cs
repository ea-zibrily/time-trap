using System;
using System.Collections;
using TimeTrap.Enum;
using TimeTrap.Managers;
using UnityEngine;

namespace TimeTrap.Puzzle
{
    [AddComponentMenu("TimeTrap/Puzzle/BellController")]
    [RequireComponent(typeof(BoxCollider2D))]
    public class BellController : MonoBehaviour, IClickable
    {
        #region Variable

        [Header("Internal Data")] 
        [SerializeField] private int uniqueId;
        [SerializeField] private bool[] isAllTimeComplete;
        
        private int _currentClickCount;
        private int _timeCompleteIndex;
        private bool _isPuzzleCorrect;
        
        //--- Const
        private const string INTERACT_TRIGGER = "Interact";
        private const string INSTALLED_TRIGGER = "isInstalled";
        
        [Header("Reference")] 
        private FragmentController _fragmentController;
        private Animator _bellAnimator;

        public Animator BellAnimator => _bellAnimator;
        
        #endregion

        #region MonoBehaviour Callbacks

        private void Awake()
        {
            _fragmentController = GetComponentInChildren<FragmentController>();
            _bellAnimator = GetComponentInChildren<Animator>();
        }

        private void Start()
        {
            isAllTimeComplete ??= new bool[3];
            
            _currentClickCount = 0;
            _timeCompleteIndex = 0;
            _isPuzzleCorrect = false;
        }

        private void Update()
        {
            // _bellAnimator.SetBool(INSTALLED_TRIGGER, _fragmentController.IsFragmentInstalled);
        }

        #endregion
        
        #region Tsukuyomi Callbacks
        
        public void OnClicked()
        {
            FindObjectOfType<AudioManager>().PlayAudio(SoundEnum.SFX_Click);
            if (!_fragmentController.IsFragmentInstalled)
            {
                Debug.LogWarning("Fragment not installed yet!");
                _currentClickCount = 0;
                return;
            }
            
            _bellAnimator.SetTrigger(INTERACT_TRIGGER);
            // _fragmentController.gameObject.SetActive(false);
            _currentClickCount++;
            StartCoroutine(CheckClickTimeIndicator());
        }
        
        public bool IsCorrect() => _isPuzzleCorrect;
        
        private IEnumerator CheckClickTimeIndicator()
        {
            var timeIndicator = TimeIndicator.None;
            var maxClickCount = 0;
            
            switch (_timeCompleteIndex)
            {
                case 3:
                    maxClickCount = 3;
                    timeIndicator = TimeIndicator.Morning;
                    break;
                case 5:
                    maxClickCount = 5;
                    timeIndicator = TimeIndicator.Night;
                    break;
            }
            
            if (_currentClickCount >= maxClickCount)
            {
                yield return new WaitForSeconds(0.25f);
                // _fragmentController.gameObject.SetActive(true);
                PuzzleManager.Instance.SetIndicatorTime(timeIndicator);
                isAllTimeComplete[_timeCompleteIndex] = true;
                
                _timeCompleteIndex++;
                _isPuzzleCorrect = _timeCompleteIndex >= isAllTimeComplete.Length;
                _currentClickCount = 0;
            }
        }
        
        #endregion
    }
}