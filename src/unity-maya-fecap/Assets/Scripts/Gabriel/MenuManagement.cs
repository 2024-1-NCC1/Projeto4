using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagement : MonoBehaviour
{
    [SerializeField] private string nomeDoLevelDoJogo;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelOpcoes;
    [SerializeField] private GameObject painelNarrativa; // Novo painel para a narrativa
    [SerializeField] private FadeController fadeController; // Referência ao FadeController

    public void Jogar()
    {
        fadeController.FadeOutAndSwitchPanel(painelMenuInicial, painelNarrativa);
    }

    public void ProsseguirNarrativa()
    {
        fadeController.FadeOutAndLoadScene(nomeDoLevelDoJogo);
    }

    public void AbrirOpcoes()
    {
        fadeController.FadeOutAndSwitchPanel(painelMenuInicial, painelOpcoes);
    }

    public void FecharOpcoes()
    {
        fadeController.FadeOutAndSwitchPanel(painelOpcoes, painelMenuInicial);
    }

    public void SairJogo()
    {
        Debug.Log("Sair do Jogo");
        Application.Quit();
    }
}
