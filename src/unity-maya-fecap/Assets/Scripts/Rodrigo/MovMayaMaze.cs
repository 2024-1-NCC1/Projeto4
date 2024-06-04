using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovMayaMaze : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public bool pulo;
    public float jumpHeight;
    public float fallMultiplier;
    public float TrampolineForce;
    private Animator anim;
    private float distToGround;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        Physics.gravity = new Vector3(0, -50.0F, 0);
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    void Update()
    {
        float x = 0;
        float z = 0;

        if (Input.GetKey(KeyCode.W))
        {
            z = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            z = -1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            x = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            x = 1;
        }

        Vector3 movementDirection = new Vector3(x, 0, z);

        Walk(movementDirection);

        if (Input.GetKey(KeyCode.C))
        {
            anim.SetBool("Capoeira", true);
        }
        else
        {
            anim.SetBool("Capoeira", false);
        }

        RaycastHit hit;
        bool isGrounded = Physics.Raycast(transform.position, -Vector3.up, out hit, distToGround + 0.1f);
        if (!isGrounded && rb.velocity.y < 0)
        {
            anim.SetBool("IsFalling", true);
            anim.SetBool("IsGrounded", false);
        }
        else if (isGrounded)
        {
            anim.SetBool("IsFalling", false);
            anim.SetBool("IsGrounded", true);
        }
    }

    private void Walk(Vector3 movementDirection)
    {
        rb.velocity = new Vector3(movementDirection.x * speed, rb.velocity.y, movementDirection.z * speed);

        Quaternion targetRotation = transform.rotation;

        if (movementDirection != Vector3.zero)
        {
            anim.SetBool("Run", true);

            if (movementDirection.x > 0)
            {
                targetRotation = Quaternion.Euler(0, 90, 0);
            }
            else if (movementDirection.x < 0)
            {
                targetRotation = Quaternion.Euler(0, -90, 0);
            }
            else if (movementDirection.z > 0)
            {
                targetRotation = Quaternion.Euler(0, 0, 0);
            }
            else if (movementDirection.z < 0)
            {
                targetRotation = Quaternion.Euler(0, 180, 0);
            }

            float rotationSpeed = 11.7f;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            anim.SetBool("Run", false);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("IsJumping", false);
            anim.SetBool("IsFalling", false);
            anim.SetBool("IsGrounded", true);
            pulo = false;
        }
    }
}
