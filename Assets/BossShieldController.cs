using System;
using UnityEngine;
using Lean.Pool;
using System.Collections;

public class BossShieldController : MonoBehaviour
{
    #region Fields

    [SerializeField] private Collider _hitBox;
    [SerializeField] private GameObject _shield;
    [SerializeField] private GameObject _flashParticlePrefab;

    private WaitForSeconds _flashDespawnDelay;

    #endregion //Fields

    #region Unity Methods

    private void Awake()
    {
        _flashDespawnDelay = new WaitForSeconds(2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBolt"))
        {
            SpawnFlashParticleAt(other.transform.position);
            Destroy(other.gameObject);
        }
    }

    #endregion //Unity Methods

    #region Public Methods

    public void SetEnable(bool isEnabled)
    {
        _hitBox.enabled = !isEnabled;
        _shield.SetActive(isEnabled);
    }

    #endregion //Public Methods

    #region Private Methods

    private void SpawnFlashParticleAt(Vector3 contactPoint)
    {
        SceneContext.Instance.GameController.StartCoroutine(SpawnFlashParticleRoutine(contactPoint));
    }

    private IEnumerator SpawnFlashParticleRoutine(Vector3 contactPoint)
    {
        var spawned = LeanPool.Spawn(_flashParticlePrefab, contactPoint, Quaternion.identity);

        yield return _flashDespawnDelay;

        LeanPool.Despawn(spawned);
    }

    #endregion //Private Methods
}
