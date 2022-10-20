using System;
using UnityEngine;

public class PlayerInteractionHandler : MonoBehaviour, IInteractionHandler
{
    #region Fields

    private PlayerEventManager _eventManager;

    #endregion //Fields

    #region Public Methods

    public void Init(PlayerEventManager eventManager)
    {
        _eventManager = eventManager;
    }

    #endregion //Public Methods

    #region IInteractionHandler Interface Implementation

    void IInteractionHandler.Handle(Interaction interaction)
    {
        switch (interaction.Type)
        {
            case InteractionType.BonusScore:
                _eventManager.BonusScore?.Invoke((int)interaction.Value);
                break;
            case InteractionType.HealthBoost:
                break;
            case InteractionType.SpeedBoost:
                break;
            case InteractionType.UnlockItem:
                _eventManager.ItemUnlocked?.Invoke(interaction.Object as UnlockableItemData);
                break;
            case InteractionType.TimedUpgrade:
                _eventManager.TimedUpgrade.Invoke(interaction.Object as TimedUpgradeData, interaction.Value);
                break;
        }
    }

    #endregion //IInteractionHandler Interface Implementation
}

public class PlayerEventManager
{
    public Action<int> BonusScore;
    public Action<TimedUpgradeData, double> TimedUpgrade;
    public Action<UnlockableItemData> ItemUnlocked;
}