using UnityEngine;

[CreateAssetMenu(menuName = "Data/Create New Collectible", fileName = "NewCollectible")]
public class CollectibleItemData : ScriptableObject
{
    #region Fields

    [SerializeField] private string _name;
    [Tooltip("Accepted Tags for Trigger Events")]
    [SerializeField] private string[] _canBeCollectedBy;
    [SerializeField] private Interaction _onCollected;

    public string Name => _name;
    public string[] CanBeCollectedBy => _canBeCollectedBy;
    public Interaction OnCollected => _onCollected;

    #endregion //Fields
}