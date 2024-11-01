using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    public GameObject circlePrefab;
    private GameObject circle;
    // Start is called before the first frame update
    void Start()
    {
       animator = GetComponent<Animator>(); 
       rb = GetComponent<Rigidbody>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)){
            animator.Play("RightLeg");
            animator.Play("LeftLeg");
        }
        
        if(Input.GetKeyDown(KeyCode.D)){
            animator.Play("LeftLeg");
            animator.Play("RightLeg");
        }
        if(Input.GetKeyDown(KeyCode.W)){
            Jump();
        }
    }
   void Jump(){
        // Hiệu ứng nhảy ở đây
        // Sau khi nhảy xong, tạo vòng tròn dưới chân
        circle = Instantiate(circlePrefab, new Vector3(
        transform.position.x, 
        transform.position.y - 1f, 
        transform.position.z), Quaternion.identity);
        Destroy(circle, 0.5f); // Biến mất vòng tròn sau 0.5 giây
   }
}
