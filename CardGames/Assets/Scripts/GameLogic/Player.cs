using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public List<Card> Cards => _cards;
    public UnityEvent<Card> NewCard = new UnityEvent<Card>();
    public UnityEvent<bool> HandUpdated = new UnityEvent<bool>();
    public UnityEvent PlayerTurnStart = new UnityEvent();
    public UnityEvent PlayerTurnEnd = new UnityEvent();
    
    
    private List<Card> _cards = new List<Card>();
    private Action<Card> _resultAction;
    private int _maxHandSize;

    public void TakeInitialHand(int count, Deck deck)
    {
        _maxHandSize = count;
        TakeCards(count, deck);
    }
    public void TakeCards(int count, Deck deck)
    {
        for (int i = 0; i < count; i++)
        {
            if (!TakeCard(deck, true)) return;
        }
    }

    public bool TakeCard(Deck deck, bool invokeEvent = true)
    {
        if (!deck.GetCardTop(out Card card)) return false;
        _cards.Add(card);
        if (invokeEvent)
        {
            NewCard.Invoke(card);
            HandUpdated.Invoke(true);
        }
        return true;
    }

    public void TurnStart(Action<Card> action)
    {
        _resultAction = action;
        PlayerTurnStart.Invoke();
    }

    public void TurnEnd(Card card)
    {
        card.CardRemoved.Invoke();
        _cards.Remove(card);
        _resultAction.Invoke(card);
        _resultAction = null;
        PlayerTurnEnd.Invoke();
        HandUpdated.Invoke(false);
    }
}
