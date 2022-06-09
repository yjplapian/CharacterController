using System.Collections;
using UnityEngine;
using System;

namespace Cassardia
{
    [Serializable]
    public class PlayerCameraSprint
    {
        private CameraController _cameraController;
        private PlayerController _controller;
        private Camera _camera;

        [Header("Wobble Tweaks")]
        [SerializeField] private AnimationCurve _rotationCurve;
        [SerializeField] private AnimationCurve _positionCurve;
        [SerializeField] private Vector2 _curveEvaluations;
        [SerializeField] private Vector2 _curveEvaluationsMultiplicators;
        [SerializeField] private Transform _wobbleTransform;

        private Vector3 _wobblePosition;
        private Vector3 _wobbleRotation;

        [Header("Camera Reset Tweaks")]
        [SerializeField] private float waitForSeconds = 3.5f;

        [Header("Field Of View Tweaks")]
        [SerializeField] private Vector2 fieldOfVisions;
        [SerializeField] private Vector2 timeMultiplication;

        public void Initialize()
        {
            _cameraController = Camera.main.GetComponentInParent<CameraController>();
            _controller = _cameraController.Player;
            _camera = Camera.main;
        }

        public void SprintingWobble()
        {
            if (_cameraController.Input.GetKeyboardInput("Sprint"))
            {
                _wobblePosition = _wobbleTransform.localPosition;
                _wobbleRotation = _wobbleTransform.localEulerAngles;

                _curveEvaluations.x += Time.deltaTime * _curveEvaluationsMultiplicators.x;
                _wobblePosition.y = _positionCurve.Evaluate(_curveEvaluations.x);

               _curveEvaluations.y += Time.deltaTime * _curveEvaluationsMultiplicators.y;
               _wobbleRotation.z = _rotationCurve.Evaluate(_curveEvaluations.y);

                _wobbleTransform.localPosition = _wobblePosition;
                _wobbleTransform.localRotation = Quaternion.Euler(_wobbleRotation);
            }

            else
            {
                if(_curveEvaluations.x != 0 || _curveEvaluations.y != 0)
                {
                    _curveEvaluations = Vector2.zero;
                    _wobbleRotation.z = 0;
                    _wobbleTransform.localRotation = Quaternion.Euler(_wobbleRotation);

                    _wobblePosition.y = 0;
                    _wobbleTransform.localPosition = _wobblePosition;
                }
            }
        }

        private void FieldOfViewState()
        {
            if (_cameraController.Input.GetKeyboardInputUp("Sprint"))
                _cameraController.ExecuteCoroutine(ResetCamera());
            
            else if (IsKeyPressed)
                _cameraController.TerminateCoroutine(ResetCamera());
        }

        private IEnumerator ResetCamera()
        {
            yield return new WaitForSeconds(waitForSeconds);
            _camera.fieldOfView = fieldOfVisions.x;
        }

        public void AdjustFOV()
        {

            if (_controller.IsSprinting)
                _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, fieldOfVisions.y, timeMultiplication.y * Time.deltaTime);

            else
                _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, fieldOfVisions.x, timeMultiplication.x * Time.deltaTime);

            FieldOfViewState();
        }

        private bool IsKeyPressed { get { return _cameraController.Input.GetKeyboardInputDown("Sprint"); } }
    }
}