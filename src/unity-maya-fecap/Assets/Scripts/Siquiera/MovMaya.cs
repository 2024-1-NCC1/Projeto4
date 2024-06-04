using System.Collections;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class MovMaya : MonoBehaviour

{
    public Rigidbody rb;
    public float speed;
    public bool pulo ; 
    public float jumpHeight;
    public float fallMultiplier; 
    public float TrampolineForce;
    private Animator anim;
    float distToGround;

    public LayerMask layerMask;
    public bool IsGrounded;
    public float GroubdCheckSize;
    public Vector3 GroundCheckPosition;

    private bool canDash = true;
    private bool isDashing;
    public float dashingPower;
    private float dashingTime = 0.2f;
    public float dashingCooldown;


    [SerializeField] private TrailRenderer tr;

    void Start()

    {
        rb = GetComponent<Rigidbody>();

        anim = GetComponent<Animator>();

        Physics.gravity = new Vector3(0, -50.0F, 0);

    }
    void Update()
    {
        if (isDashing)
        {
            return;
        }
        float x = Input.GetAxis("Horizontal");

        float y = Input.GetAxis("Vertical");

        Vector2 movementDirection = new Vector2(x, y);


        Walk(movementDirection);
  

   if (Input.GetKeyDown(KeyCode.Space) && !pulo)
    {
        rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        anim.SetBool("IsJumping", true);
        pulo = true;
    }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
      

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
    if (!isGrounded && GetComponent<Rigidbody>().velocity.y < 0)
    {
        anim.SetBool("IsJumping", false); // Adicionado esta linha
        anim.SetBool("IsFalling", true);
        anim.SetBool("IsGrounded", false);
    }
    else if (isGrounded)
    {
        anim.SetBool("IsFalling", false);
        anim.SetBool("IsGrounded", true);
    }
       
    }
   
    private void Walk(Vector2 movementDirection)
    {
    rb.velocity = new Vector2(movementDirection.x * speed, rb.velocity.y);

    Quaternion targetRotation = transform.rotation;

    if (movementDirection != Vector2.zero)
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
        IsGrounded = Physics.OverlapSphere(transform.position + GroundCheckPosition, GroubdCheckSize, layerMask).Length != 0;

        if (other.gameObject.CompareTag("Ground"))
        {   
            anim.SetBool("IsJumping", false);
            anim.SetBool("IsFalling", false);
            anim.SetBool("IsGrounded", true);
            pulo = false;
        }
    
        if (other.gameObject.CompareTag("Trampoline"))
        {
            pulo = true;
            rb.AddForce(Vector3.up * TrampolineForce * 1.4f, ForceMode.Impulse);
        }

        if (other.gameObject.CompareTag("DeathWater"))
        {
            MayaVida mayaVidaObject = GetComponent<MayaVida>();
            mayaVidaObject.Die();
        }
    }

private void OnDrawGizmos()
{
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position + GroundCheckPosition, GroubdCheckSize);
}
    private IEnumerator Dash()
    {

        canDash = false;

        isDashing = true;

        Vector3 originalGravity = Physics.gravity; // Salva a gravidade original 

        Physics.gravity = Vector3.zero; // Define a gravidade para zero 

        // Obt�m a dire��o atual do movimento 

        float movementDirection = Input.GetAxis("Horizontal");


        // Aplica a velocidade do dash na dire��o x do movimento 

        rb.velocity = new Vector3(movementDirection * dashingPower, 0f, 0f);

        tr.emitting = true;

        yield return new WaitForSeconds(dashingTime);

        tr.emitting = false;

        Physics.gravity = originalGravity; // Restaura a gravidade original 

        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);

        canDash = true;

    }

}