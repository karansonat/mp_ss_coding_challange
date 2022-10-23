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

    public Action GameStarted;
    public Action GameOver;
    public Action LevelComplete;
    public Action<int> ScoreEarned;
    public Action<double> ScoreMultiplierUpdated;
    public Action<UnlockableItemData> ItemUnlocked;
    public Action WaveStateCompleted;
    public Action WaveElementDestoryed;
    public Action BossKilled;

    //UI Events
    public Action<int, bool> ScoreUpdated;
    public Action StartButtonPressed;
    public Action RestartButtonPressed;

    #endregion //Events
}
