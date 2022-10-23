using UnityEngine;

public class BossController : EnemyController
{
    #region Fields

    [SerializeField] private EvasiveManeuver _evasiveManeuver;
    [SerializeField] private GameObject _weapons;
    [SerializeField] private BossShieldController _shield;
    [SerializeField] private WaveFactory _meteorWaveFactory;
    
    public WaveFactory MeteorWave => _meteorWaveFactory;

    #endregion //Fields

    #region Overriden Methods

    protected override void OnKilled()
    {
        base.OnKilled();

        EventManager.Instance.BossKilled.Invoke();
    }

    #endregion //Overriden Methods

    #region Public Methods

    public void SetManeuver(bool isEnabled)
    {
        _evasiveManeuver.enabled = isEnabled;
    }

    public void SetWeapons(bool isEnabled)
    {
        _weapons.SetActive(isEnabled);
    }

    public void SetShield(bool isEnabled)
    {
        _shield.SetEnable(isEnabled);
    }

    #endregion //Public Methods
}
