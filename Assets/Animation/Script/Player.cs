using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    public GameObject circlePrefab;
    private GameObject circle;
    private bool isWalking = false;
    private float initialRotation;
    // Start is called before the first frame update
    void Start()
    {
       animator = GetComponent<Animator>(); 
       rb = GetComponent<Rigidbody>();
       initialRotation = transform.eulerAngles.z;
       
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isWalking)
        {
            // Kiểm tra nếu animation đang chạy, tiếp tục chạy nó
            animator.Play("LeftLeg");
            animator.Play("RightLeg");
        }
       
    }
    
   public void Jump(){
        // Hiệu ứng nhảy ở đây
        // Sau khi nhảy xong, tạo vòng tròn dưới chân
        circle = Instantiate(circlePrefab, new Vector3(
        transform.position.x, 
        transform.position.y - 1f, 
        transform.position.z), Quaternion.identity);
        Destroy(circle, 0.5f); // Biến mất vòng tròn sau 0.5 giây
   }
   public void StartWalk(){
        isWalking = true;
   }
   public void StopWalk(){
        isWalking = false;
   }
   public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerPress.name == "LeftButton")
        {
            StartWalk();
        }
        else if (eventData.pointerPress.name == "RightButton")
        {
            StartWalk();
        }
    }
     public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerPress.name == "LeftButton")
        {
            StopWalk();
        }
        else if (eventData.pointerPress.name == "RightButton")
        {
            StopWalk();
        }
    }
    public void RotateDown()
    {
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, -40f);
        Invoke("RotateBack", 0.3f); // Quay trở lại sau 1 giây
    }

    void RotateBack()
    {
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, initialRotation);
    }

}
