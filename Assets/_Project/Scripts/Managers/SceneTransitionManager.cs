using UnityEngine;
using UnityEngine.SceneManagement;
using TimeTrap.Enum;
using TimeTrap.DesignPattern.Singleton;

namespace TimeTrap.Managers
{
    [AddComponentMenu("TimeTrap/Managers/SceneTransitionManager")]
    public class SceneTransitionManager : MonoSingleton<SceneTransitionManager>
    {
        #region Variable
    
        [Header("Interface")]
        [SerializeField] private RectTransform sceneFader;
    
        #endregion
    
        #region MonoBehaviour Callbacks

        private void Start()
        {
            //--- Og Fader w LeanTween
            StartFader();
        }
        
        #endregion

        #region Tsukuyomi Callbacks
        
        public void LoadAnotherScene()
        {
            FindObjectOfType<AudioManager>().PlayAudio(SoundEnum.SFX_Click);
            OpenCurrentScene();
        }
        
        private void StartFader()
        {
            sceneFader.gameObject.SetActive (true);
        
            LeanTween.alpha (sceneFader, 1, 0);
            LeanTween.alpha (sceneFader, 0, 1f).setOnComplete (() => {
                sceneFader.gameObject.SetActive (false);
            });
        }
        
        private void OpenCurrentScene()
        {
            Time.timeScale = 1;
            sceneFader.gameObject.SetActive (true);

            LeanTween.alpha(sceneFader, 0, 0);
            LeanTween.alpha (sceneFader, 1, 0.5f).setOnComplete (() => {
                Invoke ("LoadGame", 0.5f);
            });
        }
        
        private void LoadGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        #endregion

    }
}
