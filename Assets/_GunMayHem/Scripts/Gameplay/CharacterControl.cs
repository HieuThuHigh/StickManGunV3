using System;
using System.Collections.Generic;
using DatdevUlts.AnimationUtils;
using UnityEngine;

namespace _GunMayHem.Gameplay
{
    public class CharacterControl : MonoBehaviour
    {
        [SerializeField] private AnimatorController _animator;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private LayerMask _layerMaskGround;
        [SerializeField] private List<Transform> _listPosCheckGround;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _maxSpeedX;
        [SerializeField] private float _jumpForce;

        private bool _isGrounded;

        private void Update()
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                _rigidbody.AddForce(Vector2.right * _moveSpeed);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                SetAnimMove("FootMove");
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                _rigidbody.AddForce(Vector2.left * _moveSpeed);
                transform.rotation = Quaternion.Euler(0, 180, 0);
                SetAnimMove("FootMove");
            }
            else
            {
                SetAnimMove("FootIdle");
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _animator.SetAnimation("FootJump", true, mixDuration: 0.1f,
                    onEnd: () => { _animator.SetAnimation("FootIdle", true, mixDuration: 0f); });

                var vector2 = _rigidbody.velocity;
                vector2.y = _jumpForce;
                _rigidbody.velocity = vector2;
            }

            if (Mathf.Abs(_rigidbody.velocity.x) > _maxSpeedX)
            {
                var vector2 = _rigidbody.velocity;
                vector2.x = Mathf.Clamp(_rigidbody.velocity.x, -_maxSpeedX, _maxSpeedX);
                _rigidbody.velocity = vector2;
            }

            _isGrounded = false;
            foreach (var pos in _listPosCheckGround)
            {
                if (Physics2D.Raycast(pos.position, Vector2.down, 0.1f, _layerMaskGround))
                {
                    _isGrounded = true;
                    break;
                }
            }
        }

        private void SetAnimMove(string animName)
        {
            if (_isGrounded && _animator.AnimName != animName && _animator.AnimName != "FootJump")
            {
                _animator.SetAnimation(animName, true);
            }
        }
    }
}