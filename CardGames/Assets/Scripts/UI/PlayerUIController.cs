using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject cardPrefab;

    private void Awake()
    {
        if (!player) return;

        player.NewCard.AddListener(NewCard);
    }

    private void NewCard(Card card)
    {
        var newCard = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity, this.transform);
        CardUI ui = newCard.GetComponent<CardUI>();
        ui.AssignCard(player, card);
        ui.Deactivate();
    }
}
