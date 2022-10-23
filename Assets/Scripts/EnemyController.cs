using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    #region Fields

    [SerializeField] private float _maxHealth;
    [SerializeField] private GameObject _damageParticle;
    [SerializeField] private GameObject _explosion;
    [SerializeField] private int _scoreValue;
    protected float _currentHealth;

    #endregion //Fields

    #region Unity Methods

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    #endregion //Unity Methods

    #region IDamageable Interface

    void IDamageable.ApplyDamage(float damage)
    {
        OnDamage(damage);

        if (_currentHealth <= 0f)
            OnKilled();
    }

    #endregion //IDamageable Interface

    #region Private Methods

    protected virtual void OnDamage(float damage)
    {
        Instantiate(_damageParticle, transform.position, transform.rotation);
        _currentHealth -= damage;
    }

    protected virtual void OnKilled()
    {
        Instantiate(_explosion, transform.position, transform.rotation);
        EventManager.Instance.ScoreEarned.Invoke(_scoreValue);
        Destroy(gameObject);
    }

    #endregion //Private Methods
}
