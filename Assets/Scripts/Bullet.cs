using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Fields

    [SerializeField] private float _damage;

    #endregion //Fields

    #region Unity Methods


    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
            damageable.ApplyDamage(_damage);
    }

    #endregion //Unity Methods
}