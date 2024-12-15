using System;
using System.Collections.Generic;
using System.Linq;
using DatdevUlts.AnimationUtils;
using DatdevUlts.Ults;
using UnityEngine;
using Photon.Pun;

namespace _GunMayHem.Gameplay
{
    public class character2 : MonoBehaviour
    {
        [SerializeField] private AnimatorController _animator1;
        [SerializeField] private Rigidbody2D _rigidbody1;
        [SerializeField] private Collider2D _collider1;
        [SerializeField] private Gun2 _gunControl1;
        [SerializeField] private List<Transform> _listPosCheckGround1;
        [SerializeField] private Transform _nameTxt1;
        [SerializeField] private float _moveSpeed1;
        [SerializeField] private float _maxSpeedX1;
        [SerializeField] private float _jumpForce1;
        [SerializeField] private bool _isPlayer1;
        [SerializeField] private bool _testMode1;
        [SerializeField] private int _maxJumps1;
        [SerializeField] private Color _color1;
        private List<character2> _listCharEnemy1 = new List<character2>();


        //______________________________________________VARIABLE

        private LayerMask _layerMaskGround1;
        private LayerMask _layerMaskChar1;

        private int _currentJumps1;
        private float _timeStun1;
        private float _maxTimeCanDown1 = 1f;
        private float _currTimeCanDown1 = 0.5f;
        private bool _isGrounded1;
        private Collider2D _groundCurrent1;

        // ___________________________________________BOT
        private GroundControl _groundWish1;
        private List<character2> _listCharEnemyRaycast1 = new List<character2>();

        //_____________________________________________PROPERTY
        public bool IsPlayer => _isPlayer1;
        public bool IsBot => !_isPlayer1;
        private bool _isMovingLeft = false;
        private bool _isMovingRight = false;
        public PhotonView photonView;
        public GameObject controlButtons; // Tham chiếu tới GameObject chứa nút bấm
        public const string PLAYER_READY = "IsPlayerReady";
        public const string PLAYER_LOADED_LEVEL = "PlayerLoadedLevel";

        private RaycastHit2D[] _resultsHits1 = new RaycastHit2D[10];

        private void Start()
        {
            photonView = GetComponent<PhotonView>();
            // Kiểm tra nếu không phải là nhân vật của người chơi, ẩn các nút bấm
            if (!photonView.IsMine)
            {
                controlButtons.SetActive(false);
            }
            else
            {
                controlButtons.SetActive(true);
            }

            ChangeSkinColor();

            _layerMaskGround1 = LayerMask.GetMask("Ground");
            _layerMaskChar1 = LayerMask.GetMask("Player");

            _listCharEnemy1 = FindObjectsByType<character2>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)
                .Where(control => control != this).ToList();
        }

        private void Update()
        {
            // Kiểm tra xem người chơi có sở hữu nhân vật này không
            if (!photonView.IsMine) return;

            if (_isMovingLeft)
            {
                MoveLeft();
            }

            else if (_isMovingRight)
            {
                MoveRight();
            }
            else
            {
                Idle(); // Không di chuyển, chuyển về trạng thái Idle.
            }
            _nameTxt1.rotation = Quaternion.identity;
            _timeStun1 -= Time.deltaTime;

            _isGrounded1 = false;
            _groundCurrent1 = null;
            if (_rigidbody1.velocity.y <= 0.1f)
            {
                foreach (var pos in _listPosCheckGround1)
                {
                    var raycastHit2D = Physics2D.Raycast(pos.position, Vector2.down, 0.1f, _layerMaskGround1);
                    if (raycastHit2D.collider)
                    {
                        _isGrounded1 = true;
                        _currentJumps1 = _maxJumps1;
                        _groundCurrent1 = raycastHit2D.collider;
                        break;
                    }
                }
            }

            if (!_groundCurrent1)
            {
                _currTimeCanDown1 = _maxTimeCanDown1;
            }
            else
            {
                _currTimeCanDown1 -= Time.deltaTime;
            }

            // UpdateBot();
        }

