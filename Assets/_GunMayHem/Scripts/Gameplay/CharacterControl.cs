using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DatdevUlts.AnimationUtils;
using DatdevUlts.Ults;
using GameTool.Assistants.DesignPattern;
using GameToolSample.GameDataScripts.Scripts;
using GameToolSample.Scripts.Enum;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI; // Thêm thư viện để sử dụng Button

namespace _GunMayHem.Gameplay
{
    public class CharacterControl : MonoBehaviour
    {
        public int textAmoutRandom;

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
        [SerializeField] public bool _testMode;
        [SerializeField] private int _maxJumps;
        [SerializeField] private Color _color;

        private List<CharacterControl> _listCharEnemy = new List<CharacterControl>();

        [SerializeField] private bool isFreeze;
        [SerializeField] private GameObject stunObject;
        [SerializeField] private bool isShield;
        [SerializeField] private bool isJumping;
        [SerializeField] private GameObject shieldObject;
        [SerializeField] private GameObject jumpTextImage;


        //______________________________________________VARIABLE

        private LayerMask _layerMaskGround;
        private LayerMask _layerMaskChar;

        private int _currentJumps;
        private float _timeStun;
        private float _maxTimeCanDown = 1f;
        private float _currTimeCanDown = 0.5f;
        private bool _isGrounded;
        private Collider2D _groundCurrent;
        private bool _isMovingLeft = false;
        private bool _isMovingRight = false;
        public Button fireButton; // Nút bấm bắn súng

        // ___________________________________________BOT
        private GroundControl _groundWish;
        private List<CharacterControl> _listCharEnemyRaycast = new List<CharacterControl>();

        //_____________________________________________PROPERTY
        public bool IsPlayer => _isPlayer;
        public bool IsBot => !_isPlayer;


        private RaycastHit2D[] _resultsHits = new RaycastHit2D[10];

        private void Start()
        {
            _maxJumps = 1; // Số lần nhảy mặc định
            _currentJumps = 1; // Số lần nhảy hiện tại

            ChangeSkinColor();
            _testMode = true;
            _layerMaskGround = LayerMask.GetMask("Ground");
            _layerMaskChar = LayerMask.GetMask("Player");

            _listCharEnemy = FindObjectsByType<CharacterControl>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)
                .Where(control => control != this).ToList();
            this.RegisterListener(EventID.Freeze, OnFreezeButton);
            this.RegisterListener(EventID.Shield, OnShieldButton);
            this.RegisterListener(EventID.Jump, OnJumpButton);
        }

        private void OnFreezeButton(Component arg1, object[] arg2)
        {
            if (!_isPlayer)
            {
                isFreeze = true;
                stunObject.SetActive(true);
                StartCoroutine(UnfreezeAfterDelay(5f));
            }
        }

        private void OnShieldButton(Component arg1, object[] arg2)
        {
            if (_isPlayer)
            {
                isShield = true;
                shieldObject.SetActive(true);
                StartCoroutine(UnshieldAfterDelay(5f));
            }
        }

        private void OnJumpButton(Component arg1, object[] arg2)
        {
            if (_isPlayer)
            {
                _maxJumps = 2; // Cho phép nhảy 2 lần
                _currentJumps = 2; // Cập nhật số lần nhảy hiện tại
                jumpTextImage.SetActive(true);

                // Reset lại sau 10 giây
                Invoke(nameof(ResetJumpLimit), 10f);
            }
        }

        private IEnumerator UnfreezeAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            isFreeze = false;
            stunObject.SetActive(false);
        }

        private IEnumerator UnshieldAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            isShield = false;
            shieldObject.SetActive(false);
        }

        private void ResetJumpLimit()
        {
            _maxJumps = 1; // Giới hạn nhảy về mặc định
            _currentJumps = 1; // Số lần nhảy hiện tại về mặc định
            jumpTextImage.SetActive(false);
        }

        private void OnDestroy()
        {
            this.RemoveListener(EventID.Freeze, OnFreezeButton);
            this.RemoveListener(EventID.Shield, OnFreezeButton);
        }

        private void Update()
        {
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

            _nameTxt.rotation = Quaternion.identity; // Giữ cố định tên
            _timeStun -= Time.deltaTime;

            // Nhảy
            

            // Kiểm tra chạm đất
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
                        _currentJumps = _maxJumps; // Reset số lần nhảy khi chạm đất
                        _groundCurrent = raycastHit2D.collider;
                        break;
                    }
                }
            }

            // Kiểm tra thời gian rơi xuống
            if (!_groundCurrent)
            {
                _currTimeCanDown = _maxTimeCanDown;
            }
            else
            {
                _currTimeCanDown -= Time.deltaTime;
            }

            // Di chuyển xuống
            

            // Di chuyển ngang
            

            UpdateBot();
        }

        public void StartMoveLeft()
        {
            _isMovingLeft = true;
            SetAnimMove("FootMove");
        }

        public void StopMoveLeft()
        {
            _isMovingLeft = false;
            Idle();
        }


        public void StartMoveRight()
        {
            _isMovingRight = true;
            SetAnimMove("FootMove");
        }

        public void StopMoveRight()
        {
            _isMovingRight = false;
            Idle();
        }

        public void downbutton()
        {
            MoveDown();
        }

        public void jumbbutton()
        {
            Jump();
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

            if (isFreeze)
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
            if (isShield)
            {
                return;
            }

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

                    // Tạo một số ngẫu nhiên từ 0 đến 2 để chọn một trong ba thuộc tính
                    int randomAttribute = Random.Range(0, 3); // 0 = Freeze, 1 = Shield, 2 = Jump
                    // Tạo số ngẫu nhiên từ 1 đến 3 cho số lượng
                    int randomAmount = Random.Range(1, 4);
                    VictoryReward.Instance.UIReward(randomAmount, randomAttribute);
                    // Áp dụng giá trị cho thuộc tính được chọn
                    switch (randomAttribute)
                    {
                        case 0:
                            GameData.Freeze += randomAmount;
                            break;
                        case 1:
                            GameData.Shield += randomAmount;
                            break;
                        case 2:
                            GameData.Jump += randomAmount;
                            break;
                    }
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
            ChangeGun(RandomUlts.Range(2, 4));
        }

        public void ChangeGun(int index)
        {
            // Hủy bỏ sự kiện cũ nếu có
            if (fireButton != null)
            {
                fireButton.onClick.RemoveAllListeners();
            }
            Destroy(_gunControl.gameObject);

            // Đăng ký sự kiện bắn súng cho nút bắn với súng mới
            if (fireButton != null)
            {
                fireButton.onClick.AddListener(() => _gunControl.Shoot());
            }
            _gunControl = Instantiate(Resources.Load<GunControl>($"Prefabs/Guns/{index}"), transform);
            _gunControl.Character = this;
            
        }
    }
}