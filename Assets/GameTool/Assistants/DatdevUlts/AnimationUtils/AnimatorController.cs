using System;
using UnityEngine;

namespace DatdevUlts.AnimationUtils
{
    public class AnimatorController : MonoBehaviour
    {
        [SerializeField] private bool _loop;
        [SerializeField] private bool _pausing;
        private Animator _animator;
        private bool _pausingLoop;
        private string _animName;

        public bool Pause
        {
            get => _pausing;
            set => _pausing = value;
        }

        private int Pausing => _pausing ? 0 : 1;

        private bool PausingLoop
        {
            get => _pausingLoop;
            set => _pausingLoop = value;
        }

        private const float OffsetEnd = 0.0001f;

        private Action OnStartAnim;
        private Action OnEndAnim;

        public Animator Animator
        {
            get
            {
                SetupAnimator();

                return _animator;
            }
        }

        /// <summary>
        /// arg1: Tên sự kiện
        /// </summary>
        public event Action<string> TrackEvent;

        private void Awake()
        {
            Animator.enabled = false;
        }

        private void SetupAnimator()
        {
            if (!_animator)
            {
                _animator = GetComponentInChildren<Animator>();
            }
        }

        public void Update()
        {
            if (_pausing)
            {
                return;
            }

            var currentAnimatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

            var length = currentAnimatorStateInfo.length;
            var currentTime = length *
                              (currentAnimatorStateInfo.normalizedTime - (int)currentAnimatorStateInfo.normalizedTime);

            var deltatime = Time.deltaTime;

            if (!_loop)
            {
                if (!PausingLoop && length - currentTime < deltatime && currentAnimatorStateInfo.IsName(_animName))
                {
                    deltatime = length - currentTime - OffsetEnd;

                    Update(deltatime * Pausing);
                    PausingLoop = true;
                    OnEndAnim?.Invoke();
                }
                else if (!PausingLoop)
                {
                    Update(deltatime * Pausing);
                }
            }
            else
            {
                if (length - currentTime < deltatime)
                {
                    deltatime = length - currentTime;
                    Update(deltatime * Pausing);
                    OnEndAnim?.Invoke();
                    OnStartAnim?.Invoke();
                }
                else
                {
                    Update(deltatime * Pausing);
                }
            }
        }

        public void SetAnimation(string animationName, bool loop, float timeScale = 1f, float mixDuration = 0.25f, Action onStart = null,
            Action onEnd = null, int layer = 0)
        {
            var has = Animator.HasState(0, Animator.StringToHash(animationName));
            if (!has)
            {
                Debug.LogError($"State {animationName} is NULL");
                return;
            }

            OnStartAnim = onStart;
            OnEndAnim = null;

            _loop = loop;

            var currentAnimatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

            var length = currentAnimatorStateInfo.length;
            var currentTime = length *
                              (currentAnimatorStateInfo.normalizedTime - (int)currentAnimatorStateInfo.normalizedTime);

            if (mixDuration > length - currentTime - OffsetEnd)
            {
                mixDuration = length - currentTime - OffsetEnd * 2;
            }

            PausingLoop = false;

            _animName = animationName;

            if (mixDuration <= OffsetEnd)
            {
                Animator.Play(animationName, layer, 0);
                Animator.Update(0);
            }
            else
            {
                Animator.CrossFadeInFixedTime(animationName, mixDuration, layer, 0);
                Animator.Update(0);
            }
            Animator.speed = timeScale;
            
            OnEndAnim = onEnd;
        }

        public void Update(float deltaTime)
        {
            Animator.Update(deltaTime);
        }

        public void PostEvent(string eventName)
        {
            TrackEvent?.Invoke(eventName);
        }

        public void RegistEvent(Action<string> callBack)
        {
            TrackEvent += callBack;
        }

        public void RemoveEvent(Action<string> callBack)
        {
            TrackEvent -= callBack;
        }
    }
}