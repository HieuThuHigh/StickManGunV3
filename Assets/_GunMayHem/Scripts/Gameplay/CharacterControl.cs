using System.Collections.Generic;
using System.Linq;
using DatdevUlts.AnimationUtils;
using DatdevUlts.Ults;
using UnityEngine;

namespace _GunMayHem.Gameplay
{
    public class CharacterControl : MonoBehaviour
    {
        [SerializeField] private AnimatorController _animator;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private LayerMask _layerMaskGround;
        [SerializeField] private List<Transform> _listPosCheckGround;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _maxSpeedX;
        [SerializeField] private float _jumpForce;
        [SerializeField] private bool _isPlayer;
        [SerializeField] private int _maxJumps;

        // ___________________________________________BOT
        private GroundControl _groundWish;

        public bool IsPlayer => _isPlayer;
        public bool IsBot => !_isPlayer;

        private int _currentJumps;
        private bool _isGrounded;
        private Collider2D _groundCurrent;

        private void Update()
        {
            if (_isPlayer && Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }

            _isGrounded = false;
            _groundCurrent = null;
            if (_rigidbody.velocity.y <= 0.1f)
            {
                foreach (var pos in _listPosCheckGround)
                {
                    var raycastHit2D = Physics2D.Raycast(pos.position, Vector2.down, 0.1f, _layerMaskGround);
                    if (raycastHit2D.collider)
                    {
                        _isGrounded = true;
                        _currentJumps = _maxJumps;
                        _groundCurrent = raycastHit2D.collider;
                        break;
                    }
                }
            }


            if (_isPlayer && Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (_groundCurrent && _rigidbody.velocity.y <= 0.1f)
                {
                    var gr = _groundCurrent;
                    Physics2D.IgnoreCollision(_collider, gr);
                    this.DelayedCall(0.5f, () => { Physics2D.IgnoreCollision(_collider, gr, false); });
                }
            }

            if (_isPlayer && Input.GetKey(KeyCode.RightArrow))
            {
                MoveRight();
            }
            else if (_isPlayer && Input.GetKey(KeyCode.LeftArrow))
            {
                MoveLeft();
            }
            else
            {
                Idle();
            }

            UpdateBot();
        }

        public void UpdateBot()
        {
            if (_isPlayer)
            {
                return;
            }

            bool groundBelow = false;
            foreach (var pos in _listPosCheckGround)
            {
                var raycastHit2D = Physics2D.Raycast(pos.position, Vector2.down, 100, _layerMaskGround);
                if (raycastHit2D.collider)
                {
                    _groundWish = raycastHit2D.collider.GetComponent<GroundControl>();
                    groundBelow = true;
                    break;
                }
            }

            if (!groundBelow)
            {
                if (_currentJumps <= 0)
                {
                    var groundWish = GameplayManager.Instance.ListPos
                        .Where(pos => pos.position.y < transform.position.y - 1).ToList()
                        .GetEst(
                            (current, est) =>
                            {
                                if ((current.position - transform.position).magnitude <
                                    (est.position - transform.position).magnitude)
                                {
                                    return true;
                                }

                                return false;
                            });
                    if (groundWish)
                    {
                        _groundWish = GetComponentInParent<GroundControl>();
                    }
                    else
                    {
                        _groundWish = null;
                    }
                }
                else
                {
                    _groundWish = GameplayManager.Instance.ListPos.GetEst((current, est) =>
                    {
                        if ((current.position - transform.position).magnitude <
                            (est.position - transform.position).magnitude)
                        {
                            return true;
                        }

                        return false;
                    }).GetComponentInParent<GroundControl>();
                }
            }

            if (_groundWish)
            {
                if (_groundWish.Center.y > transform.position.y && _rigidbody.velocity.y < 0)
                {
                    Jump();
                }

                if (_groundWish.Center.x - transform.position.x > 0.5f)
                {
                    MoveRight();
                }
                else if (_groundWish.Center.x - transform.position.x < -0.5f)
                {
                    MoveLeft();
                }
                else
                {
                    Idle();
                }
            }
            else
            {
                Idle();
            }
        }

        private void Idle()
        {
            SetAnimMove("FootIdle");
        }

        private void MoveLeft()
        {
            if (_rigidbody.velocity.x > -_maxSpeedX)
            {
                _rigidbody.AddForce(Vector2.left * _moveSpeed);
            }

            transform.rotation = Quaternion.Euler(0, 180, 0);
            SetAnimMove("FootMove");
        }

        private void MoveRight()
        {
            if (_rigidbody.velocity.x < _maxSpeedX)
            {
                _rigidbody.AddForce(Vector2.right * _moveSpeed);
            }

            transform.rotation = Quaternion.Euler(0, 0, 0);
            SetAnimMove("FootMove");
        }

        private void Jump()
        {
            if (_currentJumps <= 0)
            {
                return;
            }

            _animator.SetAnimation("FootJump", true, mixDuration: 0.1f,
                onEnd: () => { _animator.SetAnimation("FootIdle", true, mixDuration: 0f); });

            var vector2 = _rigidbody.velocity;
            vector2.y = _jumpForce;
            _rigidbody.velocity = vector2;
            _currentJumps--;
        }

        private void SetAnimMove(string animName)
        {
            if (_isGrounded && _animator.AnimName != animName && _animator.AnimName != "FootJump")
            {
                _animator.SetAnimation(animName, true);
            }
        }

        public void TakeDmg(Vector3 dmg)
        {
            _rigidbody.AddForce(dmg * _rigidbody.mass, ForceMode2D.Impulse);
        }
    }
}