using System;
using GameTool.ObjectPool.Scripts;
using UnityEngine;

namespace _GunMayHem.Gameplay
{
    public class BulletControl : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb2d;
        [SerializeField] private TrailRenderer _trailRenderer;
        [SerializeField] private bool _isShotgun;

        private CharacterControl _shooter;
        private character2 _shooter1;
        private int _dmg;
        private Vector3 _direction;

        private void OnEnable()
        {
            if (!_isShotgun)
            {
                GetComponent<BasePooling>().Disable(5f);
            }
            else
            {
                GetComponent<BasePooling>().Disable(0.05f);
            }
        }

        private void OnDisable()
        {
            if (!_isShotgun)
            {
                _trailRenderer.Clear();
            }
        }

        public void SetData(float speed, Vector3 direction, Vector3 pos, int dmg, CharacterControl shooter)
        {
            _shooter = shooter;
            _dmg = dmg;
            _direction = direction;
            transform.position = pos;
            _rb2d.velocity = direction * speed;
            transform.right = direction;
        }
        public void SetData(float speed, Vector3 direction, Vector3 pos, int dmg, character2 shooter1)
        {
            _shooter1 = shooter1;
            _dmg = dmg;
            _direction = direction;
            transform.position = pos;
            _rb2d.velocity = direction * speed;
            transform.right = direction;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.attachedRigidbody)
            {
                return;
            }

            var characterControl = other.attachedRigidbody.GetComponent<CharacterControl>();
            if (characterControl && characterControl != _shooter)
            {
                characterControl.TakeDmg(_direction * _dmg);
                gameObject.SetActive(false);
            }
            // character2
            if (!other.attachedRigidbody)
            {
                return;
            }

            var Character2 = other.attachedRigidbody.GetComponent<character2>();
            if (Character2 && Character2 != _shooter)
            {
                Character2.TakeDmg(_direction * _dmg);
                gameObject.SetActive(false);
            }
        }

    }
}