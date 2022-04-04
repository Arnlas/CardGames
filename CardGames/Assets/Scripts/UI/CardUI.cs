using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class UICardParameters
{
    public string key;
    public TMPro.TextMeshProUGUI textField;
}

public class CardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [HideInInspector] public UnityEvent<CardUI> OnPointEnter = new UnityEvent<CardUI>();
    [HideInInspector] public UnityEvent<CardUI> OnPointExit = new UnityEvent<CardUI>();
    [HideInInspector] public UnityEvent<CardUI> OnPointClick = new UnityEvent<CardUI>();
    [HideInInspector] public Card Card => _card;

    public bool Clickable = false;
    [SerializeField] private GameObject front;
    [SerializeField] private GameObject back;
    [SerializeField] private List<UICardParameters> parameters = new List<UICardParameters>();

    private Card _card;
    private Player _player;
    void Update()
    {
        if (!this.transform.hasChanged) return;

        if (Vector3.Dot(this.transform.forward, Vector3.forward) > 0)
        {
            front.SetActive(true);
            back.SetActive(false);
        }
        else
        {
            front.SetActive(false);
            back.SetActive(true);
        }
    }

    public void AssignCard(Player player, Card card)
    {
        _card = card;
        _player = player;
        _player.PlayerTurnStart.AddListener(Activate);
        _player.PlayerTurnEnd.AddListener(Deactivate);
        
        foreach (var p in parameters)
        {
            if (!card.IsParameterPresent(p.key, out int val)) continue;
            p.textField.text = val.ToString();
        }

        card.CardRemoved.AddListener(DetachCard);
        Clickable = true;
    }

    public void DetachCard()
    {
        _player.PlayerTurnStart.RemoveListener(Activate);
        _player.PlayerTurnEnd.RemoveListener(Deactivate);
        _player = null;
    }

    private void Activate()
    {
        Clickable = true;
    }

    public void Deactivate()
    {
        Clickable = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!Clickable) return;
        OnPointEnter.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!Clickable) return;
        OnPointExit.Invoke(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!Clickable) return;
        OnPointClick.Invoke(this);
    }
}
