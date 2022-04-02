using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private List<Card> _cards = new List<Card>();
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
            if (!deck.GetCardTop(out Card card)) return;
            _cards.Add(card);
        }
    }
}
