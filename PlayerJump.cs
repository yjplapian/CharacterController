using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Cassardia
{
    [Serializable]
    public class PlayerJump
    {
        private Transform _transform;
        private InputManager _input;
        private PlayerController _controller;
        private Rigidbody _rigidbody;

        [Header("Ground Detection Tweaks")]
        [SerializeField] private float _groundCheckDistance;
        [SerializeField] private LayerMask _ignoredLayers;
        [SerializeField] private Vector3 _jumpForce;

        private bool _isOnGround = true;
        public bool IsOnGround { get { return _isOnGround; } }

        public void Initialize()
        {
            _controller = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            _input = _controller.Input;
            _transform = _controller.transform;

            _rigidbody = _controller.GetComponent<Rigidbody>();
        }

        public void StartJump()
        {
            if (_input.GetKeyboardInputDown("Jump") && _isOnGround)
            {
                _rigidbody.AddForce(_jumpForce);
                _isOnGround = false;
                _controller.CallCoroutine(Jump());
            }
        }

        private IEnumerator Jump()
        {
            yield return new WaitUntil(() => Physics.Raycast(_transform.position, -_transform.up, out RaycastHit hit, _groundCheckDistance, ~_ignoredLayers));
            _isOnGround = true;
        }

        public bool EnableDoubleJump { get; set; }
    }
}