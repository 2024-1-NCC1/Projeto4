using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Movement : MonoBehaviour
{
    public float jumpHeight;
    public float speed;
    public float rotateSpeed;
    Rigidbody rb;
    bool onJump;
     private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(Vector3.forward * Input.GetAxis("Horizontal") * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.UpArrow) && !onJump)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            onJump = true;
           
        } 
        
        
    
    }

    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Ground":
                onJump = false;
                break;
            case "Trampoline":
                onJump = false;
                rb.AddForce(Vector3.up * jumpHeight * 2, ForceMode.Impulse);
                break;
        }
    }
}
