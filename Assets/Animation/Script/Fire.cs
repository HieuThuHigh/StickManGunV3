using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public AudioSource sound_fire;
    private bool isFire = false;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Firee(){
        if(!isFire){
            anim.Play("Fire");
            sound_fire.Play();
        }else{
            anim.StopPlayback();
        }
    }
}
