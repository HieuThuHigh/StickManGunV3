using System;
using System.Collections.Generic;
using System.Linq;
using GameTool.Assistants.DesignPattern;
using UnityEngine;

namespace _GunMayHem.Gameplay
{
    public class GameplayManager : SingletonMonoBehaviour<GameplayManager>
    {
        private List<GroundControl> _listGroundControls = new List<GroundControl>();
        private List<Transform> _listPos = new List<Transform>();

        public List<GroundControl> ListGroundControls => _listGroundControls;
        public List<Transform> ListPos => _listPos;

        protected override void Awake()
        {
            base.Awake();
            _listGroundControls =
                FindObjectsByType<GroundControl>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).ToList();

            _listPos.AddRange(ListGroundControls.Select(control => control.LeftPoint).ToList());
            _listPos.AddRange(ListGroundControls.Select(control => control.RightPoint).ToList());
        }
    }
}