using DatdevUlts.AnimationUtils;
using DatdevUlts.Ults;
using GameTool.ObjectPool.Scripts;
using GameToolSample.ObjectPool;
using UnityEngine;
using Photon.Pun;

namespace _GunMayHem.Gameplay
{
    public class Gun2 : MonoBehaviour
    {
        [SerializeField] private Transform _posFire;
        [SerializeField] private TypeBullet _typeBullet;
        [SerializeField] private AnimatorController _animator;

        [Header("CONFIG DATA_________________________")]
        [SerializeField]
        private character2 _character1;
        [SerializeField] private int _maxAmmo;
        [SerializeField] private float _timeShoot;
        [SerializeField] private bool _isInfiniteAmmo;
        [SerializeField] private bool _isHold;
        [SerializeField] private float _speed;
        [SerializeField] private int _dmg;

        private int _ammo;
        private bool _canShoot;
        public PhotonView photonView;
        public bool HaveAmmo => _ammo > 0 || _isInfiniteAmmo;

        public character2 Character2
        {
            get => _character1;
            set => _character1 = value;
        }
        public Transform PosFire => _posFire;

        public bool CanShoot => _canShoot;

        private void OnEnable()
        {
            _ammo = _maxAmmo;
            _canShoot = true;
        }

        private void Update()
        {
            if (_character1.IsPlayer && photonView.IsMine)
            {
                if (Input.GetKeyDown(KeyCode.J))
                {
                    Shoot();
                }
                else if (_isHold && Input.GetKey(KeyCode.J))
                {
                    Shoot();
                }
            }
        }

        public void ButtonShoot()
        {
            if (photonView.IsMine)
            {
                Shoot();
            }
        }

        public void Shoot()
        {
            if (!HaveAmmo || !_canShoot)
            {
                return;
            }

            // Đồng bộ hóa bắn đạn qua Photon
            photonView.RPC("RPC_Shoot", RpcTarget.All, _speed, _character1.transform.right, _posFire.position, _dmg);

            _canShoot = false;
            this.DelayedCall(_timeShoot, () => _canShoot = true);
            if (!_isInfiniteAmmo)
            {
                _ammo--;
            }

            _animator.SetAnimation("shoot", true, onEnd: () =>
            {
                _animator.SetAnimation("Empty");
            });

            if (!HaveAmmo)
            {
                _character1.ChangeGun(1);
            }
        }

        [PunRPC]
        private void RPC_Shoot(float speed, Vector3 direction, Vector3 position, int damage)
        {
            // Kiểm tra và log ViewID để debug
            Debug.Log("PhotonView ID: " + photonView.ViewID);

            // Kiểm tra Pooling để lấy loại đạn
            BasePooling bullet;

            if (_typeBullet == TypeBullet.Short)
            {
                bullet = PoolingManager.Instance.GetObject(ePrefabPool.BulletShort);
            }
            else if (_typeBullet == TypeBullet.Long)
            {
                bullet = PoolingManager.Instance.GetObject(ePrefabPool.BulletLong);
            }
            else
            {
                bullet = PoolingManager.Instance.GetObject(ePrefabPool.BulletShotGun);
            }

            // Set các dữ liệu cho đạn
            bullet.GetComponent<BulletControl>().SetData(speed, direction, position, damage, _character1);
        }

    }

    public enum TypeBullet1
    {
        ShotGun,
        Short,
        Long
    }
}
