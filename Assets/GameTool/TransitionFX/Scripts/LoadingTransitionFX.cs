using System;
using GameTool.Assistants.DesignPattern;

namespace GameTool.TransitionFX.Scripts
{
    public class LoadingTransitionFX : SingletonMonoBehaviour<LoadingTransitionFX>
    {
        private Action callBackEndAnimInEvent;
        private Action callBackEndAnimOutEvent;

        public void ActiveLoading(Action _callBack = null)
        {
            callBackEndAnimInEvent = _callBack;
            EndLoadingInAnimEvent();
        }

        public void DisableLoading(Action _callBack = null)
        {
            callBackEndAnimOutEvent = _callBack;
        }

        void EndLoadingInAnimEvent()
        {
            callBackEndAnimInEvent?.Invoke();
            callBackEndAnimInEvent = null;
            EndLoadingOutAnimEvent();
        }

        void EndLoadingOutAnimEvent()
        {
            callBackEndAnimOutEvent?.Invoke();
            callBackEndAnimOutEvent = null;
        }
    }
}