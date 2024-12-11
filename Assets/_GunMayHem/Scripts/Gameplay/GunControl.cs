using DatdevUlts.AnimationUtils;
using DatdevUlts.Ults;
using GameTool.ObjectPool.Scripts;
using GameToolSample.ObjectPool;
using UnityEngine;

namespace _GunMayHem.Gameplay
{
    public class GunControl : MonoBehaviour
    {
        [SerializeField] private Transform _posFire;
        [SerializeField] private TypeBullet _typeBullet;
        [SerializeField] private AnimatorController _animator;

        [Header("CONFIG DATA_________________________")] [SerializeField]
        private CharacterControl _character;

        [SerializeField] private int _maxAmmo;
        [SerializeField] private float _timeShoot;
        [SerializeField] private bool _isInfiniteAmmo;
        [SerializeField] private bool _isHold;
        [SerializeField] private float _speed;
        [SerializeField] private int _dmg;

        private int _ammo;
        private bool _canShoot;

        public bool HaveAmmo => _ammo > 0 || _isInfiniteAmmo;

        public CharacterControl Character
        {
            get => _character;
            set => _character = value;
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
            if (_character.IsPlayer)
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

        public void Shoot()
        {
            if (!HaveAmmo || !_canShoot)
            {
                return;
            }

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


            bullet.GetComponent<BulletControl>().SetData(_speed, _character.transform.right, _posFire.position,
                _dmg, _character);
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
                _character.ChangeGun(1);
            }
        }
    }

    public enum TypeBullet
    {
        ShotGun,
        Short,
        Long
    }
}