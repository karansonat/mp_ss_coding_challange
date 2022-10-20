using UnityEngine;
using UnityEngine.Events;

public class UnlockableItemController : MonoBehaviour
{
    #region Fields

    [SerializeField] private UnlockableItemData _data;
    [SerializeField] private UnityEvent _onUnlocked;

    private bool _isUnlocked;

    #endregion //Fields

    #region Unity Events

    private void Awake()
    {
        _isUnlocked = false;
    }

    private void OnEnable()
    {
        EventManager.Instance.ItemUnlocked += OnItemUnlocked;
    }

    private void OnDisable()
    {
        EventManager.Instance.ItemUnlocked -= OnItemUnlocked;
    }

    private void OnItemUnlocked(UnlockableItemData itemData)
    {
        if (!_isUnlocked && itemData == _data)
            _onUnlocked?.Invoke();
    }

    #endregion //Unity Events
}
