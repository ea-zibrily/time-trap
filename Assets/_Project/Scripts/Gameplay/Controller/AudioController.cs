using UnityEngine;
using TimeTrap.Enum;
using TimeTrap.Managers;

namespace BelumProduktif.Gameplay.Controller
{
    [AddComponentMenu("TimeTrap/Controller/AudioController")]
    public class AudioController : MonoBehaviour
    {
        #region Variable

        [Header("Enum")]
        public SoundEnum playSoundEnum;
    
        [Header("Reference")]
        private AudioManager _audioManager;
    
        #endregion
    
        #region MonoBehaviour Callbacks

        private void Awake()
        {
            _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        }
        
        private void Start()
        {
            _audioManager.PlayAudio(playSoundEnum);
        }

        #endregion
    }
}
