using System.Collections;
using UnityEngine;

namespace Cassardia
{
    public class PlayerController : MonoBehaviour
    {
        public Inventory inventory;

        [SerializeField] private PlayerMovement movement;
        [SerializeField] private PlayerInteraction interaction;
        [SerializeField] private PlayerCrouch crouch;
        [SerializeField] private PlayerJump jump;

        private InputManager _input;

        public delegate void OnUpdateCallback();
        public OnUpdateCallback onUpdateEvent;

        private void Awake()
        {
            _input = GameObject.FindWithTag("GameController").GetComponent<InputManager>();

            movement.Initialize();
            interaction.Initialize();
            crouch.Initialize();
            jump.Initialize();
        }

        private void Update()
        {
            onUpdateEvent?.Invoke();

            interaction.StartInteraction();
            movement.MoveCharacter();
            jump.StartJump();

            if (_input.GetKeyboardInputDown("Crouch"))
                CallCoroutine(crouch.Crouch());
        }

        public void CallCoroutine(IEnumerator enumerator) => StartCoroutine(enumerator);

        public InputManager Input { get { return _input; } }
        public bool IsSprinting { get { return _input.GetKeyboardInput("Sprint"); } }
        public bool IsWalking { get; set; }
        public bool IsCrouched { get; set; }
        public bool IsOnGround { get { return jump.IsOnGround; } }
    }
}