using System;
using UnityEngine;

public class BossShieldController : MonoBehaviour
{
    #region Fields

    [SerializeField] private Collider _hitBox;
    [SerializeField] private GameObject _shield;
    [SerializeField] private GameObject _flashParticlePrefab;

    #endregion //Fields

    #region Unity Methods

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBolt"))
        {
            SpawnFlashParticleAt(other.transform.position);
            Destroy(other.gameObject);
        }
    }

    private void SpawnFlashParticleAt(Vector3 contactPoint)
    {
        Instantiate(_flashParticlePrefab, contactPoint, Quaternion.identity);
    }

    #endregion //Unity Methods

    #region Public Methods

    public void SetEnable(bool isEnabled)
    {
        _hitBox.enabled = !isEnabled;
        _shield.SetActive(isEnabled);
    }

    #endregion //Public Methods
}
