public interface IState
{
    #region Public Methods

    void Begin();
    IState Update();
    void End();

    #endregion //Public Methods
}
