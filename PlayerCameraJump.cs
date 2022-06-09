using System.Collections;
using UnityEngine;
using System;

namespace Cassardia
{
    [Serializable]
    public class PlayerCameraJump
    {
        private CameraController _cameraController;
        private PlayerController _controller;

        [Header("Jump Effect Tweaks")]
        [SerializeField] private Transform _transform;
        [SerializeField] private AnimationCurve _positionCurve;
        [SerializeField] private AnimationCurve _rotationCurve;

        [SerializeField] private Vector2 _evaluationTimes;
        [SerializeField] private Vector2 _evaluationTimesMultipliers;


        private Vector3 _position;
        private Vector3 _rotation;

        [Header("Reset Camera")]
        [SerializeField] private float _waitForSeconds = 1f;

        public void Initialize()
        {
            _controller = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            _cameraController = _controller.GetComponentInChildren<CameraController>();
        }

        public void StartJumpEffect()
        {
            if (_cameraController.Input.GetKeyboardInputDown("Jump") && _controller.IsOnGround)
            {
                _cameraController.onUpdateCallback += JumpEffect;
                _cameraController.ExecuteCoroutine(ResetCamera());
            }
        }

        private void JumpEffect()
        {
            _position = _transform.localPosition;
            _rotation = _transform.localEulerAngles;

            _evaluationTimes.x += Time.deltaTime * _evaluationTimesMultipliers.x;
            _position.y = _positionCurve.Evaluate(_evaluationTimes.x);

            _evaluationTimes.y += Time.deltaTime * _evaluationTimesMultipliers.y;
            _rotation.x = _rotationCurve.Evaluate(_evaluationTimes.y);

            _transform.localPosition = _position;
            _transform.localEulerAngles = _rotation;
        }

        private IEnumerator ResetCamera()
        {
            yield return new WaitForSeconds(_waitForSeconds);

            _cameraController.onUpdateCallback -= JumpEffect;

            _position.y = 0;
            _rotation.x = 0;
            _evaluationTimes = Vector2.zero;
            _transform.localPosition = _position;
            _transform.localEulerAngles = _rotation;
        }
    }
}