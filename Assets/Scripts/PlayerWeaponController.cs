using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject _shot;
    [SerializeField] private Transform _shotSpawn;
    [SerializeField] private float _fireRate;
    [SerializeField] private string _fireButton;

    private AudioSource _audioSource;
    private float _nextFire;

    #endregion //Fields

    #region Unity Methods

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetButton(_fireButton) && Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate;
            Instantiate(_shot, _shotSpawn.position, _shotSpawn.rotation);
            _audioSource.Play();
        }
    }

    #endregion //Unity Methods
}
