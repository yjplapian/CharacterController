using UnityEngine;
using System;

namespace Cassardia
{
    [Serializable]
    public class PlayerMovement
    {
        private PlayerController _controller;
        private Transform _transform;
        private InputManager _input;

        [Header("Velocity Tweaks")]
        [SerializeField] private float _walkVelocity = 1f;
        [SerializeField] private float _runVelocity = 4.5f;
        [SerializeField] private float _sprintVelocity = 7f;
        [SerializeField] private float _crouchVelocityPenalty = 0.75f;

        private float _currentVelocity;

        public void Initialize()
        {
            _controller = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            _transform = _controller.transform;
            _input = _controller.Input;
        }

        private void SetVelocities()
        {
            if (_controller.IsSprinting)
                _currentVelocity = _sprintVelocity;

            else if (_controller.IsWalking && !_controller.IsSprinting)
                _currentVelocity = _walkVelocity;

            else if (_controller.IsWalking && _controller.IsCrouched)
                _currentVelocity = _walkVelocity * _crouchVelocityPenalty;

            else if (!_controller.IsSprinting && !_controller.IsWalking)
                _currentVelocity = _runVelocity;

            else if (!_controller.IsSprinting && !_controller.IsWalking && _controller.IsCrouched)
                _currentVelocity = _runVelocity * _crouchVelocityPenalty;

            else
                _currentVelocity = 0;
        }

        public void MoveCharacter()
        {
            SetVelocities();

            if (IsMovingForward)
                _transform.Translate(Vector3.forward * (_currentVelocity * Time.deltaTime));
            
            if(IsMovingLeft)
                _transform.Translate(Vector3.left * (_currentVelocity * Time.deltaTime));

            if (IsMovingBackward)
                _transform.Translate(Vector3.back * (_currentVelocity * Time.deltaTime));

            if (IsMovingRight)
                _transform.Translate(Vector3.right * (_currentVelocity * Time.deltaTime));

            if (IsMovingForwardAndLeft)
                _transform.Translate((Vector3.forward + Vector3.left) * (_currentVelocity * Time.deltaTime));

            if (IsMovingForwardAndRight)
                _transform.Translate((Vector3.forward + Vector3.right) * (_currentVelocity * Time.deltaTime));

            if (IsMovingBackwardAndLeft)
                _transform.Translate((Vector3.back + Vector3.left) * (_currentVelocity * Time.deltaTime));

            if (IsMovingBackwardAndRight)
                _transform.Translate((Vector3.back + Vector3.right) * (_currentVelocity * Time.deltaTime));
        }

        public bool IsMovingForward { get { return _input.GetKeyboardInput("Forward") && !_input.GetKeyboardInput("Backward") && !_input.GetKeyboardInput("Left") && !_input.GetKeyboardInput("Right"); } }
        public bool IsMovingBackward { get { return !_input.GetKeyboardInput("Forward") && _input.GetKeyboardInput("Backward") && !_input.GetKeyboardInput("Left") && !_input.GetKeyboardInput("Right"); } }
        public bool IsMovingLeft { get { return !_input.GetKeyboardInput("Forward") && !_input.GetKeyboardInput("Backward") && _input.GetKeyboardInput("Left") && !_input.GetKeyboardInput("Right"); } }
        public bool IsMovingRight { get { return !_input.GetKeyboardInput("Forward") && !_input.GetKeyboardInput("Backward") && !_input.GetKeyboardInput("Left") && _input.GetKeyboardInput("Right"); } }

        public bool IsMovingForwardAndLeft { get { return _input.GetKeyboardInput("Forward") && !_input.GetKeyboardInput("Backward") && _input.GetKeyboardInput("Left") && !_input.GetKeyboardInput("Right"); } }
        public bool IsMovingForwardAndRight{ get { return _input.GetKeyboardInput("Forward") && !_input.GetKeyboardInput("Backward") && !_input.GetKeyboardInput("Left") && _input.GetKeyboardInput("Right"); } }
        public bool IsMovingBackwardAndLeft { get { return !_input.GetKeyboardInput("Forward") && _input.GetKeyboardInput("Backward") && _input.GetKeyboardInput("Left") && !_input.GetKeyboardInput("Right"); } }
        public bool IsMovingBackwardAndRight { get { return !_input.GetKeyboardInput("Forward") && _input.GetKeyboardInput("Backward") && !_input.GetKeyboardInput("Left") && _input.GetKeyboardInput("Right"); } }
    }
}