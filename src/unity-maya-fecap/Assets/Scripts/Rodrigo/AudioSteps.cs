using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSteps : MonoBehaviour
{
    private Animator anim;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (anim.GetBool("Run") && anim.GetBool("IsGrounded"))
        {
            audioSource.volume = 0.3f;
        }
        else
        {
            audioSource.volume = 0;
        }
    }
}