        public void StartMoveLeft()
        {
            if (photonView.IsMine)
            {
                photonView.RPC("RPC_StartMoveLeft", RpcTarget.All);
            }

        }

        public void StopMoveLeft()
        {
            if (photonView.IsMine)
            {
                photonView.RPC("RPC_StopMoveLeft", RpcTarget.All);
            }
        }
        [PunRPC]
        private void RPC_StartMoveLeft()
        {
            _isMovingLeft = true;
            SetAnimMove("FootMove");
        }
        [PunRPC]
        private void RPC_StopMoveLeft()
        {
            _isMovingLeft = false;
            Idle();
        }
        public void StartMoveRight()
        {
            if (photonView.IsMine)
            {
                photonView.RPC("RPC_StartMoveRight", RpcTarget.All);
            }
        }

        public void StopMoveRight()
        {
            if (photonView.IsMine)
            {
                photonView.RPC("RPC_StopMoveRight", RpcTarget.All);
            }
        }
        [PunRPC]
        private void RPC_StartMoveRight()
        {
            _isMovingRight = true;
            SetAnimMove("FootMove");
        }
        [PunRPC]
        private void RPC_StopMoveRight()
        {
            _isMovingRight = false;
            Idle();
        }
        public void ButtonJump()
        {
            if (photonView.IsMine)
            {
                photonView.RPC("RPC_Jump", RpcTarget.All);
            }
        }
        [PunRPC]
        private void RPC_Jump()
        {
            Jump();
        }
        public void ButtonMoveDown()
        {
            if (photonView.IsMine)
            {
                photonView.RPC("RPC_Down", RpcTarget.All);
            }
        }
        [PunRPC]
        private void RPC_Down()
        {
            MoveDown();
        }

        private void MoveDown()
        {
            if (_currTimeCanDown1 < 0 && _groundCurrent1 && _rigidbody1.velocity.y <= 0f)
            {
                var gr = _groundCurrent1;
                Physics2D.IgnoreCollision(_collider1, gr);
                this.DelayedCall(0.5f, () => { Physics2D.IgnoreCollision(_collider1, gr, false); });
            }
        }

        public void UpdateBot()
        {
            if (_testMode1)
            {
                return;
            }

            if (_isPlayer1)
            {
                return;
            }

            bool groundBelow = false;
            foreach (var pos in _listPosCheckGround1)
            {
                var raycastHit2D = Physics2D.Raycast(pos.position, Vector2.down, 100, _layerMaskGround1);
                if (raycastHit2D.collider)
                {
                    groundBelow = true;
                    if (!_groundWish1)
                    {
                        _groundWish1 = raycastHit2D.collider.GetComponent<GroundControl>();
                    }

                    break;
                }
            }

            if (!groundBelow)
            {
                if (_currentJumps1 <= 0)
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
                        _groundWish1 = groundWish.GetComponentInParent<GroundControl>();
                    }
                    else
                    {
                        _groundWish1 = null;
                    }
                }
                else
                {
                    _groundWish1 = GameplayManager.Instance.ListPos.GetEst((current, est) =>
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

            if (_groundWish1)
            {
                if (_groundWish1.Center.y > transform.position.y && _rigidbody1.velocity.y < 0)
                {
                    Jump();
                }
                else if (_groundWish1.Center.y < transform.position.y - 0.5f && _rigidbody1.velocity.y <= 0)
                {
                    MoveDown();
                }

                if (_groundWish1.Center.x - transform.position.x > 0.5f)
                {
                    MoveRight();
                    idle = false;
                }
                else if (_groundWish1.Center.x - transform.position.x < -0.5f)
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
            if (_resultsHits1 == null)
            {
                _resultsHits1 = new RaycastHit2D[10];
            }

            if (idle)
            {
                size = Physics2D.RaycastNonAlloc(_gunControl1.PosFire.position + Vector3.right * 100, Vector2.left,
                    _resultsHits1, 200, _layerMaskChar1);
            }
            else
            {
                size = Physics2D.RaycastNonAlloc(_gunControl1.PosFire.position, transform.right,
                    _resultsHits1, 100, _layerMaskChar1);
            }

            _listCharEnemyRaycast1.Clear();
            for (int i = 0; i < size; i++)
            {
                var hit = _resultsHits1[i];
                if (hit.collider != _collider1)
                {
                    _listCharEnemyRaycast1.Add(hit.collider.GetComponent<character2>());
                }
            }

            _listCharEnemyRaycast1 = _listCharEnemyRaycast1.Where(control =>
                (control.transform.position - transform.position).magnitude > 1).ToList();

            if (_listCharEnemyRaycast1.Count == 0 && idle)
            {
                var listCheck = _listCharEnemy1.Where(control => control._groundCurrent1)
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
                        _groundWish1 = enemy._groundCurrent1.GetComponent<GroundControl>();
                    }
                }
                else
                {
                    if (_groundWish1 == null ||
                        (_groundCurrent1 && _groundCurrent1.gameObject == _groundWish1.gameObject))
                    {
                        _groundWish1 = GameplayManager.Instance.ListGroundControls.GetRandom();
                    }
                }
            }

            var target = _listCharEnemyRaycast1.GetEst((curr, currmax) =>
            {
                if (Mathf.Abs(curr.transform.position.x - transform.position.x) <
                    Mathf.Abs(currmax.transform.position.x - transform.position.x))
                {
                    return true;
                }

                return false;
            });

            if (target && _gunControl1.CanShoot)
            {
                if (target.transform.position.x < transform.position.x)
                {
                    TurnLeft();
                }
                else
                {
                    TurnRight();
                }

                _gunControl1.Shoot();
            }
        }

