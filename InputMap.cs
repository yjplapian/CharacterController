using System.Collections.Generic;
using UnityEngine;

namespace Cassardia
{
    [CreateAssetMenu]
    public class InputMap : ScriptableObject
    {
        public Dictionary<string, KeyCode> _keyboardInputs = new Dictionary<string, KeyCode>();
        public Dictionary<string, int> _mouseInputs = new Dictionary<string, int>();

        [SerializeField] private List<string> _debugKeyboard = new List<string>();
        [SerializeField] private List<string> _debugMosue = new List<string>();
        [SerializeField] private bool _enableValidation = true;

        private void OnValidate()
        {
            if (_enableValidation)
            {
                _keyboardInputs.Clear();
                _mouseInputs.Clear();
                _debugKeyboard.Clear();
                _debugMosue.Clear();

                AssignInputs();
            }
        }

        private void AssignInputs()
        {
            ValidateKeyboardInput("Backward", KeyCode.S);
            ValidateKeyboardInput("Forward", KeyCode.W);
            ValidateKeyboardInput("Right", KeyCode.D);
            ValidateKeyboardInput("Left", KeyCode.A);

            ValidateKeyboardInput("Crouch", KeyCode.LeftControl);
            ValidateKeyboardInput("Sprint", KeyCode.LeftShift);
            ValidateKeyboardInput("Jump", KeyCode.Space);

            ValidateKeyboardInput("SwitchPerspective", KeyCode.F);
            ValidateKeyboardInput("SwitchLight", KeyCode.L);
            ValidateKeyboardInput("Sheathe", KeyCode.R);
            ValidateKeyboardInput("Interact", KeyCode.E);
            ValidateKeyboardInput("Power", KeyCode.Z);

            ValidateKeyboardInput("MainMenu", KeyCode.Escape);
            ValidateKeyboardInput("FavoritesMenu", KeyCode.Q);
            ValidateKeyboardInput("InventoryMenu", KeyCode.I);
            ValidateKeyboardInput("LevelMenu", KeyCode.Slash);
            ValidateKeyboardInput("QuestMenu", KeyCode.J);
            ValidateKeyboardInput("Worldmap", KeyCode.M);
            ValidateKeyboardInput("SpellMenu", KeyCode.P);

            ValidateKeyboardInput("Hotkey 0", KeyCode.Alpha0);
            ValidateKeyboardInput("Hotkey 1", KeyCode.Alpha1);
            ValidateKeyboardInput("Hotkey 2", KeyCode.Alpha2);
            ValidateKeyboardInput("Hotkey 3", KeyCode.Alpha3);
            ValidateKeyboardInput("Hotkey 4", KeyCode.Alpha4);
            ValidateKeyboardInput("Hotkey 5", KeyCode.Alpha5);
            ValidateKeyboardInput("Hotkey 6", KeyCode.Alpha6);
            ValidateKeyboardInput("Hotkey 7", KeyCode.Alpha7);
            ValidateKeyboardInput("Hotkey 8", KeyCode.Alpha8);
            ValidateKeyboardInput("Hotkey 9", KeyCode.Alpha9);

            ValidateMouseInput("Attack", 0);
            ValidateMouseInput("Block", 1);
            ValidateMouseInput("Aim", 1);
        }

        private void ValidateKeyboardInput(string key, KeyCode value)
        {
            if (_keyboardInputs.ContainsKey(key)) return;

            _keyboardInputs.Add(key, value);
            _debugKeyboard.Add(key + " || " + value.ToString());
        }

        private void ValidateMouseInput(string key, int value)
        {
            if (_mouseInputs.ContainsKey(key)) return;

            _mouseInputs.Add(key, value);
            _debugMosue.Add(key + " || " + value.ToString());
        }

        public KeyCode ReturnKeyboardInput(string key)
        {
            if (!_keyboardInputs.ContainsKey(key))
                return KeyCode.None;

            else
                return _keyboardInputs[key];
        }

        public int ReturnMouseInput(string key)
        {
            if (!_mouseInputs.ContainsKey(key))
                return -1;

            else
                return _mouseInputs[key];
        }
    }
}