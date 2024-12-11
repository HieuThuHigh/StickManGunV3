using System;
using UnityEngine;

namespace _GunMayHem.Gameplay
{
    public class GroundControl : MonoBehaviour
    {
        private Transform _leftPoint;
        private Transform _rightPoint;

        public Transform LeftPoint => _leftPoint;

        public Transform RightPoint => _rightPoint;

        public Vector3 Center => (_leftPoint.position + _rightPoint.position) / 2;

        private void Awake()
        {
            // Lấy BoxCollider2D từ đối tượng này
            BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();

            // Tính toán các điểm góc trên của BoxCollider2D trong không gian đối tượng (local space)
            Vector2 topLeftLocal = (Vector2)transform.position + boxCollider2D.offset +
                                   new Vector2(-boxCollider2D.size.x / 2, boxCollider2D.size.y / 2);
            Vector2 topRightLocal = (Vector2)transform.position + boxCollider2D.offset +
                                    new Vector2(boxCollider2D.size.x / 2, boxCollider2D.size.y / 2);

            // Chuyển đổi các điểm từ không gian đối tượng sang không gian thế giới (world space)
            Vector2 topLeftWorld = topLeftLocal;
            Vector2 topRightWorld = topRightLocal;

            _leftPoint = new GameObject().transform;
            _leftPoint.parent = transform;
            _leftPoint.position = topLeftWorld;

            _rightPoint = new GameObject().transform;
            _rightPoint.parent = transform;
            _rightPoint.position = topRightWorld;
        }
    }
}