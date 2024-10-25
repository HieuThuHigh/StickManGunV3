using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miniature : MonoBehaviour
{
    private float spinSpeed = 50f; // Tốc độ quay
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime); // Quay theo trục Y với tốc độ spinSpeed
    }
    
}
