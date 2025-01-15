using System.Collections.Generic;
using System.Linq;
using DatdevUlts.Ults;
using GameTool.Assistants.DesignPattern;
using GameTool.ObjectPool.Scripts;
using GameToolSample.ObjectPool;
using GameToolSample.Scripts.LoadScene;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace _GunMayHem.Gameplay
{
    [RequireComponent(typeof(PhotonView))]
    public class GameManager : SingletonMonoBehaviour<GameManager>, IPunObservable
    {
        [SerializeField] private GameObject _victory;
        [SerializeField] private GameObject _lose;
        [SerializeField] private List<Button> _listBtnHome;
        [SerializeField] private List<Button> _listBtnReplay;
        [SerializeField] private Transform _boundLeftGift;
        [SerializeField] private Transform _boundRightGift;
        private List<GroundControl> _listGroundControls = new List<GroundControl>();
        private List<Transform> _listPos = new List<Transform>();
        private PhotonView _photonView;
        private bool _ended = false;

        public List<GroundControl> ListGroundControls => _listGroundControls;
        public List<Transform> ListPos => _listPos;

        protected override void Awake()
        {
            base.Awake();
            _photonView = GetComponent<PhotonView>();

            _listGroundControls = FindObjectsOfType<GroundControl>().ToList();

            _listPos.AddRange(_listGroundControls.Select(control => control.LeftPoint));
            _listPos.AddRange(_listGroundControls.Select(control => control.RightPoint));

            foreach (var button in _listBtnHome)
            {
                button.onClick.AddListener(() =>
                {
                    if (PhotonNetwork.IsMasterClient)
                    {
                        PhotonNetwork.LoadLevel("Home");
                    }
                });
            }

            foreach (var button in _listBtnReplay)
            {
                button.onClick.AddListener(() =>
                {
                    if (PhotonNetwork.IsMasterClient)
                    {
                        PhotonNetwork.LoadLevel(PhotonNetwork.CurrentRoom.Name);
                    }
                });
            }

            if (PhotonNetwork.IsMasterClient)
            {
                DropGift();
            }
        }

        [PunRPC]
        private void SpawnGift(Vector2 pos)
        {
            PoolingManager.Instance.GetObject(ePrefabPool.Gift, position: pos).Disable(10f);
        }

        public void DropGift()
        {
            var posX = RandomUlts.Range(_boundLeftGift.position.x, _boundRightGift.position.x);
            var pos = new Vector2(posX, 20);

            _photonView.RPC("SpawnGift", RpcTarget.All, pos);

            this.DelayedCall(5f, DropGift);
        }

       public void Victory()
    {
        if (_ended) return;
        _ended = true;

        if (_victory != null)
        {
            _victory.SetActive(true);
        }
        else
        {
            Debug.LogError("Victory UI chưa được gán.");
        }
    }

    public void Lose()
    {
        if (_ended) return;
        _ended = true;

        if (_lose != null)
        {
            _lose.SetActive(true);
        }
        else
        {
            Debug.LogError("Lose UI chưa được gán.");
        }
    }
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            // Đồng bộ dữ liệu nếu cần thiết
        }
    }
}
