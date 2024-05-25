using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public Image fadeImage;  // Referência à imagem de fade
    public float fadeDuration = 1f;
    private bool isFading = false;

    private void Start()
    {
        // Inicialmente, a imagem de fade deve estar desativada
        fadeImage.gameObject.SetActive(false);
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(0f, 1f));
    }

    public void FadeOutAndLoadScene(string sceneName)
    {
        if (!isFading)
        {
            StartCoroutine(FadeOutAndSwitchScene(sceneName));
        }
    }

    public void FadeOutAndSwitchPanel(GameObject currentPanel, GameObject nextPanel)
    {
        if (!isFading)
        {
            StartCoroutine(FadeOutAndSwitch(currentPanel, nextPanel));
        }
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        isFading = true;
        fadeImage.gameObject.SetActive(true);
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, endAlpha);

        // Desativar a imagem se estiver completamente transparente
        if (endAlpha == 0f)
        {
            fadeImage.gameObject.SetActive(false);
        }

        isFading = false;
    }

    private IEnumerator FadeOutAndSwitch(GameObject currentPanel, GameObject nextPanel)
    {
        yield return Fade(0f, 1f);

        // Switch panels
        currentPanel.SetActive(false);
        nextPanel.SetActive(true);

        yield return Fade(1f, 0f);
    }

    private IEnumerator FadeOutAndSwitchScene(string sceneName)
    {
        yield return Fade(0f, 1f);

        // Carregar a cena assíncronamente
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        // Espera até que a cena esteja pronta para ser ativada
        while (!asyncLoad.isDone)
        {
            // Ativar a cena quando estiver quase carregada
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
