using System.Collections.Generic;
using System.Linq;

public class SettingState : ISwitcher
{
    private readonly List<IStates> _states;
    private IStates _currentState;

    public SettingState(SettingMenu settingMenu)
    {
        _states = new List<IStates>
        {
            new PauseState(this, settingMenu),
            new RestartState(this, settingMenu),
            new ReturnState(this, settingMenu),
        };

        _currentState = _states[0];
        _currentState.Enter();
    }

    public void SwitchState<T>() where T : IStates
    {
        IStates states = _states.FirstOrDefault(states => states is T);

        _currentState.Exit();
        _currentState = states;
        _currentState.Enter();
    }
}