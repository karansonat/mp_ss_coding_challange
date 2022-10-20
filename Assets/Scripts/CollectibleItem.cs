using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CollectibleItem : MonoBehaviour
{
    #region Fields

    [SerializeField] private CollectibleItemData _data;
    private bool _collected;

    #endregion //Fields

    #region Unity Methods

    private void Awake()
    {
        _collected = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_collected)
            return;

        if (_data.CanBeCollectedBy.Any(tag => other.CompareTag(tag)))
        {
            Debug.Log($"TriggerEnter: {other.name} ItemId: {GetInstanceID()}");
            other.GetComponent<IInteractionHandler>()?.Handle(_data.OnCollected);
            Collect();
        }
    }

    #endregion //Unity Methods

    #region Private Methods

    public void Collect()
    {
        Debug.Log($"Item ({gameObject.GetInstanceID()}) Collected! Result: {_data.OnCollected.Type}");
        _collected = true;
        Destroy(gameObject);
    }

    #endregion //Private Methods

}