using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI player1Name, player2Name;

    void Start()
    {
        if (StaticMembers.jogadores == null)
            StaticMembers.jogadores = new List<Jogador>();
    }

    public void StartGame()
    {
        if (player1Name.text.Length < 3 || player2Name.text.Length < 3)
            return;

        StaticMembers.jogadores.Add(new Jogador(player1Name.text));
        StaticMembers.jogadores.Add(new Jogador(player2Name.text));

        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }
}
