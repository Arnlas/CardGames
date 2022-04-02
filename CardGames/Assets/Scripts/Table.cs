using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    [HideInInspector] public Table Instance;
    
    [SerializeField] private int deckSize = 52;
    [SerializeField] private Game game;
    [SerializeField] private Deck deck;
    [SerializeField] private List<Player> players = new List<Player>();

    private bool _isInitialised = false;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }
    
    void Start()
    {
        if (Instance != null && Instance != this) return;

        game.Initialize();
        var parameters = game.GetParameters();
        deck.Initialize(deckSize);
        deck.CreateDeck(parameters);
        deck.Shuffle();
        
        _isInitialised = true;
    }
}
