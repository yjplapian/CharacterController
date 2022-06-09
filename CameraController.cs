using System.Collections;
using UnityEngine;

namespace Cassardia
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private PlayerCameraMovement cameraMovement;
        [SerializeField] private PlayerCameraSprint cameraSprintEffect;
        [SerializeField] private PlayerCameraJump jumpEffect;
        [SerializeField] private PlayerCameraCrouch crouchEffect;

        public delegate void OnUpdateCallback();
        public OnUpdateCallback onUpdateCallback;

        private PlayerController _controller;
        public PlayerController Player { get { return _controller; } }

        private InputManager _input;
        public InputManager Input { get { return _input; } }

        private void Awake()
        {
            _controller = GetComponentInParent<PlayerController>();
            _input = GameObject.FindWithTag("GameController").GetComponent<InputManager>();

            cameraMovement.Initialize();
            cameraSprintEffect.Initialize();
            jumpEffect.Initialize();
            crouchEffect.Initialize();
        }

        private void Update()
        {
            onUpdateCallback?.Invoke();

            cameraMovement.Rotate();
            cameraSprintEffect.SprintingWobble();
            cameraSprintEffect.AdjustFOV();
            jumpEffect.StartJumpEffect();
            crouchEffect.Crouch();
        }

        public void ExecuteCoroutine(IEnumerator task) => StartCoroutine(task);
        public void TerminateCoroutine(IEnumerator task) => StopCoroutine(task);

    }
}