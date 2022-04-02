using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameType
{
    Basic
}

public abstract class Game
{
    public GameType CurrentGameType { get; }
    public Queue<Player> Players;
    private Deck deck;
    
    protected Game(int playersCount, Deck deck){
        Players = new Queue<Player>();
        for (int i = 0; i < playersCount; i++) Players.Enqueue(new Player());
    }

    public abstract void Start();
    public abstract void RoundStart();
}
