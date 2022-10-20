using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimedUpgradeType
{
    Speed = 0,
    FireRate = 1,
    Health = 2,
    Score = 3
}

[CreateAssetMenu(menuName = "Data/Create New Timed Upgrade", fileName = "TimedUpgrade")]
public class TimedUpgradeData : ScriptableObject
{
    #region Fields

    [SerializeField] private TimedUpgradeType _type;
    [SerializeField] private double _multiplier;

    public TimedUpgradeType Type => _type;
    public double Multiplier => _multiplier;

    #endregion //Fields
}
