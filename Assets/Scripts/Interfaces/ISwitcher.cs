public interface ISwitcher
{
    void SwitchState<T>() where T : IStates;
}
