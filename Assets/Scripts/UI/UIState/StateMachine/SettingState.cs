using System;
using System.Collections.Generic;

public class SettingState : ISwitcher
{
    private readonly Dictionary<Type, IStates> _stateDictionary;
    private readonly ICoroutineRunner _coroutineRunner;
    private IStates _currentState;
    private SettingMenu _settingMenu;

    public SettingState(SettingMenu settingMenu, ICoroutineRunner coroutineRunner)
    {
        _settingMenu = settingMenu;
        _coroutineRunner = coroutineRunner;

        _stateDictionary = new Dictionary<Type, IStates>
        {
            { typeof(PauseState), new PauseState(this, settingMenu) },
            { typeof(ReturnState), new ReturnState(this, settingMenu) },
            { typeof(RestartState), new RestartState(this, settingMenu, _coroutineRunner) },
        };
    }

    public void SwitchState<T>() where T : IStates
    {
        if (_stateDictionary.TryGetValue(typeof(T), out IStates state))
        {
            if (_currentState != null && _currentState.GetType() == typeof(T))
                return;

            _currentState?.Exit();
            _currentState = state;
            _currentState.Enter();
        }
    }
}