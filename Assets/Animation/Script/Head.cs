using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    private Rigidbody2D rb;
    Quaternion initialRotation;
    // Start is called before the first frame update
    void Start()
    {
        initialRotation = transform.rotation;
        rb.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb && rb.velocity.y <0){
            StartCoroutine(RotateAndResetCoroutine());
        }
    }
       
    public void RotateAndReset()
    {
        StartCoroutine(RotateAndResetCoroutine());
    }

    IEnumerator RotateAndResetCoroutine()
    {
        Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 10f);
        Quaternion targetRotation2 = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, -10f);

        // Xoay từ trục Z 10 độ
        yield return RotateTo(targetRotation, 0.3f);

        // Xoay từ trục Z -10 độ
        yield return RotateTo(targetRotation2, 0.3f);

        // Quay trở lại vị trí ban đầu
        yield return RotateTo(initialRotation, 0.3f);
    }

    IEnumerator RotateTo(Quaternion targetRotation, float duration)
    {
        Quaternion initialRotation = transform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
    }
}
