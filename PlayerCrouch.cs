using System.Collections;
using UnityEngine;
using System;

namespace Cassardia
{
    [Serializable]
    public class PlayerCrouch
    {
        [Header("Crouch Settings")]
        [SerializeField] private float _crouchHeight;
        [SerializeField] private float _smoothTimeCrouching = 0.3f;
        [SerializeField] private float _smoothTimeStanding = 0.3f;
        [SerializeField] private Transform _cameraTransform;

        private PlayerController _controller;
        private Vector3 _velocity = Vector3.zero;
        private Vector3 _storedStandingHeight;
        private Vector3 _crouchPosition;

        public void Initialize()
        {
            _controller = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            _cameraTransform = Camera.main.transform;
        }

        public IEnumerator Crouch()
        {
            _controller.IsCrouched = !_controller.IsCrouched;

            if (_controller.IsCrouched)
            {
                _crouchPosition = _cameraTransform.localPosition;
                _storedStandingHeight = _crouchPosition;
                _crouchPosition.y = _crouchHeight;

                _controller.onUpdateEvent += TransitionPosition;
                yield return new WaitUntil(() => _cameraTransform.localPosition == _crouchPosition);
                _controller.onUpdateEvent -= TransitionPosition;
            }

            else
            {
                _controller.onUpdateEvent += TransitionPosition;
                yield return new WaitUntil(() => _cameraTransform.localPosition == _storedStandingHeight);
                _controller.onUpdateEvent -= TransitionPosition;
            }
        }

        private void TransitionPosition()
        {
            if (_controller.IsCrouched)
                _cameraTransform.localPosition = Vector3.SmoothDamp(_cameraTransform.localPosition, _crouchPosition, ref _velocity, _smoothTimeCrouching);

            else
                _cameraTransform.localPosition = Vector3.SmoothDamp(_cameraTransform.localPosition, _storedStandingHeight, ref _velocity, _smoothTimeStanding);
        }
    }
}