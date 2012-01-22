using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Big_McGreed.content.input
{
    public class InputHandler : IDisposable
    {
		/// <summary>
		/// A delegate used to create a hook callback.
		/// </summary>
		public delegate int GetMsgProc(int nCode, int wParam, ref Message msg);

        /// <summary>
        /// Install an application-defined hook procedure into a hook chain.
        /// </summary>
        /// <param name="idHook">Specifies the type of hook procedure to be installed.</param>
        /// <param name="lpfn">Pointer to the hook procedure.</param>
        /// <param name="hmod">Handle to the DLL containing the hook procedure pointed to by the lpfn parameter.</param>
        /// <param name="dwThreadId">Specifies the identifier of the thread with which the hook procedure is to be associated.</param>
        /// <returns>
        /// If the function succeeds, the return value is the handle to the hook procedure. Otherwise returns 0.
        /// </returns>
		[DllImport("user32.dll", EntryPoint = "SetWindowsHookExA")]
		public static extern IntPtr SetWindowsHookEx(HookType idHook, GetMsgProc lpfn, IntPtr hmod, int dwThreadId);

        /// <summary>
        /// Removes a hook procedure installed in a hook chain by the SetWindowsHookEx function.
        /// </summary>
        /// <param name="hHook">Handle to the hook to be removed. This parameter is a hook handle obtained by a previous call to SetWindowsHookEx.</param>
        /// <returns>
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
		[DllImport("user32.dll")]
		public static extern int UnhookWindowsHookEx(IntPtr hHook);

        /// <summary>
        /// Passes the hook information to the next hook procedure in the current hook chain.
        /// </summary>
        /// <param name="hHook">Ignored.</param>
        /// <param name="ncode">Specifies the hook code passed to the current hook procedure.</param>
        /// <param name="wParam">Specifies the wParam value passed to the current hook procedure.</param>
        /// <param name="lParam">Specifies the lParam value passed to the current hook procedure.</param>
        /// <returns>
        /// This value is returned by the next hook procedure in the chain.
        /// </returns>
		[DllImport("user32.dll")]
		public static extern int CallNextHookEx(int hHook, int ncode, int wParam, ref Message lParam);

        /// <summary>
        /// Translates virtual-key messages into character messages.
        /// </summary>
        /// <param name="lpMsg">Pointer to an Message structure that contains message information retrieved from the calling thread's message queue.</param>
        /// <returns>
        /// If the message is translated (that is, a character message is posted to the thread's message queue), the return value is true.
        /// </returns>
		[DllImport("user32.dll")]
		public static extern bool TranslateMessage(ref Message lpMsg);


        /// <summary>
        /// Retrieves the thread identifier of the calling thread.
        /// </summary>
        /// <returns>
        /// The thread identifier of the calling thread.
        /// </returns>
		[DllImport("kernel32.dll")]
		public static extern int GetCurrentThreadId();

		/// <summary>Handle for the created hook.</summary>
		private readonly IntPtr HookHandle;

		private readonly GetMsgProc ProcessMessagesCallback;

        private readonly InputEvents events;

        /// <summary>
        /// Create an instance of the TextInputHandler.
        /// </summary>
        /// <param name="whnd">Handle of the window you wish to receive messages (and thus keyboard input) from.</param>
		public InputHandler(IntPtr whnd) {
			// Create the delegate callback:
			this.ProcessMessagesCallback = new GetMsgProc(ProcessMessages);
			// Create the keyboard hook:
			this.HookHandle = SetWindowsHookEx(HookType.WH_GETMESSAGE, this.ProcessMessagesCallback, IntPtr.Zero, GetCurrentThreadId());
            // Create a new key event holder.
            this.events = new InputEvents();
		}

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            this.KeyPress += new KeyPressEventHandler(events.KeyPress);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
		public void Dispose() {
			if (this.HookHandle != IntPtr.Zero) UnhookWindowsHookEx(this.HookHandle);
		}

		private int ProcessMessages(int nCode, int wParam, ref Message msg) {
			// Check if we must process this message (and whether it has been retrieved via GetMessage):
			if (nCode == 0 && wParam == 1) {

					// We need character input, so use TranslateMessage to generate WM_CHAR messages.
					TranslateMessage(ref msg);

					// If it's one of the keyboard-related messages, raise an event for it:
					switch ((MessageType)msg.Msg) {
						case MessageType.WM_CHAR:
							this.OnKeyPress(new KeyPressEventArgs((char)msg.WParam));
							break;
						/* Nu niet nodig.
                         * case WindowMessage.WM_KEYDOWN:
							this.OnKeyDown(new KeyEventArgs((Keys)msg.WParam));
							break;
						case WindowMessage.WM_KEYUP:
							this.OnKeyUp(new KeyEventArgs((Keys)msg.WParam));
							break;*/
					}
			}

			// Call next hook in chain:
			return CallNextHookEx(0, nCode, wParam, ref msg);
		}


        /// <summary>
        /// Occurs when [key up].
        /// </summary>
		private event KeyEventHandler KeyUp;

        /// <summary>
        /// Raises the <see cref="E:KeyUp"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
		protected virtual void OnKeyUp(KeyEventArgs e) {
			if (this.KeyUp != null) this.KeyUp(this, e);
		}

        /// <summary>
        /// Occurs when [key down].
        /// </summary>
		private event KeyEventHandler KeyDown;

        /// <summary>
        /// Raises the <see cref="E:KeyDown"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
		protected virtual void OnKeyDown(KeyEventArgs e) {
			if (this.KeyDown != null) this.KeyDown(this, e);
		}

        /// <summary>
        /// Occurs when [key press].
        /// </summary>
		private event KeyPressEventHandler KeyPress;

        /// <summary>
        /// Raises the <see cref="E:KeyPress"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
		protected virtual void OnKeyPress(KeyPressEventArgs e) {
			if (this.KeyPress != null) this.KeyPress(this, e);
		}

        private event MouseEventHandler LeftMouseDown;

        /// <summary>
        /// Raises the <see cref="E:LeftMouseDown"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        protected virtual void OnLeftMouseDown(MouseEventArgs e)
        {
            if (this.LeftMouseDown != null) this.LeftMouseDown(this, e);
        }

        private event MouseEventHandler LeftMouseUp;

        /// <summary>
        /// Raises the <see cref="E:LeftMouseUp"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        protected virtual void OnLeftMouseUp(MouseEventArgs e)
        {
            if (this.LeftMouseUp != null) this.LeftMouseUp(this, e);
        }
    }
}
