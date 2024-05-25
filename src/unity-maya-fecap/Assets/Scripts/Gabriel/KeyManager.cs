using UnityEngine;

public class KeyManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    public bool isPickedUp;

    private Vector3 vel;
    private float oscillationHeight = 0.3f; // Altura da oscilação
    private float rotationSpeed = 90f; // Velocidade de rotação (graus por segundo)
    public float smoothTime = 0.5f; // Tempo de suavização
    private Vector3 initialPosition; // Posição inicial da chave

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (!isPickedUp)
        {
            // Movimento de subida e descida
            Vector3 offset = new Vector3(0, Mathf.Sin(Time.time) * oscillationHeight, 0);
            transform.position = Vector3.SmoothDamp(transform.position, initialPosition + offset, ref vel, smoothTime);
        }
        else
        {
            // Rotação no eixo Z após coleta
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            // Acompanhar o jogador suavemente, com deslocamento para cima
            Vector3 targetPosition = player.transform.position + Vector3.up * 2f; // Ajuste o valor conforme necessário
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref vel, smoothTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isPickedUp)
        {
            isPickedUp = true;
        }
    }
}
