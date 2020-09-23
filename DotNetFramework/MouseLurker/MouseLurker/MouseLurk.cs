using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Input;
using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;

namespace Spy.Windows.Lurker
{
    public class MouseLurk
    {
        public MouseLurk(Action callback)
        {
            _callback = callback;
        }

        public void StartPicking()
        {
            GlobalHook = new GlobalHooker();
            MouseListener = new MouseHookListener(GlobalHook);
            MouseListener.MouseDownExt += _pick;
            MouseListener.Start();
        }

        private MouseHookListener MouseListener { get; set; }
        private GlobalHooker GlobalHook { get; set; }

        private Action _callback;

        private void _pick(object sender, MouseEventExtArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                e.Handled = true;
                _callback?.Invoke();
            }
        }

        public void StopPicking()
        {
            MouseListener.MouseDownExt -= _pick;
            MouseListener.Stop();
        }
    }
}