using System.Collections;
using UnityEngine;

namespace TimeTrap.Gameplay.EventHandler
{
    [AddComponentMenu("TimeTrap/Event/GameEventHandler")]
    public class GameEventHandler : MonoBehaviour
    {
        public delegate void GameStart();
        public static event GameStart OnGameStart;

        public delegate void GameWin();
        public static event GameWin OnGameWin;

        public delegate void GameLose();
        public static event GameLose OnGameLose;

        #region Tsukuyomi Callbacks

        public static void GameStartEvent() => OnGameStart?.Invoke();
        public static void GameWinEvent() => OnGameWin?.Invoke();
        public static void GameLoseEvent() => OnGameLose?.Invoke();

        #endregion
    }
}