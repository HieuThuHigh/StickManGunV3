using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float rotationX = 85f;
    public float scaleX = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(rotationX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
    }
   
}
