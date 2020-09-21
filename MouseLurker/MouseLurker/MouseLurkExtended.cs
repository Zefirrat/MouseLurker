using System;
using System.Windows.Input;
using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;

namespace Spy.Windows.Lurker
{
    public class MouseLurkExtended
    {
        public MouseLurkExtended(Action<MouseEventExtArgs> callback)
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

        private Action<MouseEventExtArgs> _callback;

        private void _pick(object sender, MouseEventExtArgs e)
        {
           
                _callback?.Invoke(e);
        }

        public void StopPicking()
        {
            MouseListener.MouseDownExt -= _pick;
            MouseListener.Stop();
        }
    }
}