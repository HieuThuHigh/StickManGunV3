using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
       animator = GetComponent<Animator>(); 
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
            animator.Play("JumpLeftLeg");
            animator.Play("JumpRightLeg");
        }
    }
}
