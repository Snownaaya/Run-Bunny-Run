using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISwitcher
{
    void SwitchState<T>() where T : IStates;
}
