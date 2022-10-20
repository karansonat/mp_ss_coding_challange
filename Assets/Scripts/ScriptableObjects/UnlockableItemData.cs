using UnityEngine;

[CreateAssetMenu(menuName = "Data/Create New Unlockable Item", fileName = "UnlockableItem")]
public class UnlockableItemData : ScriptableObject
{
    #region Fields

    [SerializeField] private GameObject _itemPrefab;

    #endregion //Fields

    #region Public Methods

    public GameObject Unlock()
    {
        return Object.Instantiate(_itemPrefab);
    }

    #endregion //Public Methods
}
