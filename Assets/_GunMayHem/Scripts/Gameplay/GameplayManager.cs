using System;
using System.Collections.Generic;
using System.Linq;
using DatdevUlts.Ults;
using GameTool.Assistants.DesignPattern;
using GameToolSample.Scripts.LoadScene;
using UnityEngine;

namespace _GunMayHem.Gameplay
{
    public class GameplayManager : SingletonMonoBehaviour<GameplayManager>
    {
        [SerializeField] private GameObject _victory;
        [SerializeField] private GameObject _lose;
        private List<GroundControl> _listGroundControls = new List<GroundControl>();
        private List<Transform> _listPos = new List<Transform>();

        public List<GroundControl> ListGroundControls => _listGroundControls;
        public List<Transform> ListPos => _listPos;

        private bool _ended;

        protected override void Awake()
        {
            base.Awake();
            _listGroundControls =
                FindObjectsByType<GroundControl>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).ToList();

            _listPos.AddRange(ListGroundControls.Select(control => control.LeftPoint).ToList());
            _listPos.AddRange(ListGroundControls.Select(control => control.RightPoint).ToList());
        }

        public void Victory()
        {
            if (_ended)
            {
                return;
            }

            _ended = true;
            this.DelayedCall(2f, () => { SceneLoadManager.Instance.LoadSceneWithName("SPL"); });

            _victory.gameObject.SetActive(true);
        }

        public void Lose()
        {
            if (_ended)
            {
                return;
            }

            _ended = true;
            this.DelayedCall(2f, () => { SceneLoadManager.Instance.LoadSceneWithName("SPL"); });

            _lose.gameObject.SetActive(true);
        }
    }
}