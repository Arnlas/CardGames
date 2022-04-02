using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGame : Game
{
    private List<Parameters.CardParameter> _parameters;
    private Deck _deck;
    public override void Initialize()
    {
        _parameters = new List<Parameters.CardParameter>();
        _parameters.Add(new Parameters.CardParameter<int>("suit", 0, 3));
        _parameters.Add(new Parameters.CardParameter<int>("value", 2, 14));
    }

    public override void GameStart(List<Player> players, Deck deck)
    {
        _deck = deck;
        int handSize = Random.Range(handSizeMin, handSizeMax);
        foreach (Player p in players) p.TakeInitialHand(handSize, deck);
    }

    public override void RoundStart()
    {
        throw new System.NotImplementedException();
    }

    public override List<Parameters.CardParameter> GetParameters()
    {
        return _parameters;
    }
}
