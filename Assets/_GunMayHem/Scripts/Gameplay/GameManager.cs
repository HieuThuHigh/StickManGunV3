using System;
using System.Collections.Generic;
using System.Linq;
using DatdevUlts.Ults;
using GameTool.Assistants.DesignPattern;
using GameTool.Audio.Scripts;
using GameTool.ObjectPool.Scripts;
using GameToolSample.Audio;
using GameToolSample.ObjectPool;
using GameToolSample.Scripts.LoadScene;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

namespace _GunMayHem.Gameplay
{
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        [SerializeField] private GameObject _victory;
        [SerializeField] private GameObject _lose;
        [SerializeField] private List<Button> _listBtnHome;
        [SerializeField] private List<Button> _listBtnReplay;
        [SerializeField] private Transform _boundLeftGift;
        [SerializeField] private Transform _boundRightGift;
        private List<GroundControl> _listGroundControls = new List<GroundControl>();
        private List<Transform> _listPos = new List<Transform>();

        public List<GroundControl> ListGroundControls => _listGroundControls;
        public List<Transform> ListPos => _listPos;

        private bool _ended;

        protected override void Awake()
        {
            AudioManager.Instance.PlayMusic(eMusicName.MusicMain);

            base.Awake();
            _listGroundControls =
                FindObjectsByType<GroundControl>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).ToList();

            _listPos.AddRange(ListGroundControls.Select(control => control.LeftPoint).ToList());
            _listPos.AddRange(ListGroundControls.Select(control => control.RightPoint).ToList());

            foreach (var button in _listBtnHome)
            {
                button.onClick.AddListener(() => { SceneLoadManager.Instance.LoadSceneWithName("Home"); });
            }

            foreach (var button in _listBtnReplay)
            {
                button.onClick.AddListener(() => { SceneLoadManager.Instance.LoadCurrentScene(); });
            }

            DropGift();
        }
        public void homebutton()
        {
            if (PhotonNetwork.IsConnected)
            {
                // Ngắt kết nối khỏi Photon khi bấm nút thoát
                PhotonNetwork.Disconnect();
            }
            SceneManager.LoadScene("Home");
        }
        public void DropGift()
        {
            var posX = RandomUlts.Range(_boundLeftGift.position.x, _boundRightGift.position.x);

            var pos = new Vector2(posX, 20);

            PoolingManager.Instance.GetObject(ePrefabPool.Gift, position: pos).Disable(10f);

            this.DelayedCall(5f, DropGift);
        }

        public void Victory()
        {
            if (_ended)
            {
                return;
            }

            _ended = true;

            _victory.gameObject.SetActive(true);
        }

        public void Lose()
        {
            if (_ended)
            {
                return;
            }

            _ended = true;

            _lose.gameObject.SetActive(true);
        }
    }
}