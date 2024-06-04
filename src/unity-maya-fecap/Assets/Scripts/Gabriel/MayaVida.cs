using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MayaVida : MonoBehaviour
{
    public Slider life;
    public Image fill; // Referência para o componente de imagem do preenchimento do slider
    public GameObject deathScreen; // Referência para a tela de morte
    public Button respawnButton; // Referência para o botão de renascer
    private CameraFollow cameraFollow; // Referência ao script de seguir a câmera
    public Camera mainCamera; // Referência pública para a câmera principal
    public float damageTaken; // Dano recebido pela personagem ao longo do tempo

    private bool isSceneWithLifeLoss; // Verifica se a cena atual é aquela em que a personagem perde vida

    void Start()
    {
        life.value = 100f;
        fill = life.fillRect.GetComponent<Image>(); // Obtenha o componente de imagem do preenchimento do slider

        // Verifique se a cena atual é a cena em que a personagem perde vida
        isSceneWithLifeLoss = SceneManager.GetActiveScene().name == "RR-StageCity" ||
            SceneManager.GetActiveScene().name == "RR-MainStage" ||
            SceneManager.GetActiveScene().name == "RR-StageForest" ||
            SceneManager.GetActiveScene().name == "RR-StageWater" ||
            SceneManager.GetActiveScene().name == "RR-StageIceLab";

        // Desabilite a tela de morte no início
        deathScreen.SetActive(false);

        // Adicione o listener para o botão de renascer
        respawnButton.onClick.AddListener(Respawn);

        // Obtenha a referência ao script CameraFollow
        if (mainCamera != null)
        {
            cameraFollow = mainCamera.GetComponent<CameraFollow>();
        }
    }

    void Update()
    {
        // Se a cena atual for a cena com perda de vida, execute a lógica de perder vida
        if (isSceneWithLifeLoss)
        {
            LoseLifeOverTime();
        }

        // Aplica a lógica de mudar a cor da barra de vida conforme a quantidade de vida
        UpdateLifeBarColor();
    }

    // Lógica para perder vida ao longo do tempo
    private void LoseLifeOverTime()
    {
        TakeDamage(damageTaken); // Ajuste o valor conforme necessário
    }

    // Lógica para mudar a cor da barra de vida
    private void UpdateLifeBarColor()
    {
        if (life.value <= 0)
        {
            Die();
        }
        else if (life.value <= 30)
        {
            fill.color = new Color32(204, 70, 46, 255); // Se a vida for menor ou igual a 30, a barra fica vermelha
        }
        else if (life.value <= 60)
        {
            fill.color = new Color32(235, 202, 96, 255); // Se a vida for menor ou igual a 60, a barra fica amarela
        }
        else
        {
            fill.color = new Color32(145, 166, 70, 255); // Se a vida for maior que 60, a barra fica verde
        }
    }

    // Função para receber dano
    public void TakeDamage(float damageTaken)
    {
        life.value -= damageTaken * Time.deltaTime;
    }

    // Função para receber cura
    public void ReceiveHealth(int healthAmount)
    {
        life.value += healthAmount * Time.deltaTime;
    }

    // Função chamada quando a vida chega a 0
    public void Die()
    {
        deathScreen.SetActive(true);
        gameObject.SetActive(false);
        // Retire a referência do personagem na câmera
        if (cameraFollow != null)
        {
            cameraFollow.target = null;
        }
    }

    // Função para renascer o personagem
    private void Respawn()
    {
        // Reinicie a cena atual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
