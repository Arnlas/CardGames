using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private BasicGame game;
    [SerializeField] private TMPro.TextMeshProUGUI textField;

    private void Awake()
    {
        game.ScoreUpdated.AddListener(NewScore);
    }

    private void NewScore(List<Player> players, Dictionary<int, int[]> score)
    {
        string text = "";
        
        for (int i = 0; i < players.Count; i++)
        {
            int finalScore = 0;
            
            for (int j = 0; j < score[i].Length; j++) finalScore += (int)Mathf.Pow(score[i][j], 2);
            text += $"Player {i} Score = {finalScore} \n\r";
        }

        textField.text = text;
    }
}
