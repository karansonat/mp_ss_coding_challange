using System;

public class EventManager
{
    #region Fields

    public static EventManager Instance = new EventManager();

    #endregion //Fields

    #region Constructor

    private EventManager() { }
    static EventManager() { }

    #endregion //Constructor

    #region Events

    public Action GameOver;
    public Action<int> ScoreEarned;

    #endregion //Events
}
