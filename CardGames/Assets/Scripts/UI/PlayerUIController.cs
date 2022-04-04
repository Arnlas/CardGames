using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private bool _topPlayer = false;
    [SerializeField] private Control control;
    [SerializeField] private Transform cardTablePosition;
    [SerializeField] private Transform originTransform;
    [SerializeField] private Transform handTransform;
    [SerializeField] private Transform handRadiusTransform;
    public Player player;

    public List<CardUI> Cards { get; private set; }
    private float Radius { get; set; }

    private CardUI _card2Destroy;

    private void Awake()
    {
        if (!player) return;

        player.NewCard.AddListener(NewCard);
        player.HandUpdated.AddListener(HandUpdated);
        
        Radius = Vector3.Distance(handTransform.position, handRadiusTransform.position);
        control.Initialize(this);

    }

    private void HandUpdated(bool isNew)
    {
        Cards = this.transform.GetComponentsInChildren<CardUI>().ToList();
        for (int i = 0; i < Cards.Count; i++)
            Cards[i].transform.Pos2PosArc(handTransform.position, Radius, 90.0f,
                player.Cards.Count, i, 0.5f, !_topPlayer);
    }

    private void NewCard(Card card)
    {
        var newCard = Pool.Instance.GetObject(originTransform.position, Quaternion.Euler(0, -180, 0),
            originTransform.localScale, this.transform);
        CardUI ui = newCard.GetComponent<CardUI>();

        control.AttachControl(ui);

        ui.AssignCard(player, card);
        ui.Deactivate();
    }

    public void OnPointEnter(CardUI card)
    {
        card.transform.localScale = originTransform.localScale * 2.0f;
        Vector3 shift = card.transform.up * 100;
        card.transform.Pos2PosArc(handTransform.position + shift, Radius, 90.0f,
            player.Cards.Count, Cards.IndexOf(card), 0.75f, !_topPlayer);
    }
    
    public void OnPointExit(CardUI card)
    {
        card.transform.localScale = originTransform.localScale;
        card.transform.Pos2PosArc(handTransform.position, Radius, 90.0f,
            player.Cards.Count, Cards.IndexOf(card), 0.75f, !_topPlayer);
    }
    
    public void OnPointClick(CardUI card)
    {
        foreach (CardUI c in Cards) c.Clickable = false;
        Pool.Instance.ReturnObject(card.gameObject);
        card.transform.Pos2PosScale(cardTablePosition.position, Vector3.zero, originTransform.localScale, 1.0f, ()=>
        {
            player.TurnEnd(card.Card);
            if (_card2Destroy != null) Pool.Instance.Disable(_card2Destroy.gameObject);
            _card2Destroy = card;
        });
    }
}
