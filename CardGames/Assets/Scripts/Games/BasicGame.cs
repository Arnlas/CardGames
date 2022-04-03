using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class BasicGame : Game
{
    [SerializeField] private string param1Name, param2Name;
    [SerializeField] private int min1, max1, min2, max2;
    private List<Parameters.CardParameter> _parameters;
    private Dictionary<int, Card> currentTable = new Dictionary<int, Card>();
    private Dictionary<int, int[]> scoreBoard = new Dictionary<int, int[]>();
    private Deck _deck;
    private int _currentPlayer = 0;
    
    public override void Initialize()
    {
        _parameters = new List<Parameters.CardParameter>();
        _parameters.Add(new Parameters.CardParameter<int>(param1Name, min1, max1));
        _parameters.Add(new Parameters.CardParameter<int>(param2Name, min2, max2));
    }

    public override void GameStart(List<Player> players, Deck deck)
    {
        _deck = deck;
        _players = players;
        scoreBoard = new Dictionary<int, int[]>();
        
        int handSize = Random.Range(handSizeMin, handSizeMax);
        for (int i = 0; i < players.Count; i++)
        {
            players[i].TakeInitialHand(handSize, deck);
            scoreBoard.Add(i, new int[max1 - min1 + 1]);
        }

        RoundStart();
    }

    public override void RoundStart()
    {
        Debug.LogError($"Round Started for player {_currentPlayer}");
        _players[_currentPlayer].TurnStart(card =>
        {
            currentTable.Add(_currentPlayer, card);
            _currentPlayer++;
            if (_currentPlayer >= _players.Count)
            {
                RoundEnd();
                return;
            }

            RoundStart();
        });
    }

    public override void RoundEnd()
    {
        var sortedDict = from entry in
            currentTable orderby entry.Value.GetParameter<int>(param2Name) descending select entry;

        scoreBoard[sortedDict.First().Key][sortedDict.First().Value.GetParameter<int>(param1Name)] += 1;

        for (int i = 0; i < _players.Count; i++)
        {
            Debug.LogError(Mathf.Pow(scoreBoard[i][0],2) + Mathf.Pow(scoreBoard[i][1],2) +
                           Mathf.Pow(scoreBoard[i][2],2) + Mathf.Pow(scoreBoard[i][3],2));
        }
        
        foreach (var pair in currentTable) _deck.AddBottomCard(pair.Value);
        _deck.Shuffle();
        currentTable = new Dictionary<int, Card>();
        foreach (Player p in _players) p.TakeCard(_deck);
        _currentPlayer = 0;
        RoundStart();
    }

    public override List<Parameters.CardParameter> GetParameters()
    {
        return _parameters;
    }
}
