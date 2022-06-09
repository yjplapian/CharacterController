using UnityEngine;
using System;

namespace Cassardia
{
    [Serializable]
    public class PlayerInteraction
    {
        private InputManager _input;
        private Transform _transform;
        private PlayerController _controller;
        [SerializeField] private LayerMask _ignoredLayers;
        [SerializeField] private float _interactionDistance = 1.5f;

        public void Initialize()
        {
            _controller = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            _transform = Camera.main.transform;
            _input = _controller.Input;
        }

        public void StartInteraction()
        {
            if (_input.GetKeyboardInputDown("Interact"))
            {
                if (Physics.Raycast(_transform.position, _transform.forward, out RaycastHit hit, _interactionDistance, ~_ignoredLayers))
                {
                        Debug.Log(hit.transform.name);

                     if(hit.transform.TryGetComponent(out IInteractive currentInteractive))
                        currentInteractive.OnInteract(_controller);
                }
            }
        }
    }
}