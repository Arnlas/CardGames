using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UICardParameters
{
    public string key;
    public TMPro.TextMeshProUGUI textField;
}
public class CardUI : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private List<UICardParameters> parameters = new List<UICardParameters>();

    private Player _player;

    public void AssignCard(Player player, Card card)
    {
        _player = player;
        _player.PlayerTurnStart.AddListener(Activate);
        _player.PlayerTurnEnd.AddListener(Deactivate);
        
        foreach (var p in parameters)
        {
            if (!card.IsParameterPresent(p.key, out int val)) continue;
            p.textField.text = val.ToString();
        }
        
        button.onClick.AddListener(() => {player.TurnEnd(card);});
        card.CardRemoved.AddListener(DetachCard);
    }

    public void DetachCard()
    {
        _player.PlayerTurnStart.RemoveListener(Activate);
        _player.PlayerTurnEnd.RemoveListener(Deactivate);
        button.onClick.RemoveAllListeners();
        _player = null;
        Destroy(this.gameObject);
    }

    private void Activate()
    {
        button.interactable = true;
    }

    public void Deactivate()
    {
        button.interactable = false;
    }
}
