using System;
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

        [Header("CONFIG DATA_________________________")] [SerializeField]
        private CharacterControl _character;

        [SerializeField] private int _maxAmmo;
        [SerializeField] private float _timeShoot;
        [SerializeField] private bool _isInfiniteAmmo;
        [SerializeField] private float _speed;
        [SerializeField] private int _dmg;

        private int _ammo;
        private bool _canShoot;
        
        public bool HaveAmmo => _ammo > 0 || _isInfiniteAmmo;

        public Transform PosFire => _posFire;

        public bool CanShoot => _canShoot;

        private void OnEnable()
        {
            _ammo = _maxAmmo;
            _canShoot = true;
        }

        private void Update()
        {
            if (_character.IsPlayer && Input.GetKeyDown(KeyCode.J))
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

            var bullet = PoolingManager.Instance.GetObject(ePrefabPool.BulletShort);

            bullet.GetComponent<BulletControl>().SetData(_speed, _character.transform.right, _posFire.position,
                _dmg, _character);
            _canShoot = false;
            this.DelayedCall(_timeShoot, () => _canShoot = true);
            if (!_isInfiniteAmmo)
            {
                _ammo--;
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