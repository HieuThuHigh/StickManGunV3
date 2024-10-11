using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player; // Nhân vật mà camera sẽ theo dõi
    [SerializeField] private Vector3 offset; // Khoảng cách giữa camera và nhân vật
    [SerializeField] private float smoothSpeed = 0.125f; // Tốc độ chuyển động mượt mà

    private void LateUpdate()
    {
        // Tính toán vị trí mong muốn của camera dựa trên vị trí của nhân vật và khoảng cách offset
        Vector3 desiredPosition = player.position + offset;

        // Sử dụng Vector3.Lerp để di chuyển camera từ vị trí hiện tại đến vị trí mong muốn một cách mượt mà
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Cập nhật vị trí của camera
        transform.position = smoothedPosition;
    }
}

