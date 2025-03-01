using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterStateMachine : ISwitcher
{
    private List<ISwitcher> _states;
    private IStates _currentStates;

    public CharacterStateMachine(Player player)
    {
        
    }

    public void SwitchState<T>() where T : IStates
    {
        throw new System.NotImplementedException();
    }
}
