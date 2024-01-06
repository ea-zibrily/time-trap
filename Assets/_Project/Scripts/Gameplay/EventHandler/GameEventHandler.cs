using System.Collections;
using UnityEngine;

namespace TimeTrap.Gameplay.EventHandler
{
    [AddComponentMenu("TimeTrap/Event/GameEventHandler")]
    public class GameEventHandler : MonoBehaviour
    {
        public delegate void GameStart();
        public static event GameStart OnGameStart;

        public delegate IEnumerator GameOver();
        public static event GameOver OnGameOver;

        #region Tsukuyomi Callbacks

        public static void GameStartEvent() => OnGameStart?.Invoke();
        public void GameOverEvent() => StartCoroutine(OnGameOver?.Invoke());

        #endregion
    }
}