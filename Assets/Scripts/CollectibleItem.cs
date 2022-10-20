using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CollectibleItem : MonoBehaviour
{
    #region Fields

    [SerializeField] private CollectibleItemData _data;

    #endregion //Fields

    #region Unity Methods

    private void OnTriggerEnter(Collider other)
    {
        if (_data.CanBeCollectedBy.Any(tag => other.CompareTag(tag)))
            Collect();
    }

    #endregion //Unity Methods

    #region Private Methods

    public void Collect()
    {
        Debug.Log($"Item ({_data.Name}) Collected! Result: {_data.OnCollected.Type}");
        Destroy(gameObject);
    }

    #endregion //Private Methods

}