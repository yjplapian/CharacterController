using System.Collections;
using UnityEngine;
using System;

namespace Cassardia
{
    [Serializable]
    public class PlayerCameraCrouch
    {
        private CameraController _cameraController;
        private PlayerController _controller;

        [Header("Crouch Effect Tweaks")]
        [SerializeField] private Transform _transform;
        [SerializeField] private AnimationCurve _positionCurve;
        [SerializeField] private float _positionCurveEvaluationMultiplier = 1f;
        [SerializeField] private float _waitForSeconds = 1f;

        private float _positionCurveEvaluation;
        private Vector3 _position;

        public void Initialize()
        {
            _controller = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            _cameraController = _controller.GetComponentInChildren<CameraController>();
        }

        public void Crouch()
        {
            if (_cameraController.Input.GetKeyboardInputDown("Crouch") && !_controller.IsCrouched)
            {
                _cameraController.onUpdateCallback += CameraEffect;
                _cameraController.ExecuteCoroutine(ResetCamera());
            }

            else if(_cameraController.Input.GetKeyboardInputDown("Crouch") && _controller.IsCrouched)
                ResetCurve();
        }

        private void CameraEffect()
        {
            _position = _transform.localPosition;

            _positionCurveEvaluation += Time.deltaTime * _positionCurveEvaluationMultiplier;
            _position.y = _positionCurve.Evaluate(_positionCurveEvaluation);

            _transform.localPosition = _position;
        }

        private IEnumerator ResetCamera()
        {
            yield return new WaitForSeconds(_waitForSeconds);

            _cameraController.onUpdateCallback -= CameraEffect;
        }

        private void ResetCurve()
        {
            _position.y = 0;
            _transform.localPosition = _position;
            _positionCurveEvaluation = 0;
        }
    }
}