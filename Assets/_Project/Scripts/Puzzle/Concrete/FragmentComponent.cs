using System;
using UnityEngine;
using TimeTrap.Managers;
using KevinCastejon.MoreAttributes;

namespace TimeTrap.Puzzle
{
    [AddComponentMenu("TimeTrap/Puzzle/FragmentComponent")]
    [RequireComponent(typeof(BoxCollider2D))]
    public class FragmentComponent : MonoBehaviour, IClickable
    {
        #region Variable
        
        [Header("Internal Data")] 
        [SerializeField] private int uniqueId;
        [SerializeField, ReadOnly] private bool isFragmentInstalled;
        
        //--- Constant variable
        private const string IS_FRAGMENT_INSTALLED = "isInstalled";
        
        [Header("Reference")] 
        private Animator _fragmentAnimator;

        #endregion
        
        #region MonoBehaviour Callbacks

        private void Awake()
        {
            _fragmentAnimator = GetComponentInChildren<Animator>();
        }

        private void Start()
        {
            isFragmentInstalled = PuzzleManager.Instance.IsFragmentInstall[uniqueId];
            _fragmentAnimator.SetBool(IS_FRAGMENT_INSTALLED, isFragmentInstalled);
        }
        
        #endregion

        #region Tsukuyomi Callbacks
        
        public void OnClicked()
        {
            if (isFragmentInstalled)
            {
                //NOTE: Correct when fragment not installed!
                _fragmentAnimator.SetBool(IS_FRAGMENT_INSTALLED, false);
                isFragmentInstalled = false;
                PuzzleManager.Instance.SetFragmentData(uniqueId, isFragmentInstalled);
            }
            else
            {
                _fragmentAnimator.SetBool(IS_FRAGMENT_INSTALLED, true);
                isFragmentInstalled = true;
            }
            
        }
        
        #endregion
    }
}