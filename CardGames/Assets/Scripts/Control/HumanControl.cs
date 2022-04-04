using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanControl : IControl
{
    public override void Initialize(PlayerUIController controller)
    {
        _uiController = controller;
    }

    public override void AttachControl(CardUI card)
    {
        card.OnPointEnter.AddListener(SelectCard);
        card.OnPointExit.AddListener(DeselectCard);
        card.OnPointClick.AddListener(PickCard);
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
