using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBom : MonoBehaviour
{
    public GameObject bombPrefab;
    public Transform throwPoint; 
    public float minThrowInterval = 4f; 
    public float maxThrowInterval = 7f;
    public Transform player; // Đối tượng người chơi

    private float hihiih = 9f;
    private void Start()
    {
        StartCoroutine(ThrowBombs());
    }

    private IEnumerator ThrowBombs()
    {
        while (true)
        {
            float throwInterval = Random.Range(minThrowInterval, maxThrowInterval);
            yield return new WaitForSeconds(throwInterval);

            ThrowBomb();
        }
    }

    private void ThrowBomb()
    {
        if (bombPrefab != null && throwPoint != null && player != null)
        {
            GameObject bomb = Instantiate(bombPrefab, throwPoint.position, throwPoint.rotation);
            
            // Tính toán hướng ném về phía người chơi
            Vector2 direction = (player.position - throwPoint.position).normalized;

            // Lấy Rigidbody2D của bom để áp dụng lực
            Rigidbody2D bombRb = bomb.GetComponent<Rigidbody2D>();
            if (bombRb != null)
            {
                float throwForce = 500f; // Điều chỉnh lực ném nếu cần
                bombRb.AddForce(direction * throwForce);
            }
        }
        else
        {
            Debug.LogWarning("Prefab của bom, điểm ném hoặc đối tượng người chơi chưa được gán.");
        }
    }
}
