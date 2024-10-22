using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform target;
    public float speed = 1.0f;
    public float stopDistance = 3f; // Khoảng cách dừng lại

    void Update()
    {
        // Tính khoảng cách hiện tại đến target
        float distance = Vector2.Distance(transform.position, target.position);

        // Kiểm tra nếu khoảng cách lớn hơn stopDistance
        if (distance > stopDistance)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target.position, step);
        }
    }
}
