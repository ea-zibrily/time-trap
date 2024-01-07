using System;
using UnityEngine;
using TimeTrap.Managers;
using KevinCastejon.MoreAttributes;
using TimeTrap.Enum;

namespace TimeTrap.Puzzle
{
    [AddComponentMenu("TimeTrap/Puzzle/FragmentController")]
    public class FragmentController : MonoBehaviour, IClickable
    {
        #region Variable
        
        [Header("Internal Data")] 
        [SerializeField] private int uniqueId;
        [SerializeField] private Vector3 defaultPosition;
        [SerializeField, ReadOnly] private bool isFragmentInstalled;

        public bool IsFragmentInstalled => isFragmentInstalled;
        
        private bool _isPuzzleCorrect;
        private const string FRAGMENT_INSTALLED = "isInstalled";

        [Header("Reference")] 
        private Rigidbody2D _fragmentRb;
        
        #endregion
        
        #region MonoBehaviour Callbacks

        private void Awake()
        {
            _fragmentRb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            isFragmentInstalled = PuzzleDataHandler.Instance.GetFragmentData(uniqueId);
            _fragmentRb.gravityScale = isFragmentInstalled ? 0 : 1;
            
            //--- NOTE: False fragment is correct puzzle
            if (!isFragmentInstalled)
            {
                _isPuzzleCorrect = true;
            }
        }
        
        #endregion

        #region Tsukuyomi Callbacks
        
        public void OnClicked()
        {
            FindObjectOfType<AudioManager>().PlayAudio(SoundEnum.SFX_Click);
            SetFragmentInstallation();
            PuzzleDataHandler.Instance.SetFragmentData(uniqueId, isFragmentInstalled);
        }
        
        public bool IsCorrect() => _isPuzzleCorrect;
        
        private void SetFragmentInstallation()
        {
            if (isFragmentInstalled)
            {
                //NOTE: Correct when fragment not installed!
                _fragmentRb.gravityScale = 1;
                isFragmentInstalled = false;
            }
            else
            {
                _fragmentRb.gravityScale = 0;
                transform.localPosition = defaultPosition;
                transform.eulerAngles = Vector3.zero;
                
                isFragmentInstalled = true;
            }
        }
        
        #endregion
    }
}