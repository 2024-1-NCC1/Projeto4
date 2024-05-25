using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
     public GameObject player; // O objeto do jogador que a câmera seguirá
    private Vector3 offset; // A distância entre o jogador e a câmera

    void Start()
    {
        // Encontre o jogador pelo Tag
        player = GameObject.FindGameObjectWithTag("Player");

        // Calcule a distância entre o jogador e a câmera
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        // Atualize a posição da câmera para seguir o jogador,
        // mantendo a mesma distância e inclinação
        transform.position = player.transform.position + offset;

        // Mantenha a mesma rotação da câmera
        transform.LookAt(player.transform.position);
    }
}
