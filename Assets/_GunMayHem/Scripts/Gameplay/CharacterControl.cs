using System;
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
        [SerializeField] private GunControl _gunControl;
        [SerializeField] private List<Transform> _listPosCheckGround;
        [SerializeField] private Transform _nameTxt;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _maxSpeedX;
        [SerializeField] private float _jumpForce;
        [SerializeField] private bool _isPlayer;
        [SerializeField] private bool _testMode;
        [SerializeField] private int _maxJumps;
        [SerializeField] private Color _color;
        private List<CharacterControl> _listCharEnemy = new List<CharacterControl>();


        //______________________________________________VARIABLE

        private LayerMask _layerMaskGround;
        private LayerMask _layerMaskChar;

        private int _currentJumps;
        private float _timeStun;
        private float _maxTimeCanDown = 1f;
        private float _currTimeCanDown = 0.5f;
        private bool _isGrounded;
        private Collider2D _groundCurrent;

        // ___________________________________________BOT
        private GroundControl _groundWish;
        private List<CharacterControl> _listCharEnemyRaycast = new List<CharacterControl>();

        //_____________________________________________PROPERTY
        public bool IsPlayer => _isPlayer;
        public bool IsBot => !_isPlayer;


        private RaycastHit2D[] _resultsHits = new RaycastHit2D[10];

        private void Start()
        {
            ChangeSkinColor();
            
            _layerMaskGround = LayerMask.GetMask("Ground");
            _layerMaskChar = LayerMask.GetMask("Player");

            _listCharEnemy = FindObjectsByType<CharacterControl>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)
                .Where(control => control != this).ToList();
        }

        private void Update()
        {
            _nameTxt.rotation = Quaternion.identity;
            _timeStun -= Time.deltaTime;
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

            if (!_groundCurrent)
            {
                _currTimeCanDown = _maxTimeCanDown;
            }
            else
            {
                _currTimeCanDown -= Time.deltaTime;
            }


            if (_isPlayer && Input.GetKeyDown(KeyCode.DownArrow))
            {
                MoveDown();
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
        

        private void MoveDown()
        {
            if (_currTimeCanDown < 0 && _groundCurrent && _rigidbody.velocity.y <= 0f)
            {
                var gr = _groundCurrent;
                Physics2D.IgnoreCollision(_collider, gr);
                this.DelayedCall(0.5f, () => { Physics2D.IgnoreCollision(_collider, gr, false); });
            }
        }

        public void UpdateBot()
        {
            if (_testMode)
            {
                return;
            }
            
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
                    groundBelow = true;
                    if (!_groundWish)
                    {
                        _groundWish = raycastHit2D.collider.GetComponent<GroundControl>();
                    }

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
                        _groundWish = groundWish.GetComponentInParent<GroundControl>();
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

            bool idle = true;

            if (_groundWish)
            {
                if (_groundWish.Center.y > transform.position.y && _rigidbody.velocity.y < 0)
                {
                    Jump();
                }
                else if (_groundWish.Center.y < transform.position.y - 0.5f && _rigidbody.velocity.y <= 0)
                {
                    MoveDown();
                }

                if (_groundWish.Center.x - transform.position.x > 0.5f)
                {
                    MoveRight();
                    idle = false;
                }
                else if (_groundWish.Center.x - transform.position.x < -0.5f)
                {
                    MoveLeft();
                    idle = false;
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

            var size = 0;
            if (_resultsHits == null)
            {
                _resultsHits = new RaycastHit2D[10];
            }

            if (idle)
            {
                size = Physics2D.RaycastNonAlloc(_gunControl.PosFire.position + Vector3.right * 100, Vector2.left,
                    _resultsHits, 200, _layerMaskChar);
            }
            else
            {
                size = Physics2D.RaycastNonAlloc(_gunControl.PosFire.position, transform.right,
                    _resultsHits, 100, _layerMaskChar);
            }

            _listCharEnemyRaycast.Clear();
            for (int i = 0; i < size; i++)
            {
                var hit = _resultsHits[i];
                if (hit.collider != _collider)
                {
                    _listCharEnemyRaycast.Add(hit.collider.GetComponent<CharacterControl>());
                }
            }

            _listCharEnemyRaycast = _listCharEnemyRaycast.Where(control =>
                (control.transform.position - transform.position).magnitude > 1).ToList();

            if (_listCharEnemyRaycast.Count == 0 && idle)
            {
                var listCheck = _listCharEnemy.Where(control => control._groundCurrent)
                    .ToList();
                if (listCheck.Count > 0)
                {
                    var enemy = listCheck.GetEst((curr, max) =>
                    {
                        if ((curr.transform.position - transform.position).magnitude <
                            (max.transform.position - transform.position).magnitude)
                        {
                            return true;
                        }

                        return false;
                    });

                    if (enemy)
                    {
                        _groundWish = enemy._groundCurrent.GetComponent<GroundControl>();
                    }
                }
                else
                {
                    if (_groundWish == null ||
                        (_groundCurrent && _groundCurrent.gameObject == _groundWish.gameObject))
                    {
                        _groundWish = GameplayManager.Instance.ListGroundControls.GetRandom();
                    }
                }
            }

            var target = _listCharEnemyRaycast.GetEst((curr, currmax) =>
            {
                if (Mathf.Abs(curr.transform.position.x - transform.position.x) <
                    Mathf.Abs(currmax.transform.position.x - transform.position.x))
                {
                    return true;
                }

                return false;
            });

            if (target && _gunControl.CanShoot)
            {
                if (target.transform.position.x < transform.position.x)
                {
                    TurnLeft();
                }
                else
                {
                    TurnRight();
                }

                _gunControl.Shoot();
            }
        }

        private void Idle()
        {
            SetAnimMove("FootIdle");
        }

        private void MoveLeft()
        {
            if (_rigidbody.velocity.x > -_maxSpeedX && _timeStun <= 0)
            {
                _rigidbody.AddForce(Vector2.left * _moveSpeed);
            }

            TurnLeft();
            SetAnimMove("FootMove");
        }

        private void TurnLeft()
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        private void MoveRight()
        {
            if (_rigidbody.velocity.x < _maxSpeedX && _timeStun <= 0)
            {
                _rigidbody.AddForce(Vector2.right * _moveSpeed);
            }

            TurnRight();
            SetAnimMove("FootMove");
        }

        private void TurnRight()
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
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
            if (_timeStun < 0)
            {
                _timeStun = 0;
            }

            if (!_isPlayer)
            {
                _timeStun += 0.25f;
            }

            _rigidbody.AddForce(dmg * _rigidbody.mass, ForceMode2D.Impulse);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Die"))
            {
                Debug.LogError("DIE");
                if (_isPlayer)
                {
                    GameplayManager.Instance.Lose();
                }
                else
                {
                    GameplayManager.Instance.Victory();
                }
            }
        }

        public void ChangeSkinColor()
        {
            var collect = GetComponentsInChildren<SpriteRenderer>()
                .Where(spriteRenderer => spriteRenderer.gameObject.CompareTag("Skin"));
            foreach (var spriteRenderer in collect)
            {
                spriteRenderer.color = _color;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Gift"))
            {
                other.gameObject.SetActive(false);
                ChangeGun();
            }
        }

        [ContextMenu("CHANGE GUN")]
        public void ChangeGun()
        {
            ChangeGun(RandomUlts.Range(2,4));
        }

        public void ChangeGun(int index)
        {
            Destroy(_gunControl.gameObject);

            _gunControl = Instantiate(Resources.Load<GunControl>($"Prefabs/Guns/{index}"), transform);
            _gunControl.Character = this;
        }
    }
}