        private void Idle()
        {
            SetAnimMove("FootIdle");
        }

        private void MoveLeft()
        {
            if (_rigidbody1.velocity.x > -_maxSpeedX1 && _timeStun1 <= 0)
            {
                _rigidbody1.AddForce(Vector2.left * _moveSpeed1);
            }

            TurnLeft();
            // SetAnimMove("FootMove");
        }

        private void TurnLeft()
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        private void MoveRight()
        {
            if (_rigidbody1.velocity.x < _maxSpeedX1 && _timeStun1 <= 0)
            {
                _rigidbody1.AddForce(Vector2.right * _moveSpeed1);
            }

            TurnRight();
            // SetAnimMove("FootMove");
        }

        private void TurnRight()
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        private void Jump()
        {
            if (_currentJumps1 <= 0)
            {
                return;
            }

            _animator1.SetAnimation("FootJump", true, mixDuration: 0.1f,
                onEnd: () => { _animator1.SetAnimation("FootIdle", true, mixDuration: 0f); });

            var vector2 = _rigidbody1.velocity;
            vector2.y = _jumpForce1;
            _rigidbody1.velocity = vector2;
            _currentJumps1--;
        }

        private void SetAnimMove(string animName)
        {
            if (_isGrounded1 && _animator1.AnimName != animName && _animator1.AnimName != "FootJump")
            {
                _animator1.SetAnimation(animName, true);
            }
        }

        public void TakeDmg(Vector3 dmg)
        {
            
            if (_timeStun1 < 0)
            {
                _timeStun1 = 0;
            }

            if (!_isPlayer1)
            {
                _timeStun1 += 0.25f;
            }

            _rigidbody1.AddForce(dmg * _rigidbody1.mass, ForceMode2D.Impulse);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Die"))
            {
                Debug.LogError("DIE");
                if (_isPlayer1)
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
                spriteRenderer.color = _color1;
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
            ChangeGun(RandomUlts.Range(2, 4));
        }

        public void ChangeGun(int index)
        {
            if (photonView.IsMine)
            {
                photonView.RPC("RPC_ChangeGun", RpcTarget.All, index);
            }
        }
        [PunRPC]
        private void RPC_ChangeGun(int index)
        {
            if (_gunControl1 != null)
            {
                Destroy(_gunControl1.gameObject);
            }

            _gunControl1 = Instantiate(Resources.Load<Gun2>($"Prefabs/Guns/{index}"), transform);
            _gunControl1.Character2 = this;
        }
    }
}