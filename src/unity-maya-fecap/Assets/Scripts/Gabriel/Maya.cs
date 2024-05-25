using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maya : MonoBehaviour
{
    Rigidbody rb; 
    public float speed; 
    public bool pulo; // Variável para verificar se o objeto está pulando
    public float jumpHeight;
    public float fallMultiplier; // Adicionado para controlar a velocidade da queda
    public float TrampolineForce;

    //Variáveis referente a mecânica de Dash
    private bool canDash = true;
    private bool isDashing;
    public float dashingPower;
    private float dashingTime = 0.2f;
    public float dashingCooldown;


    

    [SerializeField] private TrailRenderer tr;

    // Método Start é chamado antes do primeiro frame de atualização
    void Start()
    {
        // Inicializando a referência ao componente Rigidbody
        rb = GetComponent<Rigidbody>();

        Physics.gravity = new Vector3(0, -50.0F, 0);
    }

    // Método Update é chamado uma vez por frame
    void Update()
    {

        if(isDashing)
        {
            return;
        }
        // Obtendo o input do usuário
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 movementDirection = new Vector2(x, y);

        // Chamando o método Walk com a direção de movimento
        Walk (movementDirection);

        // Verificando se a tecla Espaço foi pressionada e se o objeto não está pulando
        if (Input.GetKeyDown(KeyCode.Space) && pulo == false)
        {
            // Aplicando uma força para cima para fazer o objeto pular
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            // Marcando que o objeto está pulando
            pulo = true;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
      //  print("Jump enabled: " + pulo);

        // Botao especial da Maya fazendo Capoeira 

    }

    // Método para fazer o objeto andar
    private void Walk(Vector2 movementDirection)
    {
        // Definindo a velocidade do objeto
        rb.velocity = (new Vector2(movementDirection.x * speed, rb.velocity.y));
    
    }

    // Método chamado quando o objeto entra em uma colisão
    void OnCollisionEnter(Collision other)
    {
        // Verificando se o objeto colidiu com o chão
        if(other.gameObject.tag == "Ground"){
            // Reiniciando o pulo
            pulo = false;
        }

        if(other.gameObject.tag == "Trampoline"){
            pulo = true;
            rb.AddForce(Vector3.up * TrampolineForce, ForceMode.Impulse);
            
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        Vector3 originalGravity = Physics.gravity; // Salva a gravidade original
        Physics.gravity = Vector3.zero; // Define a gravidade para zero

        // Obtém a direção atual do movimento
        float movementDirection = Input.GetAxis("Horizontal");

        // Aplica a velocidade do dash na direção x do movimento
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