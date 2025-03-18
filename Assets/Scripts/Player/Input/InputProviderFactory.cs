using UnityEngine;

public class InputProviderFactory
{
    public static IInputProvider GetInputProvider(Character character)
    {
        var playerInput = character.PlayerInput;

        if (Application.isMobilePlatform || Application.platform == RuntimePlatform.WebGLPlayer)
        {
            playerInput.CharacterPC.Disable();
            playerInput.CharacterTouch.Enable();
            return new TouchInputProvider(character);
        }
        else
        {
            playerInput.CharacterTouch.Disable();
            playerInput.CharacterPC.Enable();
            return new KeyboardInputProvider(character);
        }
    }
}