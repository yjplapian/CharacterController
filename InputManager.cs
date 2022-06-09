using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cassardia
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private InputMap _currentInputTemplate;

        private void Awake() => MouseState.SetState(0);

        public bool GetKeyboardInput(string key)
        {
            if (Input.GetKey(_currentInputTemplate.ReturnKeyboardInput(key)))
                return true;

            else
                return false;
        }

        public bool GetKeyboardInputDown(string key)
        {
            if (Input.GetKeyDown(_currentInputTemplate.ReturnKeyboardInput(key)))
                return true;

            else
                return false;
        }

        public bool GetKeyboardInputUp(string key)
        {
            if (Input.GetKeyUp(_currentInputTemplate.ReturnKeyboardInput(key)))
                return true;

            else
                return false;
        }

        public bool GetMouseInput(string key)
        {
            if (Input.GetMouseButton(_currentInputTemplate.ReturnMouseInput(key)))
                return true;

            else
                return false;
        }

        public bool GetMouseInputDown(string key)
        {
            if (Input.GetMouseButtonDown(_currentInputTemplate.ReturnMouseInput(key)))
                return true;

            else
                return false;
        }

        public bool GetMouseInputUp(string key)
        {
            if (Input.GetMouseButtonUp(_currentInputTemplate.ReturnMouseInput(key)))
                return true;

            else
                return false;
        }

        public InputMap CurrentInputTemplate { private get { return _currentInputTemplate; }  set { _currentInputTemplate = value; } }
    }
}