using UnityEngine;
using UnityEngine.Serialization;
using KevinCastejon.MoreAttributes;

namespace TimeTrap.Puzzle
{
    [AddComponentMenu("TimeTrap/Puzzle/GeneratorController")]
    [RequireComponent(typeof(BoxCollider2D))]
    public class GeneratorController : MonoBehaviour, IClickable
    {
        #region Variable

        [Header("Internal Data")] 
        [SerializeField] private int uniqueId;
        [SerializeField, ReadOnly] private bool isGeneratorChanged;
        
        [Header("Reference")] 
        private Animator _generatorAnimator;
        
        #endregion

        #region MonoBehaviour Callbacks

        private void Start()
        {
            isGeneratorChanged = true;
        }
        
        #endregion
        
        #region Tsukuyomi Callbacks
        
        public void OnClicked()
        {
            //-- Logic when generator clicked
        }
        
        #endregion
    }
}