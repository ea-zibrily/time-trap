using System;
using TimeTrap.Gameplay.EventHandler;
using TMPro;
using UnityEngine;

namespace TimeTrap.Managers
{
    public class ManualManager : MonoBehaviour
    {
        [SerializeField] private GameObject manualObject;

        private void Start()
        {
            manualObject.SetActive(true);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                manualObject.SetActive(false);
                GameEventHandler.GameStartEvent();
            }
        }
    }
}