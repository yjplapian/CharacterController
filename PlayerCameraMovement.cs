using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Cassardia
{
    [Serializable]
    public class PlayerCameraMovement
    {
        [Header("Camera Rotation Tweaks")]
        [SerializeField] private Vector2 _clamp;
        [SerializeField] private float _sensitivity = 1f;

        private Transform _camera;
        private Transform _transform;
        private Vector2 _rotations;

        public float Sensitivity { get { return _sensitivity; } set { _sensitivity = value; } }

        public void Initialize()
        {
            _camera = Camera.main.transform;
            _transform = GameObject.FindWithTag("Player").transform;
        }

        public void Rotate()
        {
            _rotations.x -= Input.GetAxis("Mouse Y") * _sensitivity;
            _rotations.y += Input.GetAxis("Mouse X") * _sensitivity;

            _rotations.x = Mathf.Clamp(_rotations.x, _clamp.x, _clamp.y);

            _camera.localRotation = Quaternion.Euler(_rotations.x, 0, 0);
            _transform.rotation = Quaternion.Euler(0, _rotations.y, 0);
        }
    }
}