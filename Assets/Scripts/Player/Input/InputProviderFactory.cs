using UnityEngine;

public class InputProviderFactory
{
    private static TouchInputProvider _touchInputProviderPrefab;

    static InputProviderFactory()
    {
        _touchInputProviderPrefab = Resources.Load<TouchInputProvider>
            ("Prefabs/TouchInputProvider");
    }

    public static IInputProvider GetInputProvider()
    {
        if (Application.isMobilePlatform || Application.platform == RuntimePlatform.WebGLPlayer)
        {
            return Object.Instantiate(_touchInputProviderPrefab);
        }
        else
        {
            return new KeyboardInputProvider();
        }
    }
}
