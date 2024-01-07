﻿using System;
using UnityEngine;
using TimeTrap.Enum;
using TimeTrap.Puzzle;
using TimeTrap.Managers;

namespace TimeTrap.Gameplay.Controller
{
    [AddComponentMenu("TimeTrap/Controller/CursorController")]
    public class CursorController : MonoBehaviour
    {
        #region Variable

        private Ray _rayCursor;
        private Camera _mainCamera;

        #endregion
        
        #region MonoBehaviour Callbacks

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            HandleMouseInput();
        }

        #endregion

        #region Tsukuyomi Callbacks
        
        private void HandleMouseInput()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    ClickObject();
                }
                else
                {
                    HoldObject();
                }
            }
        }
        
        private void ClickObject()
        {
            _rayCursor = _mainCamera.ScreenPointToRay(Input.mousePosition);
            
            //--- Alloc Intersection
            RaycastHit2D[] hit2DNonAlloc = new RaycastHit2D[1];
            var numOfHits = Physics2D.GetRayIntersectionNonAlloc(_rayCursor, hit2DNonAlloc);
            foreach (var rayHit in hit2DNonAlloc)
            {
                if (rayHit.collider == null) return;
                if (rayHit.collider.CompareTag("ClickPuzzle"))
                {
                    var rayHitInterface = rayHit.collider.GetComponent<IClickable>();
                    if (rayHitInterface.IsCorrect()) return;
                    rayHitInterface.OnClicked();
                }
            }
        }
        
        private void HoldObject()
        {
            _rayCursor = _mainCamera.ScreenPointToRay(Input.mousePosition);
            
            //--- Alloc Intersection
            RaycastHit2D[] hit2DNonAlloc = new RaycastHit2D[1];
            var numOfHits = Physics2D.GetRayIntersectionNonAlloc(_rayCursor, hit2DNonAlloc);
            foreach (var rayHit in hit2DNonAlloc)
            {
                if (rayHit.collider == null) return;
                if (rayHit.collider.CompareTag("HoldPuzzle"))
                {
                    var rayHitInterface = rayHit.collider.GetComponent<IHoldable>();
                    if (rayHitInterface.IsCorrect()) return;
                    rayHitInterface.OnHolded();
                }
            }
        }
        
        #endregion
    }
}