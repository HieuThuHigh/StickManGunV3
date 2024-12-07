using System;
using System.Collections;
using UnityEngine;

namespace DatdevUlts.AnimationUtils
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorController : MonoBehaviour
    {
        [SerializeField] private bool _loop;
        [SerializeField] private bool _pausing;
        [SerializeField] private float _timeScale = 1;
        [SerializeField] private bool _ignoreTimeScale;
        [SerializeField] private bool _enableLog;
        [AnimName] [SerializeField] private string _animName;
        private string m_animName;
        private Animator _animator;
        private bool _pausingLoop;
        private bool _awaked;

        public bool Pause
        {
            get => _pausing;
            set => _pausing = value;
        }

        private int Pausing => _pausing ? 0 : 1;

        public bool IgnoreTimeScale
        {
            get => _ignoreTimeScale;
            set => _ignoreTimeScale = value;
        }

        public bool Loop
        {
            get => _loop;
            set => _loop = value;
        }

        public bool Awaked => _awaked;

        public float TimeScale
        {
            get => _timeScale;
            set => _timeScale = value;
        }

        public string AnimName
        {
            get => m_animName;
            set => m_animName = value;
        }

        public bool EnableLog
        {
            get => _enableLog;
            set => _enableLog = value;
        }

        public Action OnStartAnim
        {
            get => _onStartAnim;
            set => _onStartAnim = value;
        }

        public Action OnEndAnim
        {
            get => _onEndAnim;
            set => _onEndAnim = value;
        }

        private bool PausingLoop
        {
            get => _pausingLoop;
            set => _pausingLoop = value;
        }

        private const float OffsetEnd = 0.0001f;

        private Action _onStartAnim;
        private Action _onEndAnim;

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

        private void OnEnable()
        {
            Update(0);
            if (!_awaked)
            {
                StartCoroutine(CheckAwake());
            }

            IEnumerator CheckAwake()
            {
                yield return null;
                _awaked = true;
            }
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
            var currentAnimatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

            if (_animName != m_animName)
            {
                SetAnimation(_animName);
                return;
            }

            if (_pausing)
            {
                return;
            }

            var length = currentAnimatorStateInfo.length;
            var currentTime = length *
                              (currentAnimatorStateInfo.normalizedTime - (int)currentAnimatorStateInfo.normalizedTime);

            var deltatime = Time.deltaTime * _timeScale;
            if (_ignoreTimeScale)
            {
                deltatime = Time.unscaledDeltaTime * _timeScale;
            }

            if (!_loop)
            {
                if (!PausingLoop && length - currentTime < deltatime && currentAnimatorStateInfo.IsName(m_animName))
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

        public void SetAnimation(string animationName, bool loop, float timeScale = 1f, float mixDuration = 0.25f,
            Action onStart = null, Action onEnd = null, int layer = 0, bool quiet = true)
        {
            var has = Animator.HasState(layer, Animator.StringToHash(animationName));
            if (!has)
            {
                if (!quiet)
                {
                    if (_enableLog)
                    {
                        Debug.LogError($"State {animationName} of layer {layer} is NULL");
                    }

                    return;
                }
            }

            if (layer == 0)
            {
                OnStartAnim = onStart;
                OnEndAnim = null;

                _loop = loop;
            }

            var currentAnimatorStateInfo = _animator.GetCurrentAnimatorStateInfo(layer);

            var length = currentAnimatorStateInfo.length;
            var currentTime = length *
                              (currentAnimatorStateInfo.normalizedTime - (int)currentAnimatorStateInfo.normalizedTime);

            if (mixDuration > length - currentTime - OffsetEnd)
            {
                mixDuration = length - currentTime - OffsetEnd * 2;
            }

            if (layer == 0)
            {
                PausingLoop = false;

                m_animName = animationName;
                _animName = m_animName;
            }

            if (mixDuration <= OffsetEnd || mixDuration <= 0)
            {
                Animator.Play(animationName, layer);
            }
            else
            {
                Animator.CrossFadeInFixedTime(animationName, mixDuration, layer, 0);

                if (Awaked)
                {
                    Animator.Update(0);
                }
            }

            if (layer == 0)
            {
                _timeScale = timeScale;

                OnEndAnim = onEnd;
            }
        }

        public void SetAnimation(string animName)
        {
            SetAnimation(animName, _loop);
        }

        [ContextMenu("SetAnimation")]
        public void SetAnimation()
        {
            SetAnimation(m_animName);
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