using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject gameMenu;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameMenu.gameObject.activeSelf)
            {
                gameMenu.gameObject.SetActive(false);
            }
            else
            {
                gameMenu.gameObject.SetActive(true);
            }
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("TelaInicial");
    }

    public void Voltar()
    {
        gameMenu.gameObject.SetActive(false);
    }
}
