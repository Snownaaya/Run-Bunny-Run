using UnityEngine;
using UnityEngine.InputSystem;

public class InputProviderFactory
{
    public static IInputProvider GetInputProvider(Character character)
    {
        if (Application.isMobilePlatform || Application.platform == RuntimePlatform.WebGLPlayer)
        {
            return new TouchInputProvider(character);
        }
        else
        {
            return new KeyboardInputProvider(character);
        }
    }
}