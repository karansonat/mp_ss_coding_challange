using UnityEngine;

public class WaveElement : MonoBehaviour
{
    #region Unity Methods

    private void OnDestroy()
    {
        EventManager.Instance.WaveElementDestoryed?.Invoke();
    }

    #endregion //Unity Methods
}
