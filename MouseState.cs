using UnityEngine;
using System;

namespace Cassardia
{
    public static class MouseState
    {
        public static void SetState(int value)
        {
            if (value < 0 || value >= 2)
                throw new NotImplementedException("Invalid input. Mouse state has only three states with a range of 0 to 2.");

            switch (value)
            {
                case 0:
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    break;

                case 1:
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                    break;

                case 2:
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    break;
            }
        }
    }
}