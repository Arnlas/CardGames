using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControl : Control
{
    public override void Initialize(PlayerUIController controller)
    {
        _uiController = controller;
        _uiController.player.PlayerTurnStart.AddListener(TurnStart);
    }

    private void TurnStart()
    {
        StartCoroutine(TurnStartEnumerator());
    }

    private IEnumerator TurnStartEnumerator()
    {
        int moves = Random.Range(1, 5);
        int cardId = 0;
        for (int i = 0; i < moves; i++)
        {
            cardId = Random.Range(0, _uiController.Cards.Count);
            SelectCard(_uiController.Cards[cardId]);
            yield return new WaitForSeconds(1.0f);
            if (i + 1 != moves) DeselectCard(_uiController.Cards[cardId]);
        }
        
        PickCard(_uiController.Cards[cardId]);
    }

    public override void AttachControl(CardUI card)
    {
        //card.OnPointEnter.AddListener(SelectCard);
        //card.OnPointExit.AddListener(DeselectCard);
        //card.OnPointClick.AddListener(PickCard);
    }

    public override void SelectCard(CardUI card)
    {
        _uiController.OnPointEnter(card);
    }

    public override void DeselectCard(CardUI card)
    {
        _uiController.OnPointExit(card);
    }

    public override void PickCard(CardUI card)
    {
        _uiController.OnPointClick(card);
    }
}
