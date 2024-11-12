using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public AudioClip soundToPlay;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
         audioSource = GetComponent<AudioSource>();
        audioSource.clip = soundToPlay;
        audioSource.Stop();
    }

     public void PlayAudioAfterDelay()
    {
        StartCoroutine(PlayAudioDelayed());
    }

    private IEnumerator PlayAudioDelayed()
    {
        yield return new WaitForSeconds(3f); // Đợi 3 giây

        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
