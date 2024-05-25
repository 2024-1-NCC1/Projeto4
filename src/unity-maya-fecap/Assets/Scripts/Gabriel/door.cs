using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class door : MonoBehaviour
{
    public bool locked;
    public bool keyInRange; // Variável para verificar se a chave está no alcance
    public KeyManager keyManager; // Referência para o KeyManager
    public AudioClip lockedSound; // Som da porta trancada
    public AudioClip unlockedSound; // Som da porta destrancada
    public Text messageText; // Componente de texto para exibir a mensagem
    public string newSceneName;  // Nome da nova cena a ser carregada

    private AudioSource audioSource; // Referência para o componente de áudio

    void Start()
    {
        locked = true;
        keyInRange = false;
        audioSource = GetComponent<AudioSource>(); // Obtenha o componente de áudio

        // Tente encontrar o objeto Text na cena
        if (messageText == null)
        {
            messageText = GameObject.FindObjectOfType<Text>();
        }

        if (messageText != null)
        {
            messageText.text = ""; // Inicialize o texto como vazio
            messageText.canvasRenderer.SetAlpha(0.0f); // Inicialize a transparência do texto como 0
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Verifica se a tecla 'E' foi pressionada
        if (Input.GetKeyDown(KeyCode.E) && keyInRange)
        {
            if (keyManager != null && keyManager.isPickedUp)
            {
                locked = false;
                audioSource.PlayOneShot(unlockedSound); // Reproduz o som da porta destrancada
                
                // Espera um curto período de tempo antes de chamar a nova cena
                StartCoroutine(LoadNextScene());
            }
            else
            {
                audioSource.PlayOneShot(lockedSound); // Reproduz o som da porta trancada
            }
        }
    }

    private IEnumerator LoadNextScene()
    {
        // Espera um segundo antes de carregar a nova cena
        yield return new WaitForSeconds(1f);

        // Carrega a nova cena
        SceneManager.LoadScene(newSceneName);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            keyInRange = true;
            if (keyManager != null && !keyManager.isPickedUp && messageText != null)
            {
                messageText.text = "Preciso de uma chave."; // Exibe a mensagem quando o jogador entra no colisor da porta sem a chave
                messageText.CrossFadeAlpha(1.0f, 1.5f, false); // Fade in do texto ao longo de 1.5 segundos
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            keyInRange = false;
            if (messageText != null)
            {
                messageText.CrossFadeAlpha(0.0f, 1.5f, false); // Fade out do texto ao longo de 1.5 segundos
            }
        }
    }
}
