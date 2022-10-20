using UnityEngine;

[System.Serializable]
public class Interaction
{
    #region Fields

    [SerializeField] private InteractionType _interaction;
    [SerializeField] private Object _object;
    [SerializeField] private double _value;

    public InteractionType Type => _interaction;
    public Object Object => _object;
    public double Value => _value;

    #endregion //Fields
}