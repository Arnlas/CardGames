using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class IControl : MonoBehaviour
{
    protected PlayerUIController _uiController;
    public abstract void Initialize(PlayerUIController controller);
    public abstract void AttachControl(CardUI card);
    public abstract void SelectCard(CardUI card);
    public abstract void DeselectCard(CardUI card);
    public abstract void PickCard(CardUI card);
}
