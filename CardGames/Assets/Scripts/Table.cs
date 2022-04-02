using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField] private GameType gameType;
    
    [HideInInspector] public Table Instance;
    
    private Game game;

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

        switch (gameType)
        {
            case GameType.Basic:
                game = new BasicGame();
                break;
        }
        
        _isInitialised = true;
    }
}
