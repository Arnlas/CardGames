using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasicDeck : Deck
{
    public override void CreateDeck(List<Parameters.CardParameter> parameters)
    {
        if (parameters.Count != 2) throw new InvalidOperationException("Parameters size must equal to 2!!!");
        for (int i = 0; i < 2; i++)
            if (!(parameters[i] is Parameters.CardParameter))
                throw new InvalidOperationException("Input must be CardParameter type int!!!");

        cards = new List<Card>();
        var param1 = parameters[0] as Parameters.CardParameter<int>;
        var param2 = parameters[1] as Parameters.CardParameter<int>;
        for (int i = param1.Min; i <= param1.Max; i++)
        for (int j = param2.Min; j <= param2.Max; j++)
        {
            cards.Add(CreateCard(param1.Key, i, param2.Key, j));
        }
        
    }
    private Card CreateCard(string key1, int val1, string key2, int val2)
    {
        Card card = new Card();
        card.AddParameter(key1, val1);
        card.AddParameter(key2, val2);
        return card;
    }
}
