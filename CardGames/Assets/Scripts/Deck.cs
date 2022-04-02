using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class Deck : MonoBehaviour
{
    private int _deckSize = 0;
    public List<Card> cards;

    public virtual void Initialize(int size)
    {
        _deckSize = size;
    }

    public abstract void CreateDeck(List<Parameters.CardParameter> parameters);
    
    public virtual bool GetCardTop(out Card card)
    {
        card = null;
        if (cards.Count == 0) return false;
        card = cards.Last();
        cards.RemoveAt(cards.Count - 1);
        return true;
    }

    public virtual bool GetCardBottom(out Card card)
    {
        card = null;
        if (cards.Count == 0) return false;
        card = cards.Last();
        cards.RemoveAt(0);
        return true;
    }

    public virtual void AddTopCard(Card card)
    {
        cards.Add(card);
    }

    public virtual void AddBottomCard(Card card)
    {
        cards.Insert(0, card);
    }

    public virtual void Shuffle()
    {
        cards.Shuffle();
    }
}
