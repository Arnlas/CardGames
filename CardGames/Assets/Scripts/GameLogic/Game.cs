using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Game : MonoBehaviour
{
    [SerializeField] protected int handSizeMin = 3;
    [SerializeField] protected int handSizeMax = 3;
    protected List<Player> _players;
    public abstract void Initialize();
    public abstract void GameStart(List<Player> players, Deck deck);
    public abstract void RoundStart();
    public abstract void RoundEnd();

    public abstract List<Parameters.CardParameter> GetParameters();
}
