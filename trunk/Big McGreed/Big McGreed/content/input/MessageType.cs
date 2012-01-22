using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Big_McGreed.content.input
{
    /// <summary>
    /// Window message types.
    /// </summary>
    /// <remarks>Heavily abridged, naturally.</remarks>
    public enum MessageType
    {
        /// <summary>
        /// Key is down.
        /// </summary>
        WM_KEYDOWN = 0x100,
        /// <summary>
        /// Key is up.
        /// </summary>
        WM_KEYUP = 0x101,
        /// <summary>
        /// Key is pressed.
        /// </summary>
        WM_CHAR = 0x102,
        /// <summary>
        /// System key is down.
        /// </summary>
        WM_SYSKEYDOWN = 0x104,
        /// <summary>
        /// System key is up.
        /// </summary>
        WM_SYSKEYUP = 0x105,
        /// <summary>
        /// System key is pressed.
        /// </summary>
        WM_SYSCHAR = 0x106,
        /// <summary>
        /// Mouse movement.
        /// </summary>
        WM_MOUSE_MOVEMENT = 0x0001,
        /// <summary>
        /// Left mouse button is down.
        /// </summary>
        WM_MOUSE_LEFTDOWN = 0x0002,
        /// <summary>
        /// Left mouse button is up.
        /// </summary>
        WM_MOUSE_LEFTUP = 0x0004,
        /// <summary>
        /// Right mouse button is down.
        /// </summary>
        WM_MOUSE_RIGHTDOWN = 0x0008,
        /// <summary>
        /// Right mouse button is up.
        /// </summary>
        WM_MOUSE_RIGHTUP = 0x0010,
        /// <summary>
        /// Middle mouse button is down.
        /// </summary>
        WM_MOUSE_MIDDLEDOWN = 0x0020,
        /// <summary>
        /// Middle mouse button is up.
        /// </summary>
        WM_MOUSE_MIDDLEUP = 0x0040,
        /// <summary>
        /// Middle mouse button value changed.
        /// </summary>
        WM_MOUSE_WHEEL = 0x0800,
    };
}
